using Fiby.Cloud.Web.Client.Extensions;
using Fiby.Cloud.Web.DTO.Modules.Clinica.Request;
using Fiby.Cloud.Web.DTO.Modules.Clinica.Response;
using Fiby.Cloud.Web.DTO.Modules.Parametro.Request;
using Fiby.Cloud.Web.Service.Interfaces.Clinica;
using Fiby.Cloud.Web.Service.Interfaces.Parametro;
using Fiby.Cloud.Web.Util.Utility;
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
    public class CitaController : Controller
    {

        private readonly ICitaService _citaService;
        private readonly IPacienteService _pacienteService;
        private readonly ITablaDetalleService _tablaDetalleService;
        private readonly IDoctorService _doctorService;

        public CitaController(ICitaService citaService,
                                IPacienteService pacienteService,
                                ITablaDetalleService tablaDetalleService,
                                IDoctorService doctorService)
        {
            _citaService = citaService;
            _pacienteService = pacienteService;
            _tablaDetalleService = tablaDetalleService;
            _doctorService = doctorService;
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

            CitaDTORequest citaDTORequest = new CitaDTORequest();

            var model = await _citaService.GetCitaAll(citaDTORequest);

            return View(model);
        }

        public async Task<IActionResult> RegisterUpdate(int idCita)
        {

            CitaDTOResponse obj = new CitaDTOResponse();
            CitaDTORequest citaDTORequest = new CitaDTORequest()
            {
                IdCita = idCita
            };

            if (idCita == 0) { ViewBag.titleModal = "Agregar"; }
            if (idCita > 0) { ViewBag.titleModal = "Editar"; }

            ViewBag.ListaEspecialidad = await _tablaDetalleService.GetTablaDetalleAll(new TablaDetalleDTORequest() { CodigoTabla = "ARME" } );

            if (idCita > 0)//EDITAR
            {
                ViewBag.ModalGeneralIsNew = "0";
                obj = await _citaService.GetCitaPorId(citaDTORequest);

                ViewBag.ListaMedicos = await _doctorService.GetDoctorPorEspecialidad(new DoctorDTORequest() { CodigoEspecialidad = obj.CodigoEspecialidad });

            }
            else
            {
                ViewBag.ModalGeneralIsNew = "1";
                ViewBag.ListaMedicos = new List<DoctorDTOResponse>();
            }
            return PartialView(obj);
        }

        [HttpPost]
        public async Task<PacienteDTOResponse> GetPacientePorDocumento(PacienteDTORequest pacienteDTORequest)
        {
            var model = await _pacienteService.GetPacientePorDocumento(pacienteDTORequest);
            return model;
        }

        [HttpPost]
        public async Task<List<DoctorDTOResponse>> GetDoctorPorEspecialidad(DoctorDTORequest doctorDTORequest)
        {
            var model = await _doctorService.GetDoctorPorEspecialidad(doctorDTORequest);
            return model;
        }

        [HttpPost]
        public async Task<string> GuardarCita(CitaDTORequest citaDTORequest)
        {
            string resultado = string.Empty;
            try
            {
                citaDTORequest.FechaCita = General.ConvertFormatDateTime(citaDTORequest.FechaCitaText);
                //citaDTORequest.FechaCita = citaDTORequest.FechaCitaText;
                resultado = await _citaService.GuardarCita(citaDTORequest);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }

            return resultado;
        }

        [HttpPost]
        public async Task<string> EditarCita(CitaDTORequest citaDTORequest)
        {
            string resultado = string.Empty;
            try
            {
                citaDTORequest.FechaCita = General.ConvertFormatDateTime(citaDTORequest.FechaCitaText);
                resultado = await _citaService.EditarCita(citaDTORequest);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }

            return resultado;
        }

        [HttpPost]
        public async Task<IActionResult> GetCitaAll(CitaDTORequest citaDTORequest)
        {
            var model = await _citaService.GetCitaAll(citaDTORequest);
            return View("Grid",model);
        }

        [HttpPost]
        public async Task<CitaDTOResponse> GetCitaPorId(CitaDTORequest citaDTORequest)
        {
            var model = await _citaService.GetCitaPorId(citaDTORequest);
            return model;
        }

        [HttpDelete]
        public async Task<string> EliminarCita(int idCita)
        {

            var citaDTORequest = new CitaDTORequest
            {
                IdCita = idCita,
            };

            string response = await _citaService.EliminarCita(citaDTORequest);
            return response;
        }
    }
}
