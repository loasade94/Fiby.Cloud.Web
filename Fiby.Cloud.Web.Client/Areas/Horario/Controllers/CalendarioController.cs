using Fiby.Cloud.Web.Client.Extensions;
using Fiby.Cloud.Web.DTO.Modules.Horario.Request;
using Fiby.Cloud.Web.DTO.Modules.Horario.Response;
using Fiby.Cloud.Web.Service.Interfaces;
using Fiby.Cloud.Web.Service.Interfaces.Horario;
using Fiby.Cloud.Web.Util.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Client.Areas.Horario.Controllers
{
    [Authorize]
    [Area("Horario")]
    public class CalendarioController : Controller
    {

        private readonly IEmpleadoService _empleadoService;
        private readonly ICalendarioService _calendarioService;
        private readonly ISemanaService _semanaService;

        public CalendarioController(IEmpleadoService empleadoService,
                                    ICalendarioService calendarioService,
                                    ISemanaService semanaService)
        {
            _empleadoService = empleadoService;
            _calendarioService = calendarioService;
            _semanaService = semanaService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ListaEmpleados = await _empleadoService.GetEmpleadoAll();
            ViewBag.ListaHorario = await _semanaService.GetListaHorario();
            return View();
        }

        [HttpPost]
        public async Task<string> RegistrarServicio(CalendarioDTORequest calendarioDTORequest)
        {
            string resultado = string.Empty;
            calendarioDTORequest.UsuarioCreacion = 1;
            try
            {
                //calendarioDTORequest.Fecha = DateTime.Parse(calendarioDTORequest.FechaText);
                calendarioDTORequest.Fecha = General.ConvertFormatDateTime(calendarioDTORequest.FechaText);
                resultado = await _calendarioService.RegistrarServicio(calendarioDTORequest);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }

            return resultado;
        }

        [HttpPost]
        public async Task<JsonResult> BuscarServicioXEmpleado(CalendarioDTORequest calendarioDTORequest, int indicadorInicio)
        {
            calendarioDTORequest.Fecha = General.ConvertFormatDateTime(calendarioDTORequest.FechaText);
            var model = await _calendarioService.GetServicioXEmpleado(calendarioDTORequest);
            return Json(model);
        }

        [HttpPost]
        public async Task<JsonResult> BuscarServicioXEmpleadoCalendario(CalendarioDTORequest calendarioDTORequest, int indicadorInicio)
        {
            var model = await _calendarioService.GetServicioXEmpleadoCalendario(calendarioDTORequest);
            return Json(model);
        }

        [HttpPost]
        public async Task<JsonResult> BuscarServicioXEmpleadoTotales(CalendarioDTORequest calendarioDTORequest, int indicadorInicio)
        {
            calendarioDTORequest.Fecha = General.ConvertFormatDateTime(calendarioDTORequest.FechaText);
            var model = await _calendarioService.GetServicioXEmpleadoTotales(calendarioDTORequest);
            return Json(model);
        }

        [HttpDelete]
        public async Task<string> EliminarServicio(int idServicio)
        {

            var calendarioDTORequest = new CalendarioDTORequest
            {
                IdServicio = idServicio,
            };

            string response = await _calendarioService.EliminarServicio(calendarioDTORequest);
            return response;
        }

        public async Task<IActionResult> Actualizar(int idServicio, int option)
        {
            CalendarioDTOResponse calendarioDTOResponse = new CalendarioDTOResponse();
            CalendarioDTORequest calendarioDTORequest = new CalendarioDTORequest()
            {
                IdServicio = idServicio
            };

            if (idServicio == 0) { ViewBag.titleModal = "Agregar"; }
            if (idServicio > 0 && option == 0) { ViewBag.titleModal = "Editar"; }

            if (idServicio > 0)//EDITAR
            {
                calendarioDTOResponse = await _calendarioService.GetCalendarioById(calendarioDTORequest);
                ViewBag.ModalGeneralIsNew = "0";
            }
            else
            {
                ViewBag.ModalGeneralIsNew = "1";
            }

            ViewBag.ListaHorario = await _semanaService.GetListaHorario();

            return PartialView(calendarioDTOResponse);
        }

        public string CalcularHoraFin(DateTime? fecha,string horaInicio, int horas)
        {
            var fechaFormato = fecha.Value.ToString("yyyy-MM-dd");
            DateTime? myDate = DateTime.ParseExact(fechaFormato + " "+ horaInicio + ":00,000", "yyyy-MM-dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture);

            var DateHoraFinal = myDate.Value.AddHours(horas);

            var horaFinal = DateHoraFinal.ToString("HH:mm");

            return horaFinal;
        }
    }
}
