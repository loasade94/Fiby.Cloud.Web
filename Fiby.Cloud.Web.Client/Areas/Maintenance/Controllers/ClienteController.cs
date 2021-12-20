using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fiby.Cloud.Web.Client.Extensions;
using Fiby.Cloud.Web.DTO.Modules.Maintenance.Request;
using Fiby.Cloud.Web.DTO.Modules.Maintenance.Response;
using Fiby.Cloud.Web.Service.Interfaces.Maintenance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiby.Cloud.Web.Client.Areas.Maintenance.Controllers
{
    [Authorize]
    [Area("Maintenance")]
    public class ClienteController : Controller
    {

        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        public IActionResult Index()
        {
            if (User.Identity.GetProfileId() != "1")
            {
                return RedirectToAction("Logout", "Account", new { Area = "" });
            }

            return View();
        }

        [HttpPost]
        public async Task<string> RegistrarCliente(ClienteDTORequest clienteDTORequest)
        {
            string resultado = string.Empty;
            try
            {
                resultado = await _clienteService.RegistrarCliente(clienteDTORequest);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }

            return resultado;
        }

        [HttpPost]
        public async Task<JsonResult> BuscarClientes()
        {
            var model = await _clienteService.GetClienteAll();
            return Json(model);
        }

        [HttpDelete]
        public async Task<string> EliminarCliente(int idCliente)
        {

            var clienteDTORequest = new ClienteDTORequest
            {
                IdCliente = idCliente,
            };

            string response = await _clienteService.EliminarCliente(clienteDTORequest);
            return response;
        }

        public async Task<IActionResult> Actualizar(int idCliente, int option)
        {
            ClienteDTOResponse clienteDTOResponse = new ClienteDTOResponse();
            ClienteDTORequest clienteDTORequest = new ClienteDTORequest()
            {
                IdCliente = idCliente
            };

            if (idCliente == 0) { ViewBag.titleModal = "Agregar"; }
            if (idCliente > 0 && option == 0) { ViewBag.titleModal = "Editar"; }

            if (idCliente > 0)//EDITAR
            {
                clienteDTOResponse = await _clienteService.GetClientePorCodigo(clienteDTORequest);
                ViewBag.ModalGeneralIsNew = "0";
            }
            else
            {
                ViewBag.ModalGeneralIsNew = "1";
            }

            return PartialView(clienteDTOResponse);
        }

        [HttpPost]
        public async Task<JsonResult> BuscarClientesCodio(int codigo)
        {
            ClienteDTORequest request = new ClienteDTORequest();
            request.IdCliente = codigo;
            var model = await _clienteService.GetClientePorCodigo(request);
            return Json(model);
        }

    }
}
