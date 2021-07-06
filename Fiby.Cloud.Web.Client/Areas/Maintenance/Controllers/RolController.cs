using Fiby.Cloud.Web.Client.App_Start.Extensions;
using Fiby.Cloud.Web.DTO.Modules.User.Request;
using Fiby.Cloud.Web.DTO.Modules.User.Response;
using Fiby.Cloud.Web.Service.Modules.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Client.Areas.Maintenance.Controllers
{
    [Area("Maintenance")]
    public class RolController : Controller
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<List<RolDTOResponse>> GetRolAll(RolDTORequest rolDTORequest)
        {
            rolDTORequest.Description = rolDTORequest.Description == null ? string.Empty : rolDTORequest.Description;
            //classificationDTORequest.StatusReg = classificationDTORequest.StatusReg == null ? string.Empty : classificationDTORequest.StatusReg;

            var model = await _rolService.GetRolAll(rolDTORequest);
            return model;
        }

        public async Task<IActionResult> RegisterUpdateRol(int rolId, int option)
        {
            RolDTOResponse rolDTOResponse = new RolDTOResponse();
            RolDTORequest rolDTORequest = new RolDTORequest()
            {
                RolId = rolId
            };

            if (rolId == 0) { ViewBag.titleModal = "Agregar"; }
            if (rolId > 0 && option == 0) { ViewBag.titleModal = "Editar"; }

            ViewBag.listStatus = Lists.GetListStatus();

            if (rolId > 0)//EDITAR
            {
                rolDTOResponse = await _rolService.GetRolById(rolDTORequest);
                ViewBag.ModalGeneralIsNew = "0";
            }
            else
            {
                ViewBag.ModalGeneralIsNew = "1";
            }
            return PartialView(rolDTOResponse);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterOrUpdateRol(RolDTORequest request)
        {
            string response = string.Empty;
            request.Description = request.Description == null ? string.Empty : request.Description;

            if (request.RolId == 0)
            {
                response = await _rolService.RegisterRol(request);
            }
            else
            {
                response = await _rolService.UpdateRol(request);
            }
            
            return Json(response);
        }

        [HttpDelete]
        public async Task<string> DeleteRol(int rolId)
        {

            var rolDTORequest = new RolDTORequest
            {
                RolId = rolId,
            };

            string response = await _rolService.DeleteRol(rolDTORequest);
            return response;
        }

    }
}
