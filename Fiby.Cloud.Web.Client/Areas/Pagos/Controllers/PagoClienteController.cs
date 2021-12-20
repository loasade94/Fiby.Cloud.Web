using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Fiby.Cloud.Web.Client.Extensions;
using Fiby.Cloud.Web.DTO.Modules.Horario.Request;
using Fiby.Cloud.Web.DTO.Modules.Horario.Response;
using Fiby.Cloud.Web.DTO.Modules.Pagos.Request;
using Fiby.Cloud.Web.Service.Interfaces;
using Fiby.Cloud.Web.Service.Interfaces.Horario;
using Fiby.Cloud.Web.Service.Interfaces.Pagos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiby.Cloud.Web.Client.Areas.Pagos.Controllers
{
    [Authorize]
    [Area("Pagos")]
    public class PagoClienteController : Controller
    {
        private readonly IEmpleadoService _empleadoService;
        private readonly ISemanaService _semanaService;
        private readonly IPagoClienteService _pagoClienteService;
        private readonly ICalendarioService _calendarioService;

        public PagoClienteController(IEmpleadoService empleadoService,
                                        ISemanaService semanaService,
                                        IPagoClienteService pagoClienteService,
                                        ICalendarioService calendarioService)
        {
            _empleadoService = empleadoService;
            _semanaService = semanaService;
            _pagoClienteService = pagoClienteService;
            _calendarioService = calendarioService;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.GetProfileId() != "1")
            {
                return RedirectToAction("Logout", "Account", new { Area = "" });
            }

            ViewBag.ListaEmpleados = await _empleadoService.GetEmpleadoAll();
            ViewBag.ListaSemana = await _semanaService.GetListaSemana();
            return View();
        }

        [HttpPost]
        public async Task<string> ActualizarPagoClienteXServicio(PagoClienteDTORequest pagoClienteDTORequest)
        {
            string resultado = string.Empty;

            try
            {
                CultureInfo culture = new CultureInfo("en-US");
                var pago = Math.Round(decimal.Parse(pagoClienteDTORequest.MontoPagoClienteText, culture), 2);
                pagoClienteDTORequest.MontoPagoCliente = pago;
                resultado = await _pagoClienteService.ActualizarPagoClienteXServicio(pagoClienteDTORequest);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }

            return resultado;
        }

        public async Task<IActionResult> ActualizarPagoCliente(int idServicio, int option)
        {
            CalendarioDTOResponse calendarioDTOResponse = new CalendarioDTOResponse();
            CalendarioDTORequest calendarioDTORequest = new CalendarioDTORequest()
            {
                IdServicio = idServicio
            };

            if (idServicio == 0) { ViewBag.titleModal = "Agregar"; }
            if (idServicio > 0 && option == 0) { ViewBag.titleModal = "Actualizar Monto Pagado del Cliente"; }

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
