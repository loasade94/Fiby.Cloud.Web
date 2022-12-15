using Fiby.Cloud.Web.Client.App_Start.Extensions;
using Fiby.Cloud.Web.Client.Extensions;
using Fiby.Cloud.Web.DTO.Modules.Facturacion.Request;
using Fiby.Cloud.Web.DTO.Modules.Facturacion.Response;
using Fiby.Cloud.Web.DTO.Modules.Ple.Request;
using Fiby.Cloud.Web.DTO.Modules.Reportes.Request;
using Fiby.Cloud.Web.Service.Interfaces.Facturacion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Client.Areas.Facturacion.Controllers
{
    [Authorize]
    [Area("Facturacion")]
    public class CargaFacturaController : Controller
    {
        private readonly ICargaFacturaService _cargaFacturaService;
        //private readonly IPleService _pleService;
        //private readonly ISerieService _serieService;

        public CargaFacturaController(ICargaFacturaService cargaFacturaService)
        {
            _cargaFacturaService = cargaFacturaService;
        }

        public async Task<IActionResult> Index()
        {

            if (User.Identity.GetProfileId() != "1")
            {
                return RedirectToAction("Logout", "Account", new { Area = "" });
            }

            //SerieDTORequest serieDTORequest = new SerieDTORequest();
            //serieDTORequest.Description = serieDTORequest.Description == null ? string.Empty : serieDTORequest.Description;

            //ViewBag.listaSerie = await _serieService.GetSerieAll(serieDTORequest);

            ViewBag.listaMeses = Lists.GeneraMeses(todos: false, seleccione: false);
            ViewBag.listaAnios = Lists.GeneraAnios(todos: false, anioInicial: 2021);

            var model = await _cargaFacturaService.ConsultaFacturas(new CargaFacturaDTORequest()
            {
                Mes = DateTime.Now.ToString("MM"),
                Ano = DateTime.Now.ToString("yyyy")

            });

            return View(model);
        }

        [HttpPost]
        public async Task<List<string>> RegistrarCargaFactura(CargaFacturaDTORequest request)
        {
            var resultado = new List<string>();
            try
            {
                //cargaFacturaDTORequest.Mes = cargaFacturaDTORequest.Mes == null ? string.Empty : cargaFacturaDTORequest.Mes;
                request.IdEmpresa = int.Parse(User.Identity.CompanyId());
                resultado = await _cargaFacturaService.RegistrarFactura(request);
            }
            catch (Exception ex)
            {
                //resultado = ex.Message;
                var error = ex.Message;
                resultado.Add(error);
            }

            return resultado;
        }

        public async Task<FileResult> GetPle0801(string mes , string anio)
        {

            var nombreArhivoTemp = DateTime.Now.ToString("yyyyMMddhhmmss");
            TextWriter tw = new StreamWriter(Path.Combine(Path.GetTempPath(), nombreArhivoTemp + ".txt"), true);


            CargaFacturaDTORequest request = new CargaFacturaDTORequest();
            request.Mes = mes;
            request.Ano = anio;

            var model = await _cargaFacturaService.GetPle0801(request);

            for (int i = 0; i < model.Count; i++)
            {
                tw.WriteLine(model[i]);
            }

            tw.Close();

            byte[] byteArray = System.IO.File.ReadAllBytes(Path.Combine(Path.GetTempPath(), nombreArhivoTemp + ".txt"));

            return File(byteArray, "text/plain", "LE20606961805" + anio + mes + "00080100001111.txt");
        }

        public async Task<FileResult> GetPle1401(string mes, string anio)
        {
            var nombreArhivoTemp = DateTime.Now.ToString("yyyyMMddhhmmss");
            TextWriter tw = new StreamWriter(Path.Combine(Path.GetTempPath(), nombreArhivoTemp + ".txt"), true);


            CargaFacturaDTORequest request = new CargaFacturaDTORequest();
            request.Mes = mes;
            request.Ano = anio;

            var model = await _cargaFacturaService.GetPle1401(request);

            for (int i = 0; i < model.Count; i++)
            {
                tw.WriteLine(model[i]);
            }

            tw.Close();

            byte[] byteArray = System.IO.File.ReadAllBytes(Path.Combine(Path.GetTempPath(), nombreArhivoTemp + ".txt"));

            return File(byteArray, "text/plain", "LE20606961805" + anio + mes + "00140100001111.txt");
        }

        public FileResult GetPle0802(string mes, string anio)
        {
            var nombreArhivoTemp = DateTime.Now.ToString("yyyyMMddhhmmss");
            TextWriter tw = new StreamWriter(Path.Combine(Path.GetTempPath(), nombreArhivoTemp + ".txt"), true);


            CargaFacturaDTORequest request = new CargaFacturaDTORequest();
            request.Mes = mes;
            request.Ano = anio;

            var model = new List<string>();

            for (int i = 0; i < model.Count; i++)
            {
                tw.WriteLine(model[i]);
            }

            tw.Close();

            byte[] byteArray = System.IO.File.ReadAllBytes(Path.Combine(Path.GetTempPath(), nombreArhivoTemp + ".txt"));

            return File(byteArray, "text/plain", "LE20606961805" + anio + mes + "00080200001011.txt");
        }

        [HttpDelete]
        public async Task<string> EliminarFactura(int idFacturaEmpresa)
        {

            var request = new CargaFacturaDTORequest
            {
                IdFacturaEmpresa = idFacturaEmpresa,
            };

            string response = await _cargaFacturaService.EliminarFactura(request);
            return response;
        }

        [HttpPost]
        public async Task<ActionResult> ConsultaFacturas(CargaFacturaDTORequest cargaFacturaDTORequest)
        {
            cargaFacturaDTORequest.Mes = cargaFacturaDTORequest.Mes == null ? string.Empty : cargaFacturaDTORequest.Mes;
            var model = await _cargaFacturaService.ConsultaFacturas(cargaFacturaDTORequest);
            return View("Grid", model);
        }
    }
}
