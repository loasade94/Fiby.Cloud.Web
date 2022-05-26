using Fiby.Cloud.Web.Client.Extensions;
using Fiby.Cloud.Web.DTO.Modules.Facturacion.Request;
using Fiby.Cloud.Web.DTO.Modules.Facturacion.Response;
using Fiby.Cloud.Web.Service.Interfaces.Facturacion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

            var model = await _cargaFacturaService.ConsultaFacturas(new CargaFacturaDTORequest() { 
            
            
            });

            return View(model);
        }

        [HttpPost]
        public async Task<List<string>> RegistrarCargaFactura(CargaFacturaDTORequest cargaFacturaDTORequest)
        {
            var resultado = new List<string>();
            try
            {
                //cargaFacturaDTORequest.Mes = cargaFacturaDTORequest.Mes == null ? string.Empty : cargaFacturaDTORequest.Mes;
                resultado = await _cargaFacturaService.RegistrarFactura(cargaFacturaDTORequest);
            }
            catch (Exception ex)
            {
                //resultado = ex.Message;
                var error = ex.Message;
                resultado.Add(error);
            }

            return resultado;
        }

        //[HttpPost]
        //public async Task<JsonResult> ConsultaFacturas(CargaFacturaDTORequest cargaFacturaDTORequest)
        //{
        //    cargaFacturaDTORequest.Mes = cargaFacturaDTORequest.Mes == null ? string.Empty : cargaFacturaDTORequest.Mes;
        //    var model = await _cargaFacturaService.ConsultaFacturas(cargaFacturaDTORequest);
        //    return Json(model);
        //}

        [HttpPost]
        public async Task<ActionResult> ConsultaFacturas(CargaFacturaDTORequest cargaFacturaDTORequest)
        {
            cargaFacturaDTORequest.Mes = cargaFacturaDTORequest.Mes == null ? string.Empty : cargaFacturaDTORequest.Mes;
            var model = await _cargaFacturaService.ConsultaFacturas(cargaFacturaDTORequest);
            return View("Grid", model);
        }
    }
}
