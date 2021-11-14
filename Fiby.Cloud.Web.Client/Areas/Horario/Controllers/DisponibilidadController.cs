using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fiby.Cloud.Web.DTO.Modules.Horario.Request;
using Fiby.Cloud.Web.Service.Interfaces.Horario;
using Microsoft.AspNetCore.Mvc;

namespace Fiby.Cloud.Web.Client.Areas.Horario.Controllers
{
    [Area("Horario")]
    public class DisponibilidadController : Controller
    {

        private readonly ISemanaService _semanaService;

        public DisponibilidadController(ISemanaService semanaService)
        {
            _semanaService = semanaService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ListaSemana = await _semanaService.GetListaSemana();
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetDisponibilidadSemana(SemanaDTORequest semanaDTORequest)
        {
            var model = await _semanaService.GetDisponibilidadSemana(semanaDTORequest);
            return Json(model);
        }
    }
}
