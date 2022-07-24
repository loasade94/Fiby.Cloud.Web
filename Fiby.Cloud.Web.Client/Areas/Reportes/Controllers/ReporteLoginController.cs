using Fiby.Cloud.Web.Client.Extensions;
using Fiby.Cloud.Web.DTO.Modules.Reportes.Request;
using Fiby.Cloud.Web.Service.Interfaces;
using Fiby.Cloud.Web.Service.Interfaces.Reportes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Client.Areas.Reportes.Controllers
{
    [Authorize]
    [Area("Reportes")]
    public class ReporteLoginController : Controller
    {
        private readonly IReporteSemanaService _reporteSemanaService;
        private readonly IEmpleadoService _empleadoService;

        public ReporteLoginController(IReporteSemanaService reporteSemanaService,
                                        IEmpleadoService empleadoService)
        {
            _reporteSemanaService = reporteSemanaService;
            _empleadoService = empleadoService;
        }

        public async Task<IActionResult> Index()
        {

            if (User.Identity.GetProfileId() != "1")
            {
                return RedirectToAction("Logout", "Account", new { Area = "" });
            }

            ViewBag.ListaEmpleados = await _empleadoService.GetEmpleadoAll();

            var model = await _reporteSemanaService.GetReporteLogin(new ReporteSemanaDTORequest()
            {


            });

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> BuscarReporteLogin(ReporteSemanaDTORequest request)
        {
            var model = await _reporteSemanaService.GetReporteLogin(request);

            return View("Grid", model);
        }
    }
}
