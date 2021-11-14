using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public PagoEmpleadoController(IEmpleadoService empleadoService,
                                        ISemanaService semanaService,
                                        IPagoEmpleadoService pagoEmpleadoService)
        {
            _empleadoService = empleadoService;
            _semanaService = semanaService;
            _pagoEmpleadoService = pagoEmpleadoService;
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
    }
}
