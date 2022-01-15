using Fiby.Cloud.Web.Client.Extensions;
using Fiby.Cloud.Web.DTO.Modules.Horario.Request;
using Fiby.Cloud.Web.Service.Interfaces.Horario;
using Fiby.Cloud.Web.Service.Interfaces.Maintenance;
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
    public class ServicioClienteController : Controller
    {

        private readonly ISemanaService _semanaService;
        private readonly IClienteService _clienteService;
        public ServicioClienteController(ISemanaService semanaService,
                                        IClienteService clienteService)
        {
            _semanaService = semanaService;
            _clienteService = clienteService;
        }

        public async Task<IActionResult> Index()
        {

            if (User.Identity.GetProfileId() != "1")
            {
                return RedirectToAction("Logout", "Account", new { Area = "" });
            }

            var listaSemana = await _semanaService.GetListaSemana();

            ViewBag.ListaCliente = await _clienteService.GetClienteAll();

            ViewBag.ListaSemana = listaSemana;

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> BuscarServicioXCliente(ServicioClienteDTORequest servicioClienteDTORequest, int indicadorInicio)
        {

            var model = await _semanaService.GetListaServicioXCliente(servicioClienteDTORequest);
            return Json(model);
        }
    }
}
