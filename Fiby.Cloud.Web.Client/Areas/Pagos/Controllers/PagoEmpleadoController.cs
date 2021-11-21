using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Fiby.Cloud.Web.DTO.Modules.Horario.Request;
using Fiby.Cloud.Web.DTO.Modules.Horario.Response;
using Fiby.Cloud.Web.DTO.Modules.Pagos.Request;
using Fiby.Cloud.Web.DTO.Modules.Pagos.Response;
using Fiby.Cloud.Web.Service.Interfaces;
using Fiby.Cloud.Web.Service.Interfaces.Horario;
using Fiby.Cloud.Web.Service.Interfaces.Pagos;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Fiby.Cloud.Web.Client.Areas.Pagos.Controllers
{
    [Authorize]
    [Area("Pagos")]
    public class PagoEmpleadoController : Controller
    {

        private readonly IEmpleadoService _empleadoService;
        private readonly ISemanaService _semanaService;
        private readonly IPagoEmpleadoService _pagoEmpleadoService;
        private readonly ICalendarioService _calendarioService;
        private readonly IHostingEnvironment _env;

        public PagoEmpleadoController(IEmpleadoService empleadoService,
                                        ISemanaService semanaService,
                                        IPagoEmpleadoService pagoEmpleadoService,
                                        ICalendarioService calendarioService,
                                        IHostingEnvironment env)
        {
            _empleadoService = empleadoService;
            _semanaService = semanaService;
            _pagoEmpleadoService = pagoEmpleadoService;
            _calendarioService = calendarioService;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ListaEmpleados = await _empleadoService.GetEmpleadoAll();
            ViewBag.ListaSemana = await _semanaService.GetListaSemana();
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> BuscarPagosXEmpleadoSemana(PagoEmpleadoDTORequest pagoEmpleadoDTORequest)
        {
            var model = await _pagoEmpleadoService.GetPagosXEmpleadoSemana(pagoEmpleadoDTORequest);
            return Json(model);
        }

        [HttpPost]
        public async Task<string> RegistrarPagoEmpleado(PagoEmpleadoDTORequest pagoEmpleadoDTORequest)
        {
            string resultado = string.Empty;

            try
            {
                resultado = await _pagoEmpleadoService.RegistrarPagoEmpleado(pagoEmpleadoDTORequest);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }

            return resultado;
        }

        [HttpPost]
        public async Task<JsonResult> BuscarPagosXEmpleadoSemanaCab(PagoEmpleadoDTORequest pagoEmpleadoDTORequest)
        {
            var model = await _pagoEmpleadoService.GetPagosXEmpleadoSemanaCab(pagoEmpleadoDTORequest);
            return Json(model);
        }

        [HttpPost]
        public async Task<string> ActualizarPasajeXServicio(PagoEmpleadoDTORequest pagoEmpleadoDTORequest)
        {
            string resultado = string.Empty;

            try
            {
                CultureInfo culture = new CultureInfo("en-US");
                var pasaje = Math.Round(decimal.Parse(pagoEmpleadoDTORequest.PasajeText, culture), 2);
                pagoEmpleadoDTORequest.Pasaje = pasaje;
                resultado = await _pagoEmpleadoService.ActualizarPasajeXServicio(pagoEmpleadoDTORequest);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }

            return resultado;
        }

        public async Task<IActionResult> ActualizarPasaje(int idServicio, int option)
        {
            CalendarioDTOResponse calendarioDTOResponse = new CalendarioDTOResponse();
            CalendarioDTORequest calendarioDTORequest = new CalendarioDTORequest()
            {
                IdServicio = idServicio
            };

            if (idServicio == 0) { ViewBag.titleModal = "Agregar"; }
            if (idServicio > 0 && option == 0) { ViewBag.titleModal = "Actualizar Pasaje de Servicio"; }

            if (idServicio > 0)//EDITAR
            {
                calendarioDTOResponse = await _calendarioService.GetCalendarioById(calendarioDTORequest);
                ViewBag.ModalGeneralIsNew = "0";
            }
            else
            {
                ViewBag.ModalGeneralIsNew = "1";
            }
            return PartialView(calendarioDTOResponse);
        }

        public async Task<FileResult> GenerarBoletaPagoPDF(int idEmpleado,int idSemana)
        {
            PagoEmpleadoDTORequest pagoEmpleadoDTORequest = new PagoEmpleadoDTORequest();
            pagoEmpleadoDTORequest.IdEmpleado = idEmpleado;
            pagoEmpleadoDTORequest.IdSemana = idSemana;

            var cab = await _pagoEmpleadoService.GetPagosXEmpleadoSemanaCab(pagoEmpleadoDTORequest);

            var det = await _pagoEmpleadoService.GetPagosXEmpleadoSemana(pagoEmpleadoDTORequest);


            var myTempFile = Path.Combine(Path.GetTempPath());

            List<string> lstArray = new List<string>();
            lstArray.Add(_env.WebRootPath);
            lstArray.Add("img");
            lstArray.Add("logo.png");
            var path = Path.Combine(lstArray.ToArray());

            var strResult = await CrearBoletaEmpleadoPDF(myTempFile, path, cab, det);
            

            var myTempFileFinal = Path.Combine(Path.GetTempPath(), strResult);

            System.IO.File.Copy(myTempFileFinal, myTempFileFinal.Substring(0, myTempFileFinal.Length - 4) + "_f.pdf");

            var routeFinal = Path.Combine(Path.GetTempPath(), strResult.Substring(0, strResult.Length - 4) + "_f.pdf");

            //System.Diagnostics.Process.Start(myTempFileFinal);

            byte[] byteArray = System.IO.File.ReadAllBytes(routeFinal);
            return File(byteArray, "application/pdf", strResult);
        }

        public async Task<string> CrearBoletaEmpleadoPDF(string ruta, string ruta_imagen
                                                            ,List<PagoEmpleadoDTOResponse> cab
                                                            ,List<PagoEmpleadoDTOResponse> det)
        {
            string no_pdf = string.Empty;

            try
            {
                Font Fuente1 = new Font(FontFactory.GetFont("Arial", 10, Font.BOLD));
                Font fuente = new Font(FontFactory.GetFont("Arial", 7, Font.NORMAL));
                Font fuenteN = new Font(FontFactory.GetFont("Arial", 7, Font.BOLD));
                Font fuentePie = new Font(FontFactory.GetFont("Arial", 7, Font.NORMAL));
                Font fuenteTit = new Font(FontFactory.GetFont("Arial", 12, Font.BOLD));

                //var worker = await _workerService.GetWorkerByEnrollment(User.Identity.GetCompanyId(), enrollment, User.Identity.GetToken());

                //no_pdf = string.Format("{0}_{1}.{2}", worker.DocumentNumber.Trim(), DateTime.Now.ToString("yyyyMMddHHmmss"), "pdf");

                if (cab.Count > 0)
                {
                    no_pdf = string.Format("{0}_{1}.{2}", cab[0].ApellidoPaterno + " " + cab[0].ApellidoMaterno + " " + cab[0].Nombres,
                                        DateTime.Now.ToString("yyyyMMddHHmmss"), "pdf");

                }
                else
                {
                    no_pdf = string.Format("{0}_{1}.{2}", "Error",
                                        DateTime.Now.ToString("yyyyMMddHHmmss"), "pdf");
                }


                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                Document document = new Document(PageSize.A4, 50, 50, 25, 25);
                PdfWriter.GetInstance(document, new FileStream(string.Concat(ruta, no_pdf), FileMode.Create));
                document.Open();

                #region "Cabecera"
                Image imagen = Image.GetInstance(ruta_imagen);
                imagen.BorderWidth = 0; imagen.Alignment = Element.ALIGN_LEFT;
                //imagen.ScalePercent(24f);
                //imagen.ScaleAbsolute(150f, 70f);
                //imagen.ScaleAbsolute(10, 4);
                /*
                imagen.ScalePercent(24f);
                document.Add(imagen);
                */
                PdfPTable tableCab = new PdfPTable(3) { WidthPercentage = 100 };
                PdfPCell cell = new PdfPCell(new Phrase("", new Font(Font.NORMAL, 12f, Font.BOLD)));
                tableCab.SetWidths(new float[] { 100, 190, 190 });

                PdfPTable nested = new PdfPTable(1);
                cell.Image = imagen;
                cell.Border = 0;
                nested.AddCell(cell);

                PdfPCell nesthousing = new PdfPCell(nested);
                nesthousing.Padding = 0f;
                nesthousing.Border = 0;
                tableCab.AddCell(nesthousing);

                cell = new PdfPCell(); cell.BorderColor = BaseColor.White;
                tableCab.AddCell(cell);

                cell = new PdfPCell(); cell.BorderColor = BaseColor.White;
                tableCab.AddCell(cell);
                document.Add(tableCab);
                #endregion

                document.Add(new Paragraph(" "));
                document.Add(new Paragraph("I. FICHA DE DATOS PERSONALES", fuenteTit));
                document.Add(new Paragraph(" "));

                #region "Datos Personales"
                if (cab.Count > 0)
                {
                    PdfPTable tblDatosPersonales = new PdfPTable(4) { WidthPercentage = 100 };
                    tblDatosPersonales.SetWidths(new int[] { 25, 25, 25, 25 });
                    foreach (PagoEmpleadoDTOResponse objRal in cab)
                    {
                        Phrase phrase = new Phrase();
                        phrase.Add(new Chunk("Apellidos:", fuenteN));
                        phrase.Add(new Chunk(string.Format("{0} {1} {2}", " ", objRal.ApellidoPaterno, objRal.ApellidoMaterno), fuente));
                        cell = new PdfPCell();
                        cell.AddElement(phrase);
                        cell.Colspan = 2;
                        tblDatosPersonales.AddCell(cell);

                        phrase = new Phrase();
                        phrase.Add(new Chunk("Nombres:", fuenteN));
                        phrase.Add(new Chunk(string.Format("{0} {1}", " ", objRal.Nombres), fuente));
                        cell = new PdfPCell();
                        cell.AddElement(phrase);
                        cell.Colspan = 2;
                        tblDatosPersonales.AddCell(cell);

                    }
                    document.Add(tblDatosPersonales);
                }

                #endregion

                //#region "Familiares"
                //if (listaFamilia.Count > 0)
                //{
                //    document.Add(new Paragraph(" "));
                //    document.Add(new Paragraph("Familiares", Fuente1));
                //    document.Add(new Paragraph(" "));

                //    PdfPTable tblFamilia = new PdfPTable(7) { WidthPercentage = 100 };
                //    tblFamilia.SetWidths(new float[] { 16, 15, 13, 13, 13, 20, 10 });

                //    cell = new PdfPCell();
                //    Paragraph oParagraph = new Paragraph("APELLIDOS", fuenteN);
                //    oParagraph.Alignment = Element.ALIGN_CENTER;
                //    cell.AddElement(oParagraph);
                //    tblFamilia.AddCell(cell);

                //    cell = new PdfPCell();
                //    oParagraph = new Paragraph("NOMBRE", fuenteN);
                //    oParagraph.Alignment = Element.ALIGN_CENTER;
                //    cell.AddElement(oParagraph);
                //    tblFamilia.AddCell(cell);

                //    cell = new PdfPCell();
                //    oParagraph = new Paragraph("PARENTESCO", fuenteN);
                //    oParagraph.Alignment = Element.ALIGN_CENTER;
                //    cell.AddElement(oParagraph);
                //    tblFamilia.AddCell(cell);

                //    cell = new PdfPCell();
                //    oParagraph = new Paragraph("SEXO", fuenteN);
                //    oParagraph.Alignment = Element.ALIGN_CENTER;
                //    cell.AddElement(oParagraph);
                //    tblFamilia.AddCell(cell);

                //    cell = new PdfPCell();
                //    oParagraph = new Paragraph("FECHA NACIMIENTO", fuenteN);
                //    oParagraph.Alignment = Element.ALIGN_CENTER;
                //    cell.AddElement(oParagraph);
                //    tblFamilia.AddCell(cell);

                //    cell = new PdfPCell();
                //    oParagraph = new Paragraph("TIPO Y NRO DOCUMENTO", fuenteN);
                //    oParagraph.Alignment = Element.ALIGN_CENTER;
                //    cell.AddElement(oParagraph);
                //    tblFamilia.AddCell(cell);

                //    cell = new PdfPCell();
                //    oParagraph = new Paragraph("CONDICIÓN ACADEMICA", fuenteN);
                //    oParagraph.Alignment = Element.ALIGN_CENTER;
                //    cell.AddElement(oParagraph);
                //    tblFamilia.AddCell(cell);


                //    foreach (FamilyUpdateDTOResponse objRal in listaFamilia)
                //    {
                //        cell = new PdfPCell();
                //        cell.AddElement(new Paragraph(string.Format("{0} {1}", objRal.LastName, objRal.LastMotherName), fuente));
                //        tblFamilia.AddCell(cell);

                //        cell = new PdfPCell();
                //        cell.AddElement(new Paragraph(objRal.Name, fuente));
                //        tblFamilia.AddCell(cell);

                //        cell = new PdfPCell();
                //        cell.AddElement(new Paragraph(objRal.TypeFamilyDescription, fuente));
                //        tblFamilia.AddCell(cell);

                //        cell = new PdfPCell();
                //        cell.AddElement(new Paragraph(objRal.GenderDescription, fuente));
                //        tblFamilia.AddCell(cell);

                //        cell = new PdfPCell();

                //        if (objRal.FechaBirth == null || objRal.FechaBirth == DateTime.MinValue)
                //        {
                //            oParagraph = new Paragraph("", fuente);
                //        }
                //        else
                //        {
                //            oParagraph = new Paragraph(objRal.FechaBirth.Value.ToString("dd/MM/yyyy"), fuente);
                //        }


                //        oParagraph.Alignment = Element.ALIGN_CENTER;
                //        cell.AddElement(oParagraph);
                //        tblFamilia.AddCell(cell);

                //        cell = new PdfPCell();
                //        cell.AddElement(new Paragraph(string.Format("{0} {1}", objRal.TypeDocumentDescription, objRal.NumberDocument), fuente));
                //        tblFamilia.AddCell(cell);

                //        cell = new PdfPCell();
                //        if (objRal.IndicatorIsStudent != "0") { cell.AddElement(new Paragraph(string.Format("{0}", "ESTUDIANTE"), fuente)); }
                //        else { cell.AddElement(new Paragraph(string.Format("{0}", "NO ES ESTUDIANTE"), fuente)); }
                //        tblFamilia.AddCell(cell);

                //    }
                //    document.Add(tblFamilia);
                //}
                //#endregion

                document.Add(new Paragraph(" "));
                document.Add(new Paragraph("II. DETALLE DE PAGO", fuenteTit));
                document.Add(new Paragraph(" "));

                //document.Add(new Paragraph("Grado Academico", Fuente1));
                //document.Add(new Paragraph(" "));

                #region "Detalle de Pago"
                if (det.Count > 0)
                {
                    PdfPTable tblGradoAca = new PdfPTable(8) { WidthPercentage = 100 };
                    tblGradoAca.SetWidths(new float[] { 12, 20, 11, 11, 9, 11,11,15 });

                    cell = new PdfPCell();
                    Paragraph oParagraph = new Paragraph("DÍA", fuenteN);
                    oParagraph.Alignment = Element.ALIGN_CENTER;
                    cell.AddElement(oParagraph);
                    tblGradoAca.AddCell(cell);

                    cell = new PdfPCell();
                    oParagraph = new Paragraph("CLIENTE", fuenteN);
                    oParagraph.Alignment = Element.ALIGN_CENTER;
                    cell.AddElement(oParagraph);
                    tblGradoAca.AddCell(cell);

                    cell = new PdfPCell();
                    oParagraph = new Paragraph("HORA INICIO", fuenteN);
                    oParagraph.Alignment = Element.ALIGN_CENTER;
                    cell.AddElement(oParagraph);
                    tblGradoAca.AddCell(cell);

                    cell = new PdfPCell();
                    oParagraph = new Paragraph("HORA FIN", fuenteN);
                    oParagraph.Alignment = Element.ALIGN_CENTER;
                    cell.AddElement(oParagraph);
                    tblGradoAca.AddCell(cell);

                    cell = new PdfPCell();
                    oParagraph = new Paragraph("HORAS", fuenteN);
                    oParagraph.Alignment = Element.ALIGN_CENTER;
                    cell.AddElement(oParagraph);
                    tblGradoAca.AddCell(cell);

                    cell = new PdfPCell();
                    oParagraph = new Paragraph("SUBTOTAL", fuenteN);
                    oParagraph.Alignment = Element.ALIGN_CENTER;
                    cell.AddElement(oParagraph);
                    tblGradoAca.AddCell(cell);

                    cell = new PdfPCell();
                    oParagraph = new Paragraph("PASAJE", fuenteN);
                    oParagraph.Alignment = Element.ALIGN_CENTER;
                    cell.AddElement(oParagraph);
                    tblGradoAca.AddCell(cell);

                    cell = new PdfPCell();
                    oParagraph = new Paragraph("MONTO FINAL", fuenteN);
                    oParagraph.Alignment = Element.ALIGN_CENTER;
                    cell.AddElement(oParagraph);
                    tblGradoAca.AddCell(cell);

                    int contador = 0;

                    foreach (PagoEmpleadoDTOResponse objRal in det)
                    {

                        contador += 1;

                        if (contador == det.Count)
                        {
                            cell = new PdfPCell();
                            oParagraph = new Paragraph("TOTALES : ", fuente);
                            oParagraph.Alignment = Element.ALIGN_CENTER;
                            cell.Colspan = 4;
                            cell.AddElement(oParagraph);
                            tblGradoAca.AddCell(cell);

                            cell = new PdfPCell();
                            oParagraph = new Paragraph(objRal.Horas.ToString(), fuente);
                            oParagraph.Alignment = Element.ALIGN_CENTER;
                            cell.AddElement(oParagraph);
                            tblGradoAca.AddCell(cell);

                            cell = new PdfPCell();
                            oParagraph = new Paragraph("S/. " + objRal.SubTotal.ToString("0.00"), fuente);
                            oParagraph.Alignment = Element.ALIGN_CENTER;
                            cell.AddElement(oParagraph);
                            tblGradoAca.AddCell(cell);

                            cell = new PdfPCell();
                            oParagraph = new Paragraph("S/. " + objRal.Pasaje.ToString("0.00"), fuente);
                            oParagraph.Alignment = Element.ALIGN_CENTER;
                            cell.AddElement(oParagraph);
                            tblGradoAca.AddCell(cell);

                            cell = new PdfPCell();
                            oParagraph = new Paragraph("S/. " + objRal.Pago.ToString("0.00"), fuente);
                            oParagraph.Alignment = Element.ALIGN_CENTER;
                            cell.AddElement(oParagraph);
                            tblGradoAca.AddCell(cell);
                        }
                        else
                        {
                            cell = new PdfPCell();
                            cell.AddElement(new Paragraph(objRal.NombreDia + ' ' + objRal.NumeroDia, fuente));
                            tblGradoAca.AddCell(cell);

                            cell = new PdfPCell();
                            cell.AddElement(new Paragraph(objRal.Cliente, fuente));
                            tblGradoAca.AddCell(cell);

                            cell = new PdfPCell();
                            cell.AddElement(new Paragraph(objRal.HoraInicio, fuente));
                            tblGradoAca.AddCell(cell);

                            cell = new PdfPCell();
                            oParagraph = new Paragraph(objRal.HoraFin, fuente);
                            oParagraph.Alignment = Element.ALIGN_CENTER;
                            cell.AddElement(oParagraph);
                            tblGradoAca.AddCell(cell);

                            cell = new PdfPCell();
                            oParagraph = new Paragraph(objRal.Horas.ToString(), fuente);
                            oParagraph.Alignment = Element.ALIGN_CENTER;
                            cell.AddElement(oParagraph);
                            tblGradoAca.AddCell(cell);

                            cell = new PdfPCell();
                            oParagraph = new Paragraph("S/. " + objRal.SubTotal.ToString("0.00"), fuente);
                            oParagraph.Alignment = Element.ALIGN_CENTER;
                            cell.AddElement(oParagraph);
                            tblGradoAca.AddCell(cell);

                            cell = new PdfPCell();
                            oParagraph = new Paragraph("S/. " + objRal.Pasaje.ToString("0.00"), fuente);
                            oParagraph.Alignment = Element.ALIGN_CENTER;
                            cell.AddElement(oParagraph);
                            tblGradoAca.AddCell(cell);

                            cell = new PdfPCell();
                            oParagraph = new Paragraph("S/. " + objRal.Pago.ToString("0.00"), fuente);
                            oParagraph.Alignment = Element.ALIGN_CENTER;
                            cell.AddElement(oParagraph);
                            tblGradoAca.AddCell(cell);
                        }

                        

                    }
                    document.Add(tblGradoAca);
                }
                #endregion

                //document.Add(new Paragraph(" "));
                //document.Add(new Paragraph("Cursos/Certificaciones", Fuente1));
                //document.Add(new Paragraph(" "));

                //#region "Estudios Externos"
                //if (listaEstudiosExternos.Count > 0)
                //{
                //    PdfPTable tblEstudiosExternos = new PdfPTable(6) { WidthPercentage = 100 };
                //    tblEstudiosExternos.SetWidths(new float[] { 24, 22, 20, 12, 12, 10 });

                //    cell = new PdfPCell();
                //    Paragraph oParagraph = new Paragraph("TIPO ESTUDIO", fuenteN);
                //    oParagraph.Alignment = Element.ALIGN_CENTER;
                //    cell.AddElement(oParagraph);
                //    tblEstudiosExternos.AddCell(cell);

                //    cell = new PdfPCell();
                //    oParagraph = new Paragraph("CURSO", fuenteN);
                //    oParagraph.Alignment = Element.ALIGN_CENTER;
                //    cell.AddElement(oParagraph);
                //    tblEstudiosExternos.AddCell(cell);

                //    cell = new PdfPCell();
                //    oParagraph = new Paragraph("INSTITUCIÓN", fuenteN);
                //    oParagraph.Alignment = Element.ALIGN_CENTER;
                //    cell.AddElement(oParagraph);
                //    tblEstudiosExternos.AddCell(cell);

                //    cell = new PdfPCell();
                //    oParagraph = new Paragraph("INICIO", fuenteN);
                //    oParagraph.Alignment = Element.ALIGN_CENTER;
                //    cell.AddElement(oParagraph);
                //    tblEstudiosExternos.AddCell(cell);

                //    cell = new PdfPCell();
                //    oParagraph = new Paragraph("FIN", fuenteN);
                //    oParagraph.Alignment = Element.ALIGN_CENTER;
                //    cell.AddElement(oParagraph);
                //    tblEstudiosExternos.AddCell(cell);

                //    cell = new PdfPCell();
                //    oParagraph = new Paragraph("¿ES PROMOVIDO?", fuenteN);
                //    oParagraph.Alignment = Element.ALIGN_CENTER;
                //    cell.AddElement(oParagraph);
                //    tblEstudiosExternos.AddCell(cell);


                //    var tableId = TablesResource.TableWorkerDSE;
                //    var modelTemp = await _payrollHorizontalService.GetListGeneric(tableId, User.Identity.GetToken());

                //    for (int i = 0; i < listaEstudiosExternos.Count; i++)
                //    {

                //        for (int j = 0; j < modelTemp.Count; j++)
                //        {
                //            if (listaEstudiosExternos[i].TypeCourse == modelTemp[j].RegisterId)
                //            {
                //                listaEstudiosExternos[i].NameCourse = modelTemp[j].RegisterDescription;
                //            }
                //        }

                //    }


                //    foreach (StudyExternalUpdateDTOResponse objRal in listaEstudiosExternos)
                //    {
                //        cell = new PdfPCell();
                //        cell.AddElement(new Paragraph(objRal.NameCourse, fuente));
                //        tblEstudiosExternos.AddCell(cell);

                //        cell = new PdfPCell();
                //        cell.AddElement(new Paragraph(objRal.OtherCourse, fuente));
                //        tblEstudiosExternos.AddCell(cell);

                //        cell = new PdfPCell();
                //        cell.AddElement(new Paragraph(objRal.InstitutionFromStudy, fuente));
                //        tblEstudiosExternos.AddCell(cell);

                //        cell = new PdfPCell();

                //        if (objRal.DateInitial == null || objRal.DateInitial == DateTime.MinValue)
                //        {
                //            oParagraph = new Paragraph("No Registrado", fuente);
                //        }
                //        else
                //        {
                //            oParagraph = new Paragraph(objRal.DateInitial.Value.ToString("dd/MM/yyyy"), fuente);
                //        }


                //        oParagraph.Alignment = Element.ALIGN_CENTER;
                //        cell.AddElement(oParagraph);
                //        tblEstudiosExternos.AddCell(cell);

                //        cell = new PdfPCell();

                //        if (objRal.DateEnd == null || objRal.DateEnd == DateTime.MinValue)
                //        {
                //            oParagraph = new Paragraph("No Registrado", fuente);
                //        }
                //        else
                //        {
                //            oParagraph = new Paragraph(objRal.DateEnd.Value.ToString("dd/MM/yyyy"), fuente);
                //        }

                //        oParagraph.Alignment = Element.ALIGN_CENTER;
                //        cell.AddElement(oParagraph);
                //        tblEstudiosExternos.AddCell(cell);

                //        cell = new PdfPCell();
                //        if (objRal.IndPromoted != "0")
                //        {

                //            oParagraph = new Paragraph(string.Format("{0}", "SI"), fuente);
                //            oParagraph.Alignment = Element.ALIGN_CENTER;
                //            cell.AddElement(oParagraph);
                //        }
                //        else
                //        {
                //            oParagraph = new Paragraph(string.Format("{0}", "NO"), fuente);
                //            oParagraph.Alignment = Element.ALIGN_CENTER;
                //            cell.AddElement(oParagraph);

                //        }
                //        tblEstudiosExternos.AddCell(cell);
                //    }
                //    document.Add(tblEstudiosExternos);
                //}
                //#endregion

                //document.Add(new Paragraph(" "));
                //document.Add(new Paragraph("Idiomas", Fuente1));
                //document.Add(new Paragraph(" "));

                //#region "Idiomas"
                //if (listaIdiomas.Count > 0)
                //{
                //    PdfPTable tblIdiomas = new PdfPTable(6) { WidthPercentage = 100 };
                //    tblIdiomas.SetWidths(new float[] { 20, 20, 18, 16, 16, 10 });

                //    cell = new PdfPCell();
                //    Paragraph oParagraph = new Paragraph("IDIOMA", fuenteN);
                //    oParagraph.Alignment = Element.ALIGN_CENTER;
                //    cell.AddElement(oParagraph);
                //    tblIdiomas.AddCell(cell);

                //    cell = new PdfPCell();
                //    oParagraph = new Paragraph("INSTITUCIÓN", fuenteN);
                //    oParagraph.Alignment = Element.ALIGN_CENTER;
                //    cell.AddElement(oParagraph);
                //    tblIdiomas.AddCell(cell);

                //    cell = new PdfPCell();
                //    oParagraph = new Paragraph("NIVEL CONVERSACIÓN", fuenteN);
                //    oParagraph.Alignment = Element.ALIGN_CENTER;
                //    cell.AddElement(oParagraph);
                //    tblIdiomas.AddCell(cell);

                //    cell = new PdfPCell();
                //    oParagraph = new Paragraph("NIVEL LECTURA", fuenteN);
                //    oParagraph.Alignment = Element.ALIGN_CENTER;
                //    cell.AddElement(oParagraph);
                //    tblIdiomas.AddCell(cell);

                //    cell = new PdfPCell();
                //    oParagraph = new Paragraph("NIVEL ESCRITURA", fuenteN);
                //    oParagraph.Alignment = Element.ALIGN_CENTER;
                //    cell.AddElement(oParagraph);
                //    tblIdiomas.AddCell(cell);

                //    cell = new PdfPCell();
                //    oParagraph = new Paragraph("¿CERTIFICACIÓN?", fuenteN);
                //    oParagraph.Alignment = Element.ALIGN_CENTER;
                //    cell.AddElement(oParagraph);
                //    tblIdiomas.AddCell(cell);

                //    foreach (LanguageUpdateDTOResponse objRal in listaIdiomas)
                //    {
                //        cell = new PdfPCell();
                //        cell.AddElement(new Paragraph(objRal.LanguageDescription, fuente));
                //        tblIdiomas.AddCell(cell);

                //        cell = new PdfPCell();
                //        cell.AddElement(new Paragraph(objRal.DscInstitution, fuente));
                //        tblIdiomas.AddCell(cell);

                //        cell = new PdfPCell();
                //        cell.AddElement(new Paragraph(objRal.LevelConversation, fuente));
                //        tblIdiomas.AddCell(cell);

                //        cell = new PdfPCell();
                //        cell.AddElement(new Paragraph(objRal.LevelReading, fuente));
                //        tblIdiomas.AddCell(cell);

                //        cell = new PdfPCell();
                //        cell.AddElement(new Paragraph(objRal.LevelWriting, fuente));
                //        tblIdiomas.AddCell(cell);

                //        cell = new PdfPCell();
                //        if (!string.IsNullOrEmpty(objRal.HaveCertificate) || objRal.HaveCertificate != "0")
                //        {

                //            oParagraph = new Paragraph(string.Format("{0}", "SI"), fuente);
                //            oParagraph.Alignment = Element.ALIGN_CENTER;
                //            cell.AddElement(oParagraph);
                //        }
                //        else
                //        {
                //            oParagraph = new Paragraph(string.Format("{0}", "NO"), fuente);
                //            oParagraph.Alignment = Element.ALIGN_CENTER;
                //            cell.AddElement(oParagraph);

                //        }
                //        tblIdiomas.AddCell(cell);

                //    }
                //    document.Add(tblIdiomas);
                //}
                //#endregion

                //document.Add(new Paragraph(" "));
                //document.Add(new Paragraph("III. Experiencia Interna y Externa", fuenteTit));
                //document.Add(new Paragraph(" "));

                //#region "Experiencia Otras Compañias"
                //if (listaExperiencia.Count > 0)
                //{
                //    PdfPTable tblExpOtras = new PdfPTable(5) { WidthPercentage = 100 };
                //    tblExpOtras.SetWidths(new float[] { 20, 25, 12, 13, 30 });

                //    cell = new PdfPCell();
                //    Paragraph oParagraph = new Paragraph("EMPRESA", fuenteN);
                //    oParagraph.Alignment = Element.ALIGN_CENTER;
                //    cell.AddElement(oParagraph);
                //    tblExpOtras.AddCell(cell);

                //    cell = new PdfPCell();
                //    oParagraph = new Paragraph("PUESTO DESEMPEÑADO", fuenteN);
                //    oParagraph.Alignment = Element.ALIGN_CENTER;
                //    cell.AddElement(oParagraph);
                //    tblExpOtras.AddCell(cell);

                //    cell = new PdfPCell();
                //    oParagraph = new Paragraph("FECHA INICIO", fuenteN);
                //    oParagraph.Alignment = Element.ALIGN_CENTER;
                //    cell.AddElement(oParagraph);
                //    tblExpOtras.AddCell(cell);

                //    cell = new PdfPCell();
                //    oParagraph = new Paragraph("FECHA FIN", fuenteN);
                //    oParagraph.Alignment = Element.ALIGN_CENTER;
                //    cell.AddElement(oParagraph);
                //    tblExpOtras.AddCell(cell);

                //    cell = new PdfPCell();
                //    oParagraph = new Paragraph("FUNCIONES", fuenteN);
                //    oParagraph.Alignment = Element.ALIGN_CENTER;
                //    cell.AddElement(oParagraph);
                //    tblExpOtras.AddCell(cell);

                //    foreach (CorporateNoExperienceUpdateDTOResponse objRal in listaExperiencia)
                //    {
                //        cell = new PdfPCell();
                //        cell.AddElement(new Paragraph(objRal.NameInstitution, fuente));
                //        tblExpOtras.AddCell(cell);

                //        cell = new PdfPCell();
                //        cell.AddElement(new Paragraph(objRal.PositionPlayedDescription, fuente));
                //        tblExpOtras.AddCell(cell);

                //        string sfecha = "";
                //        if (objRal.DateInitialPosition != null)
                //        {
                //            //sfecha = objRal.DateInitialPositionText;
                //            sfecha = (objRal.DateInitialPosition.ToString()).Substring(0, 10);
                //            if (sfecha == null || sfecha.Contains("01/01/1900")) sfecha = "";
                //        }
                //        cell = new PdfPCell();
                //        oParagraph = new Paragraph(sfecha, fuente);
                //        oParagraph.Alignment = Element.ALIGN_CENTER;
                //        cell.AddElement(oParagraph);
                //        tblExpOtras.AddCell(cell);

                //        sfecha = "";
                //        if (objRal.DateEndPosition != null)
                //        {
                //            //sfecha = objRal.DateEndPositionText;
                //            sfecha = (objRal.DateEndPosition.ToString()).Substring(0, 10);
                //            if (sfecha == null || sfecha.Contains("01/01/1900")) sfecha = "";
                //        }
                //        cell = new PdfPCell();
                //        oParagraph = new Paragraph(sfecha, fuente);
                //        oParagraph.Alignment = Element.ALIGN_CENTER;
                //        cell.AddElement(oParagraph);
                //        tblExpOtras.AddCell(cell);

                //        cell = new PdfPCell();
                //        cell.AddElement(new Paragraph(objRal.FunctionDesem, fuente));
                //        tblExpOtras.AddCell(cell);
                //    }
                //    document.Add(tblExpOtras);
                //}
                //#endregion

                document.Add(new Paragraph(" "));
                document.Add(new Paragraph(" "));

                var texto = "Documento generado automaricamente por sistema Fibysoft";
                Paragraph p2 = new Paragraph(texto, fuentePie);
                p2.Alignment = Element.ALIGN_LEFT;
                document.Add(p2);

                document.Add(new Paragraph(" "));
                document.Add(new Paragraph(" "));

                PdfPTable tableFirma = new PdfPTable(3) { WidthPercentage = 100 };
                cell = new PdfPCell(new Phrase("", new Font(Font.NORMAL, 12f, Font.BOLD)));
                tableFirma.SetWidths(new float[] { 35, 30, 35 });

                cell = new PdfPCell(); cell.BorderColor = BaseColor.White;
                cell.Border = 0;
                tableFirma.AddCell(cell);

                //cell = new PdfPCell();
                //Paragraph parrafo = new Paragraph("_________________________________", fuente);
                //parrafo.Alignment = Element.ALIGN_CENTER;
                //cell.AddElement(parrafo);
                //cell.Border = 0;
                //tableFirma.AddCell(cell);

                cell = new PdfPCell(); cell.BorderColor = BaseColor.White;
                cell.Border = 0;
                tableFirma.AddCell(cell);

                //
                cell = new PdfPCell(); cell.BorderColor = BaseColor.White;
                cell.Border = 0;
                tableFirma.AddCell(cell);

                //cell = new PdfPCell();
                ////parrafo = new Paragraph(string.Format("{0} {1} {2}", worker.Name, worker.ParentName, worker.MaternalName), fuente);
                //parrafo = new Paragraph(string.Format("{0} {1} {2}", "HECTOR", "MORON", "PAIVA"), fuente);
                //parrafo.Alignment = Element.ALIGN_CENTER;
                //cell.AddElement(parrafo);
                //cell.Border = 0;
                //tableFirma.AddCell(cell);

                cell = new PdfPCell(); cell.BorderColor = BaseColor.White;
                cell.Border = 0;
                tableFirma.AddCell(cell);

                //
                cell = new PdfPCell(); cell.BorderColor = BaseColor.White;
                cell.Border = 0;
                tableFirma.AddCell(cell);

                //cell = new PdfPCell();
                ////parrafo = new Paragraph(string.Format("{0} {1} {2}", "DNI", "N°", worker.DocumentNumber), fuente);
                //parrafo = new Paragraph(string.Format("{0} {1} {2}", "DNI", "N°", "76692170"), fuente);
                //parrafo.Alignment = Element.ALIGN_CENTER;
                //cell.AddElement(parrafo);
                //cell.Border = 0;
                //tableFirma.AddCell(cell);

                cell = new PdfPCell(); cell.BorderColor = BaseColor.White;
                cell.Border = 0;
                tableFirma.AddCell(cell);

                document.Add(tableFirma);

                document.Close();

            }
            catch (Exception ex)
            {
                //Varias.EscribeLog(ex);
            }

            return no_pdf;
        }
    }
}
