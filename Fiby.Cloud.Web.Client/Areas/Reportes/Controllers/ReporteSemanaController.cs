using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fiby.Cloud.Web.DTO.Modules.Reportes.Request;
using Fiby.Cloud.Web.Service.Interfaces.Horario;
using Fiby.Cloud.Web.Service.Interfaces.Reportes;
using Microsoft.AspNetCore.Mvc;

namespace Fiby.Cloud.Web.Client.Areas.Reportes.Controllers
{
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
            ViewBag.ListaSemana = await _semanaService.GetListaSemana();
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> BuscarReporteRentabilidadSemanal(ReporteSemanaDTORequest reporteSemanaDTORequest)
        {
            var model = await _reporteSemanaService.GetReporteRentabilidadSemanal(reporteSemanaDTORequest);
            return Json(model);
        }

    }
}
