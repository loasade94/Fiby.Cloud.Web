using Fiby.Cloud.Web.Client.Extensions;
using Fiby.Cloud.Web.DTO.Modules.Facturacion.Request;
using Fiby.Cloud.Web.DTO.Modules.Horario.Request;
using Fiby.Cloud.Web.DTO.Modules.Maintenance.Request;
using Fiby.Cloud.Web.DTO.Modules.Pagos.Request;
using Fiby.Cloud.Web.DTO.Modules.Parametro.Request;
using Fiby.Cloud.Web.Service.Interfaces.Facturacion;
using Fiby.Cloud.Web.Service.Interfaces.Maintenance;
using Fiby.Cloud.Web.Service.Interfaces.Parametro;
using Fiby.Cloud.Web.Util.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Client.Areas.Facturacion.Controllers
{
    [Authorize]
    [Area("Facturacion")]
    public class GenerarController : Controller
    {

        private readonly IClienteService _clienteService;
        private readonly ITablaDetalleService _tablaDetalleService;
        private readonly IGenerarService _generarService;

        public GenerarController(IClienteService clienteService,
                                    ITablaDetalleService tablaDetalleService,
                                    IGenerarService generarService)
        {
            _clienteService = clienteService;
            _tablaDetalleService = tablaDetalleService;
            _generarService = generarService;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.GetProfileId() != "1")
            {
                return RedirectToAction("Logout", "Account", new { Area = "" });
            }

            ViewBag.ListaTipoCliente = await _tablaDetalleService.GetTablaDetalleAll(new TablaDetalleDTORequest() { CodigoTabla = "TI01" });

            ViewBag.FechaInicial = DateTime.Now.AddMonths(-3).ToString("dd/MM/yyyy");
            ViewBag.FechaFinal = DateTime.Now.AddMonths(+3).ToString("dd/MM/yyyy");

            var model = await _generarService.ListarDocumentosGenerados(new VentaDTORequest() {
                FechaInicio = DateTime.Now.AddMonths(-3),
                FechaFin = DateTime.Now.AddMonths(+3)
            });

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> BuscarComprobantes(VentaDTORequest request)
        {
            if (User.Identity.GetProfileId() != "1")
            {
                return RedirectToAction("Logout", "Account", new { Area = "" });
            }

            request.FechaInicio = General.ConvertFormatDateTime(request.FechaInicioTexto);
            request.FechaFin = General.ConvertFormatDateTime(request.FechaFinTexto);

            ViewBag.ListaTipoCliente = await _tablaDetalleService.GetTablaDetalleAll(new TablaDetalleDTORequest() { CodigoTabla = "TI01" });

            var model = await _generarService.ListarDocumentosGenerados(request);

            return View("GridComprobantes", model);
        }

        public async Task<IActionResult> Imprimir(int idVenta)
        {
            if (User.Identity.GetProfileId() != "1")
            {
                return RedirectToAction("Logout", "Account", new { Area = "" });
            }

            //ViewBag.ListaTipoCliente = await _tablaDetalleService.GetTablaDetalleAll(new TablaDetalleDTORequest() { CodigoTabla = "TI01" });

            var venta = await _generarService.ListarVentaPorId(new VentaDTORequest() { IdVenta = idVenta });
            var detalleVenta = await _generarService.ListarDetallePorId(new VentaDTORequest() { IdVenta = idVenta });

            venta.listaDetalleVenta = detalleVenta;

            return View(venta);
        }

        [HttpPost]
        public async Task<JsonResult> BuscarDocumento(string documento)
        {
            var model = await _clienteService.GetClientePorDocumento(new ClienteDTORequest() 
            { 
                NumeroDocumento = documento
            });

            return Json(model);
        }

        [HttpPost]
        public async Task<JsonResult> CalcularIGV(PagoClienteDTORequest pagoClienteDTORequest)
        {
            string resultado = string.Empty;
            List<string> lista = new List<string>();
            try
            {
                CultureInfo culture = new CultureInfo("en-US");
                var pago = Math.Round(decimal.Parse(pagoClienteDTORequest.MontoPagoClienteText, culture), 2);
                if (pago == 0)
                {
                    lista.Add("");
                    lista.Add("");
                    lista.Add("");
                }
                else
                {
                    lista.Add(Convert.ToDecimal(string.Format("{0:0.00}", pago / 1.18m)).ToString());
                    lista.Add(Convert.ToDecimal(string.Format("{0:0.00}", pago - (pago / 1.18m))).ToString());
                    lista.Add(Convert.ToDecimal(string.Format("{0:0.00}", pago)).ToString());
                }
                
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }

            return Json(lista);
        }

        public async Task<JsonResult> RegistrarVenta(VentaDTORequest ventaDTORequest, List<DetalleVentaDTORequest> listDetalleVentaDTORequest)
        {
            string resultado = string.Empty;

            try
            {

                ventaDTORequest.IdEmpresa = 1;
                ventaDTORequest.CodigoTipoIdentificacion = ventaDTORequest.CodigoComprobante == "01" ? "6" : "1";

                CultureInfo culture = new CultureInfo("en-US");

                var subtotal = Math.Round(decimal.Parse(ventaDTORequest.TotSubtotalTexto, culture), 2);
                ventaDTORequest.TotSubtotal = subtotal;

                var igv = Math.Round(decimal.Parse(ventaDTORequest.TotalIgvTexto, culture), 2);
                ventaDTORequest.TotalIgv = igv;

                var total = Math.Round(decimal.Parse(ventaDTORequest.Monto_totalTexto, culture), 2);
                ventaDTORequest.Monto_total = total;

                var resultadoCab = await _generarService.RegistrarVenta(ventaDTORequest);

                if (resultadoCab != null)
                {
                    var listaResultado = resultadoCab;

                    if (listaResultado.Count > 0)
                    {
                        if (listaResultado[0] == "0")
                        {
                            for (int i = 0; i < listDetalleVentaDTORequest.Count; i++)
                            {

                                listDetalleVentaDTORequest[i].idventa = int.Parse(listaResultado[2]);
                                listDetalleVentaDTORequest[i].Unidad_de_medida = "ZZ";
                                listDetalleVentaDTORequest[i].cantidad = 1;

                                var Total_a_pagar = Math.Round(decimal.Parse(listDetalleVentaDTORequest[i].Total_a_pagarTexto, culture), 2);
                                listDetalleVentaDTORequest[i].Total_a_pagar = Total_a_pagar;

                                var preciounitario = Math.Round(decimal.Parse(listDetalleVentaDTORequest[i].preciounitarioTexto, culture), 2);
                                listDetalleVentaDTORequest[i].preciounitario = preciounitario;

                                var mtoValorVentaItem = Math.Round(decimal.Parse(listDetalleVentaDTORequest[i].mtoValorVentaItemTexto, culture), 2);
                                listDetalleVentaDTORequest[i].mtoValorVentaItem = mtoValorVentaItem;

                                var porIgvItem = Math.Round(decimal.Parse(listDetalleVentaDTORequest[i].porIgvItemTexto, culture), 2);
                                listDetalleVentaDTORequest[i].porIgvItem = porIgvItem;

                                var resultadoDetalle = await _generarService.RegistrarDetalleVenta(listDetalleVentaDTORequest[i]);
                            }

                            #region GenerarComprobanteXML

                            var generar = await _generarService.GenerarComprobante(listaResultado[2]);

                            var listaGenerar = generar.Split('|');

                            if (listaGenerar.Length == 2)
                            {
                                return Json(listaGenerar[1].Trim());
                            }
                            else
                            {
                                return Json(listaResultado[2]);
                            }

                            #endregion
                        }
                        else
                        {
                            resultado = listaResultado[2];
                            return Json(resultado.Trim());
                        }

                    }
                }
                else
                {
                    resultado = "Ocurrio un error al grabar el registro";
                }
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }

            return Json(resultado.Trim());
        }

        //public async Task<ActionResult> BuscarComprobantes(VentaDTORequest filtro)
        //{
        //    var modelo = await _generarService.ListarDocumentosGenerados(new VentaDTORequest() { });

        //    return View("GridComprobantes", modelo);
        //}

        public async Task<string> RegistrarBajaFactura(VentaDTORequest ventaDTORequest)
        {
            string resultado = string.Empty;

            try
            {

                var listResultadoOne = await _generarService.GenerarBaja(ventaDTORequest.IdVenta);

                if (listResultadoOne != null)
                {
                    var resultadoOne = listResultadoOne.Split('|');

                    if (resultadoOne.Length > 0)
                    {
                        if (resultadoOne[1] == "0")
                        {
                            ventaDTORequest.Ticket = resultadoOne[0];
                            ventaDTORequest.Estadosunat = "A";
                            ventaDTORequest.Codigo = resultadoOne[1];
                            var listResultadoCab = await _generarService.RegistrarBaja(ventaDTORequest);

                            if (listResultadoCab.Count > 0)
                            {
                                if (listResultadoCab[0] == "0")
                                {
                                    resultado = string.Empty;
                                }
                                else
                                {
                                    resultado = "No hay respuesta del BD";
                                }
                            }
                            else
                            {
                                resultado = "No hay respuesta del BD";
                            }
                        }
                        else
                        {
                            resultado = "No hay respuesta del servidor";
                        }

                        
                    }
                    else
                    {
                        resultado = "No hay respuesta del servidor";
                    }
                }
                else
                {
                    resultado = "No hay respuesta del servidor";
                }

               
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }

            return resultado.Trim();
        }

        public async Task<string> RegistrarBajaBoleta(VentaDTORequest ventaDTORequest)
        {
            string resultado = string.Empty;

            try
            {

                var listResultadoOne = await _generarService.GenerarBajaBoleta(ventaDTORequest.IdVenta);

                if (listResultadoOne != null)
                {
                    var resultadoOne = listResultadoOne.Split('|');

                    if (resultadoOne.Length > 0)
                    {
                        if (resultadoOne[1] == "0" || resultadoOne[1] == "0098")
                        {
                            ventaDTORequest.Ticket = resultadoOne[0];
                            ventaDTORequest.Estadosunat = "A";
                            ventaDTORequest.Codigo = resultadoOne[1];
                            var listResultadoCab = await _generarService.RegistrarBaja(ventaDTORequest);

                            if (listResultadoCab.Count > 0)
                            {
                                if (listResultadoCab[0] == "0")
                                {
                                    resultado = string.Empty;
                                }
                                else
                                {
                                    resultado = "No hay respuesta del BD";
                                }
                            }
                            else
                            {
                                resultado = "No hay respuesta del BD";
                            }
                        }
                        else
                        {
                            resultado = "No hay respuesta del servidor";
                        }


                    }
                    else
                    {
                        resultado = "No hay respuesta del servidor";
                    }
                }
                else
                {
                    resultado = "No hay respuesta del servidor";
                }


            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }

            return resultado.Trim();
        }

        [HttpPost]
        public async Task<JsonResult> BuscarDocumentosGenerados(VentaDTORequest ventaDTORequest)
        {
            ventaDTORequest.FechaInicio = General.ConvertFormatDateTime(ventaDTORequest.FechaInicioTexto);
            ventaDTORequest.FechaFin = General.ConvertFormatDateTime(ventaDTORequest.FechaFinTexto);

            var model = await _generarService.ListarDocumentosGenerados(ventaDTORequest);
            return Json(model);
        }
    }
}
