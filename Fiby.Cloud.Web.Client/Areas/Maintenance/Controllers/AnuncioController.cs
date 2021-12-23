using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Fiby.Cloud.Web.Client.Extensions;
using Fiby.Cloud.Web.DTO.Modules.Reportes.Request;
using Fiby.Cloud.Web.DTO.Modules.Reportes.Response;
using Fiby.Cloud.Web.Service.Interfaces.Maintenance;
using Fiby.Cloud.Web.Util.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiby.Cloud.Web.Client.Areas.Maintenance.Controllers
{
    [Authorize]
    [Area("Maintenance")]
    public class AnuncioController : Controller
    {
        private readonly IAnuncioService _anuncioService;

        public AnuncioController(IAnuncioService anuncioService)
        {
            _anuncioService = anuncioService;
        }

        public IActionResult Index()
        {
            if (User.Identity.GetProfileId() != "1")
            {
                return RedirectToAction("Logout", "Account", new { Area = "" });
            }

            return View();
        }

        [HttpPost]
        public async Task<string> RegistrarAnuncio(AnuncioDTORequest anuncioDTORequest)
        {
            string resultado = string.Empty;
            try
            {
                anuncioDTORequest.FechaInicio = General.ConvertFormatDateTime(anuncioDTORequest.FechaInicioText);
                anuncioDTORequest.FechaFin = General.ConvertFormatDateTime(anuncioDTORequest.FechaFinText);
                resultado = await _anuncioService.RegistrarAnuncio(anuncioDTORequest);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }

            return resultado;
        }

        [HttpPost]
        public async Task<JsonResult> BuscarAnuncios(AnuncioDTORequest anuncioDTORequest)
        {
            var model = await _anuncioService.GetAnuncioAll(anuncioDTORequest);
            return Json(model);
        }

        [HttpDelete]
        public async Task<string> EliminarAnuncio(int idAnuncio)
        {

            var anuncioDTORequest = new AnuncioDTORequest
            {
                IdAnuncio = idAnuncio,
            };

            string response = await _anuncioService.EliminarAnuncio(anuncioDTORequest);
            return response;
        }

        public async Task<IActionResult> Actualizar(int idAnuncio, int option)
        {
            AnuncioDTOResponse anuncioDTOResponse = new AnuncioDTOResponse();
            AnuncioDTORequest anuncioDTORequest = new AnuncioDTORequest()
            {
                IdAnuncio = idAnuncio
            };

            if (idAnuncio == 0) { ViewBag.titleModal = "Agregar"; }
            if (idAnuncio > 0 && option == 0) { ViewBag.titleModal = "Editar"; }

            if (idAnuncio > 0)//EDITAR
            {
                anuncioDTOResponse = await _anuncioService.GetAnuncioPorCodigo(anuncioDTORequest);
                ViewBag.ModalGeneralIsNew = "0";
            }
            else
            {
                ViewBag.ModalGeneralIsNew = "1";
            }

            return PartialView(anuncioDTOResponse);
        }

        [HttpPost]
        public async Task<JsonResult> BuscarClientesCodio(int codigo)
        {
            AnuncioDTORequest request = new AnuncioDTORequest();
            request.IdAnuncio = codigo;
            var model = await _anuncioService.GetAnuncioPorCodigo(request);
            return Json(model);
        }
    }
}
