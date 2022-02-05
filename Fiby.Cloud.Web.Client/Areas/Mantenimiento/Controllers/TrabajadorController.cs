using Fiby.Cloud.Web.Client.Extensions;
using Fiby.Cloud.Web.DTO.Modules.Mantenimiento.Request;
using Fiby.Cloud.Web.DTO.Modules.Mantenimiento.Response;
using Fiby.Cloud.Web.DTO.Modules.Parametro.Request;
using Fiby.Cloud.Web.Service.Interfaces.Mantenimiento;
using Fiby.Cloud.Web.Service.Interfaces.Parametro;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Client.Areas.Mantenimiento.Controllers
{
    [Authorize]
    [Area("Mantenimiento")]
    public class TrabajadorController : Controller
    {
        private readonly ITrabajadorService _trabajadorService;
        private readonly ITablaDetalleService _tablaDetalleService;

        public TrabajadorController(ITrabajadorService trabajadorService,
                                    ITablaDetalleService tablaDetalleService)
        {
            _trabajadorService = trabajadorService;
            _tablaDetalleService = tablaDetalleService;
        }

        public async Task<IActionResult> Index()
        {

            if (User.Identity.GetProfileId() == "2")
            {
                ViewBag.Layout = "~/Views/Shared/_LayoutEmpleado.cshtml";
            }
            else
            {
                ViewBag.Layout = "~/Views/Shared/_LayoutAdministrator.cshtml";
            }

            ViewBag.NombreSesion = User.Identity.GetNombre();
            ViewBag.IdPerfil = User.Identity.GetProfileId();

            TrabajadorDTORequest trabajadorDTORequest = new TrabajadorDTORequest();

            var model = await _trabajadorService.GetTrabajadorAll(trabajadorDTORequest);

            return View(model);
        }

        public async Task<IActionResult> RegisterUpdate(int idTrabajador)
        {

            TrabajadorDTOResponse obj = new TrabajadorDTOResponse();
            TrabajadorDTORequest trabajadorDTORequest = new TrabajadorDTORequest()
            {
                CodigoTrabajador = idTrabajador
            };

            if (idTrabajador == 0) { ViewBag.titleModal = "Agregar"; }
            if (idTrabajador > 0) { ViewBag.titleModal = "Editar"; }

            ViewBag.ListaSexo = await _tablaDetalleService.GetTablaDetalleAll(new TablaDetalleDTORequest() { CodigoTabla = "GEGE" });


            if (idTrabajador > 0)//EDITAR
            {
                ViewBag.ModalGeneralIsNew = "0";
                obj = await _trabajadorService.GetTrabajadorPorId(trabajadorDTORequest);
            }
            else
            {
                ViewBag.ModalGeneralIsNew = "1";
            }
            return PartialView(obj);
        }

        [HttpPost]
        public async Task<string> GuardarTrabajador(TrabajadorDTORequest trabajadorDTORequest)
        {
            string resultado = string.Empty;
            try
            {
                resultado = await _trabajadorService.GuardarTrabajador(trabajadorDTORequest);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }

            return resultado;
        }

        [HttpPost]
        public async Task<string> EditarTrabajador(TrabajadorDTORequest trabajadorDTORequest)
        {
            string resultado = string.Empty;
            try
            {
                resultado = await _trabajadorService.EditarTrabajador(trabajadorDTORequest);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }

            return resultado;
        }

        [HttpPost]
        public async Task<IActionResult> GetTrabajadorAll(TrabajadorDTORequest trabajadorDTORequest)
        {
            var model = await _trabajadorService.GetTrabajadorAll(trabajadorDTORequest);
            return View("Grid", model);
        }

        [HttpPost]
        public async Task<TrabajadorDTOResponse> GetTrabajadorPorId(TrabajadorDTORequest trabajadorDTORequest)
        {
            var model = await _trabajadorService.GetTrabajadorPorId(trabajadorDTORequest);
            return model;
        }

        [HttpDelete]
        public async Task<string> EliminarTrabajador(int idTrabajador)
        {

            var trabajadorDTORequest = new TrabajadorDTORequest
            {
                CodigoTrabajador = idTrabajador,
            };

            string response = await _trabajadorService.EliminarTrabajador(trabajadorDTORequest);
            return response;
        }
    }
}
