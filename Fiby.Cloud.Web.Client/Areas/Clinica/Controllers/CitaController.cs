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
    public class CitaController : Controller
    {

        private readonly ICitaService _clienteService;
        private readonly IPacienteService _pacienteService;
        private readonly ITablaDetalleService _tablaDetalleService;
        private readonly IDoctorService _doctorService;

        public CitaController(ICitaService clienteService,
                                IPacienteService pacienteService,
                                ITablaDetalleService tablaDetalleService,
                                IDoctorService doctorService)
        {
            _clienteService = clienteService;
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

            var model = await _clienteService.GetCitaAll(citaDTORequest);

            return View(model);
        }

        public async Task<IActionResult> RegisterUpdate(int idCita)
        {
            var company = User.Identity.CompanyId();

            CitaDTOResponse obj = new CitaDTOResponse();
            CitaDTORequest CitaDTORequest = new CitaDTORequest()
            {
                IdCita = idCita
            };

            if (idCita == 0) { ViewBag.titleModal = "Agregar"; }
            if (idCita > 0) { ViewBag.titleModal = "Editar"; }

            ViewBag.ListaEspecialidad = await _tablaDetalleService.GetTablaDetalleAll(new TablaDetalleDTORequest() { CodigoTabla = "ARME" } );

            if (idCita > 0)//EDITAR
            {
                ViewBag.ModalGeneralIsNew = "0";
                //obj = await _CitaService.GetCitaById(CitaDTORequest, token);
            }
            else
            {
                ViewBag.ModalGeneralIsNew = "1";
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
    }
}
