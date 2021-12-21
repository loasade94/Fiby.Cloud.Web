﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fiby.Cloud.Web.Client.Extensions;
using Fiby.Cloud.Web.DTO.Modules.Reportes.Request;
using Fiby.Cloud.Web.Service.Interfaces.Horario;
using Fiby.Cloud.Web.Service.Interfaces.Reportes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiby.Cloud.Web.Client.Areas.Reportes.Controllers
{
    [Authorize]
    [Area("Reportes")]
    public class ReporteSemanaController : Controller
    {
        private readonly IReporteSemanaService _reporteSemanaService;
        private readonly ISemanaService _semanaService;

        public ReporteSemanaController(IReporteSemanaService reporteSemanaService,
                                        ISemanaService semanaService)
        {
            _reporteSemanaService = reporteSemanaService;
            _semanaService = semanaService;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.GetProfileId() != "1")
            {
                return RedirectToAction("Logout", "Account", new { Area = "" });
            }

            ViewBag.ListaSemana = await _semanaService.GetListaSemana();
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> BuscarReporteRentabilidadSemanal(ReporteSemanaDTORequest reporteSemanaDTORequest)
        {
            var model = await _reporteSemanaService.GetReporteRentabilidadSemanal(reporteSemanaDTORequest);
            return Json(model);
        }


        [HttpPost]
        public async Task<JsonResult> BuscarRentabilidadGraficoDashboard()
        {
            var model = await _semanaService.GetRentabilidadGraficoDashboard();
            return Json(model);
        }

        [HttpPost]
        public async Task<JsonResult> BuscarPasajesEmpleadoDashboard()
        {
            var model = await _semanaService.GetPasajesEmpleadoDashboard();
            return Json(model);
        }

        [HttpPost]
        public async Task<JsonResult> BuscarAnunciosParaEmpleados()
        {
            var model = await _reporteSemanaService.GetAnunciosParaEmpleados();
            return Json(model);
        }

    }
}
