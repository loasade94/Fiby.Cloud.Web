using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fiby.Cloud.Web.Client.Extensions;
using Fiby.Cloud.Web.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiby.Cloud.Web.Client.Areas.Horario.Controllers
{
    [Authorize]
    [Area("Horario")]
    public class ServicioSemanaController : Controller
    {
        private readonly IEmpleadoService _empleadoService;

        public ServicioSemanaController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }
        public async Task<IActionResult> Index()
        {
            if (User.Identity.GetProfileId() == "2")
            {
                var empleadoId = User.Identity.GetIdEmpleado();
                var listEmpleado = await _empleadoService.GetEmpleadoAll();
                ViewBag.ListaEmpleados = listEmpleado.Where(x => x.Codigo.ToString() == empleadoId);

                ViewBag.Layout = "~/Views/Shared/_LayoutEmpleado.cshtml";
            }
            else
            {
                ViewBag.ListaEmpleados = await _empleadoService.GetEmpleadoAll();
                ViewBag.Layout = "~/Views/Shared/_LayoutAdministrator.cshtml";
            }

            ViewBag.IdPerfil = User.Identity.GetProfileId();

            return View();
        }
    }
}
