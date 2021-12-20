using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Fiby.Cloud.Web.Client.Extensions;
using Fiby.Cloud.Web.DTO.Modules.Inversiones.Request;
using Fiby.Cloud.Web.DTO.Modules.Inversiones.Response;
using Fiby.Cloud.Web.Service.Interfaces.Horario;
using Fiby.Cloud.Web.Service.Interfaces.Inversiones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiby.Cloud.Web.Client.Areas.Inversiones.Controllers
{
    [Authorize]
    [Area("Inversiones")]
    public class GastoController : Controller
    {

        private readonly IGastoService _gastoService;
        private readonly ISemanaService _semanaService;

        public GastoController(IGastoService gastoService
                                ,ISemanaService semanaService)
        {
            _gastoService = gastoService;
            _semanaService = semanaService;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.GetProfileId() != "1")
            {
                return RedirectToAction("Logout", "Account", new { Area = "" });
            }

            var listaSemana = await _semanaService.GetListaSemana();

            ViewBag.ListaSemana = listaSemana;

            return View();
        }

        [HttpPost]
        public async Task<string> RegistrarGasto(GastoDTORequest gastoDTORequest)
        {
            string resultado = string.Empty;
            try
            {
                CultureInfo culture = new CultureInfo("en-US");
                var pago = Math.Round(decimal.Parse(gastoDTORequest.MontoText, culture), 2);
                gastoDTORequest.Monto = pago;
                gastoDTORequest.IdCategoria = 1;
                resultado = await _gastoService.RegistrarGasto(gastoDTORequest);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }

            return resultado;
        }

        [HttpPost]
        public async Task<JsonResult> BuscarGastos(GastoDTORequest gastoDTORequest)
        {
            var model = await _gastoService.GetGastoAll(gastoDTORequest);
            return Json(model);
        }

        [HttpDelete]
        public async Task<string> EliminarGasto(int idGasto)
        {

            var gastoDTORequest = new GastoDTORequest
            {
                IdGasto = idGasto,
            };

            string response = await _gastoService.EliminarGasto(gastoDTORequest);
            return response;
        }

        public async Task<IActionResult> Actualizar(int idGasto, int option)
        {
            GastoDTOResponse gastoDTOResponse = new GastoDTOResponse();
            GastoDTORequest gastoDTORequest = new GastoDTORequest()
            {
                IdGasto = idGasto
            };

            var listaSemana = await _semanaService.GetListaSemana();

            ViewBag.ListaSemana = listaSemana;

            if (idGasto == 0) { ViewBag.titleModal = "Agregar"; }
            if (idGasto > 0 && option == 0) { ViewBag.titleModal = "Editar"; }

            if (idGasto > 0)//EDITAR
            {
                gastoDTOResponse = await _gastoService.GetGastoPorCodigo(gastoDTORequest);
                ViewBag.ModalGeneralIsNew = "0";
            }
            else
            {
                ViewBag.ModalGeneralIsNew = "1";
            }

            return PartialView(gastoDTOResponse);
        }

        [HttpPost]
        public async Task<JsonResult> BuscarClientesCodio(int codigo)
        {
            GastoDTORequest request = new GastoDTORequest();
            request.IdGasto = codigo;
            var model = await _gastoService.GetGastoPorCodigo(request);
            return Json(model);
        }
    }
}
