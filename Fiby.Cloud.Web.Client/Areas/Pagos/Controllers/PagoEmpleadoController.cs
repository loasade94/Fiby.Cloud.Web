using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Fiby.Cloud.Web.DTO.Modules.Horario.Request;
using Fiby.Cloud.Web.DTO.Modules.Horario.Response;
using Fiby.Cloud.Web.DTO.Modules.Pagos.Request;
using Fiby.Cloud.Web.Service.Interfaces;
using Fiby.Cloud.Web.Service.Interfaces.Horario;
using Fiby.Cloud.Web.Service.Interfaces.Pagos;
using Microsoft.AspNetCore.Mvc;

namespace Fiby.Cloud.Web.Client.Areas.Pagos.Controllers
{
    [Area("Pagos")]
    public class PagoEmpleadoController : Controller
    {

        private readonly IEmpleadoService _empleadoService;
        private readonly ISemanaService _semanaService;
        private readonly IPagoEmpleadoService _pagoEmpleadoService;
        private readonly ICalendarioService _calendarioService;

        public PagoEmpleadoController(IEmpleadoService empleadoService,
                                        ISemanaService semanaService,
                                        IPagoEmpleadoService pagoEmpleadoService,
                                        ICalendarioService calendarioService)
        {
            _empleadoService = empleadoService;
            _semanaService = semanaService;
            _pagoEmpleadoService = pagoEmpleadoService;
            _calendarioService = calendarioService;
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
    }
}
