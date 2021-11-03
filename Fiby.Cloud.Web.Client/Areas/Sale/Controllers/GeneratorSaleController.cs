using Fiby.Cloud.Web.DTO.Modules.Ple.Request;
using Fiby.Cloud.Web.DTO.Modules.User.Request;
using Fiby.Cloud.Web.DTO.Modules.User.Response;
using Fiby.Cloud.Web.Service.Modules.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiby.Cloud.Web.Client.Extensions;
using Fiby.Cloud.Web.DTO.Modules.Ple.Response;
using Fiby.Cloud.Web.Service.Modules.Data.Interfaces.Sale;
using Fiby.Cloud.Web.DTO.Modules.Sale.Request.Serie;
using Fiby.Cloud.Web.Client.Extensions.Sunat;
using System.Xml.Serialization;
using System.Xml;

namespace Fiby.Cloud.Web.Client.Areas.Sale.Controllers
{
    [Area("Sale")]
    public class GeneratorSaleController : Controller
    {

        private readonly IPleService _pleService;
        private readonly ISerieService _serieService;

        public GeneratorSaleController(IPleService pleService,
                                        ISerieService serieService)
        {
            _pleService = pleService;
            _serieService = serieService;
        }

        public async Task<IActionResult> Index()
        {
            SerieDTORequest serieDTORequest = new SerieDTORequest();
            serieDTORequest.Description = serieDTORequest.Description == null ? string.Empty : serieDTORequest.Description;

            ViewBag.listaSerie = await _serieService.GetSerieAll(serieDTORequest);

            return View();
        }

        //[HttpPost]
        public async Task<FileResult> GetPleAll(string mes)
        {
            //pleDTORequest.SequenceCUO = pleDTORequest.Description == null ? string.Empty : pleDTORequest.Description;

            TextWriter tw = new StreamWriter(Path.Combine(Path.GetTempPath(), "plantilla.txt"), true);

            //var model = await _pleService.GetPleAll(pleDTORequest);

            PLE14100DTORequest pLE14100DTORequest = new PLE14100DTORequest();
            pLE14100DTORequest.CODIGO = 0;
            pLE14100DTORequest.MesLista = "01/" + mes + "/2021";

            var model = await _pleService.GetPleAllNew(pLE14100DTORequest);

            for (int i = 0; i < model.Count; i++)
            {
                tw.WriteLine(model[i].Line);
            }

            tw.Close();

            //return File(Path.Combine(Path.GetTempPath(), "plantilla.txt"), "text/plain");

            byte[] byteArray = System.IO.File.ReadAllBytes(Path.Combine(Path.GetTempPath(), "plantilla.txt"));

            //var reportName = "Plev1";

            //Response.Headers.Add("Content-Disposition", String.Format("attachment; filename={0}", $"{reportName}_{DateTime.Now.ToString()}.txt"));
            //return new FileContentResult(byteArray, "text/plain");

            //String text = "Hola mundo" + Environment.NewLine + "Hoy es Martes";
            //byte[] result = Encoding.ASCII.GetBytes(text);
            return File(byteArray, "text/plain", "ple.txt");
        }


        [HttpPost]
        public async Task<string> RegistrarPle14100DPorMes(PLE14100DTORequest pLE14100DTORequest)
        {
            string resultado = string.Empty;
            pLE14100DTORequest.IdEmpresa = Int16.Parse(User.Identity.CompanyId());
            try
            {
                resultado = await _pleService.RegistrarPle14100DPorMes(pLE14100DTORequest);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }

            return resultado;
        }

        [HttpPost]
        public async Task<JsonResult> BuscarPle14100DPorMes(PLE14100DTORequest pLE14100DTORequest, int indicadorInicio)
        {
            pLE14100DTORequest.IdEmpresa = Int16.Parse(User.Identity.CompanyId());

            if (indicadorInicio == 1)
            {
                pLE14100DTORequest.MesLista = DateTime.Now.ToString("dd/MM/yyyy");
            }

            var model = await _pleService.GetPlePLE14100All(pLE14100DTORequest);
            return Json(model);
        }

        [HttpPost]
        public async Task<string> GenerarXML()
        {
            string resultado = string.Empty;

            InvoiceType Factura = new InvoiceType();

            //CABECERA XML
            Factura.Cac = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2";
            Factura.Cbc = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2";
            Factura.Ccts = "urn:un:unece:uncefact:documentation:2";
            Factura.Ds = "http://www.w3.org/2000/09/xmldsig#";
            Factura.Ext = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2";
            Factura.Qdt = "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2";
            Factura.Udt = "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2";
            UBLExtensionType[] uBLExtensions = new UBLExtensionType[11];
            UBLExtensionType uBLExtension = new UBLExtensionType();
            uBLExtensions[0] = uBLExtension;
            Factura.UBLExtensions = uBLExtensions;


            //VERSIONES UBL
            Factura.UBLVersionID =  new UBLVersionIDType();
            Factura.UBLVersionID.Value = "2.1";
            Factura.CustomizationID = new CustomizationIDType();
            Factura.CustomizationID.Value = "2.0";


            //SERIE Y NUMERO

            Factura.ID = new IDType();
            Factura.ID.Value = "F001" + "-" + "00000001";

            //GENERAR XML
            //XmlSerializer xmlSerializer = new XmlSerializer(typeof(InvoiceType));
            //var xmlEscribir = new StringWriter();
            //xmlSerializer.Serialize(XmlWriter.Create(xmlEscribir),Factura);
            //string stringXml = xmlEscribir.ToString();
            //System.IO.File.WriteAllText("Xmlfactura.xml", stringXml);

            XmlWriterSettings propiedades = new XmlWriterSettings();
            propiedades.Indent = true;
            propiedades.IndentChars = "\t";
            string rutaxml = @"C:\Users\hecto\source\repos\Fiby.Cloud.Web\Fiby.Cloud.Web.Client\factura.xml";
            using (XmlWriter escribir = XmlWriter.Create(rutaxml,propiedades))
            {
                Type serializacion = typeof(InvoiceType);
                XmlSerializer crearxml = new XmlSerializer(serializacion);
                crearxml.Serialize(escribir, Factura);

            }

            return resultado;
        }


    }
}
