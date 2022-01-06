using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fiby.Cloud.Web.Client.Extensions;
using Fiby.Cloud.Web.DTO.Modules.Horario.Request;
using Fiby.Cloud.Web.Service.Interfaces;
using Fiby.Cloud.Web.Service.Interfaces.Horario;
using Fiby.Cloud.Web.Service.Interfaces.Maintenance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiby.Cloud.Web.Client.Areas.Horario.Controllers
{
    [Authorize]
    [Area("Horario")]
    public class DisponibilidadController : Controller
    {

        private readonly ISemanaService _semanaService;
        private readonly IEmpleadoService _empleadoService;
        private readonly IClienteService _clienteService;
        public DisponibilidadController(ISemanaService semanaService,
                                        IEmpleadoService empleadoService,
                                        IClienteService clienteService)
        {
            _semanaService = semanaService;
            _empleadoService = empleadoService;
            _clienteService = clienteService;
        }

        public async Task<IActionResult> Index()
        {

            if (User.Identity.GetProfileId() != "1")
            {
                return RedirectToAction("Logout", "Account", new { Area = "" });
            }

            var listaSemana = await _semanaService.GetListaSemana();

            var semanaDTORequest = new SemanaDTORequest();
            semanaDTORequest.IdSemana = listaSemana.Where(x => x.Prioridad == 1).FirstOrDefault().IdSemana;

            ViewBag.ListaDiasXSemana = await _semanaService.GetListaDiasXSemana(semanaDTORequest);

            ViewBag.ListaSemana = listaSemana;

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetDisponibilidadSemana(SemanaDTORequest semanaDTORequest)
        {
            var model = await _semanaService.GetDisponibilidadSemana(semanaDTORequest);
            return Json(model);
        }

        [HttpPost]
        public async Task<JsonResult> GetListaDiasXSemana(SemanaDTORequest semanaDTORequest)
        {
            var model = await _semanaService.GetListaDiasXSemana(semanaDTORequest);
            return Json(model);
        }

        public async Task<IActionResult> AgregarServicio(string fecha)
        {
            ViewBag.ListaCliente = await _clienteService.GetClienteAll();
            ViewBag.ListaEmpleados = await _empleadoService.GetEmpleadoAll();
            ViewBag.ListaHorario = await _semanaService.GetListaHorario();
            ViewBag.Fecha = fecha;
            ViewBag.titleModal = "Registrar Servicio";


            return PartialView();
        }
    }
}
