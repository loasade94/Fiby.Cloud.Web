using Fiby.Cloud.Web.Client.Extensions;
using Fiby.Cloud.Web.DTO.Modules.Clinica.Request;
using Fiby.Cloud.Web.DTO.Modules.Clinica.Response;
using Fiby.Cloud.Web.DTO.Modules.Parametro.Request;
using Fiby.Cloud.Web.Service.Interfaces.Clinica;
using Fiby.Cloud.Web.Service.Interfaces.Parametro;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Client.Areas.Clinica.Controllers
{
    [Authorize]
    [Area("Clinica")]
    public class PacienteController : Controller
    {

        private readonly IPacienteService _pacienteService;
        private readonly ITablaDetalleService _tablaDetalleService;

        public PacienteController(IPacienteService pacienteService,
                                    ITablaDetalleService tablaDetalleService)
        {
            _pacienteService = pacienteService;
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

            PacienteDTORequest pacienteDTORequest = new PacienteDTORequest();

            var model = await _pacienteService.GetPacienteAll(pacienteDTORequest);

            return View(model);
        }

        public async Task<IActionResult> RegisterUpdate(int idPaciente)
        {

            PacienteDTOResponse obj = new PacienteDTOResponse();
            PacienteDTORequest pacienteDTORequest = new PacienteDTORequest()
            {
                CodigoPaciente = idPaciente
            };

            if (idPaciente == 0) { ViewBag.titleModal = "Agregar"; }
            if (idPaciente > 0) { ViewBag.titleModal = "Editar"; }

            ViewBag.ListaSexo = await _tablaDetalleService.GetTablaDetalleAll(new TablaDetalleDTORequest() { CodigoTabla = "GEGE" });


            if (idPaciente > 0)//EDITAR
            {
                ViewBag.ModalGeneralIsNew = "0";
                obj = await _pacienteService.GetPacientePorId(pacienteDTORequest);
            }
            else
            {
                ViewBag.ModalGeneralIsNew = "1";
            }
            return PartialView(obj);
        }

        [HttpPost]
        public async Task<string> GuardarPaciente(PacienteDTORequest pacienteDTORequest)
        {
            string resultado = string.Empty;
            try
            {
                resultado = await _pacienteService.GuardarPaciente(pacienteDTORequest);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }

            return resultado;
        }

        [HttpPost]
        public async Task<string> EditarPaciente(PacienteDTORequest pacienteDTORequest)
        {
            string resultado = string.Empty;
            try
            {
                resultado = await _pacienteService.EditarPaciente(pacienteDTORequest);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }

            return resultado;
        }

        [HttpPost]
        public async Task<IActionResult> GetPacienteAll(PacienteDTORequest pacienteDTORequest)
        {
            var model = await _pacienteService.GetPacienteAll(pacienteDTORequest);
            return View("Grid", model);
        }

        [HttpPost]
        public async Task<PacienteDTOResponse> GetPacientePorId(PacienteDTORequest pacienteDTORequest)
        {
            var model = await _pacienteService.GetPacientePorId(pacienteDTORequest);
            return model;
        }

        [HttpDelete]
        public async Task<string> EliminarPaciente(int idPaciente)
        {

            var pacienteDTORequest = new PacienteDTORequest
            {
                CodigoPaciente = idPaciente,
            };

            string response = await _pacienteService.EliminarPaciente(pacienteDTORequest);
            return response;
        }

    }
}
