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

        public async Task<IActionResult> RegisterUpdateRol(int variableId, int option)
        {
            //List<VariableDTOResponse> variableDTOResponse = new List<VariableDTOResponse>();
            //VariableDTOResponse objVariableDTOResponse = new VariableDTOResponse();
            //VariableDTORequest variableDTORequest = new VariableDTORequest()
            //{
            //    VariableId = variableId
            //};
            //ViewBag.listRTPSEquivalent = await _tableBaseService.GetTableDetailAll("R018", User.Identity.GetToken());
            //ViewBag.listSituation = await _variableService.GetStatus(User.Identity.GetToken());
            //if (variableId == 0) { ViewBag.titleModal = "Agregar"; }
            //if (variableId > 0 && option == 0) { ViewBag.titleModal = "Editar"; }
            //if (variableId > 0 && option > 0) { ViewBag.titleModal = "Consultar"; }

            //if (variableId > 0)//EDITAR
            //{
            //    ViewBag.ModalGeneralIsNew = "0";
            //    variableDTOResponse = await _variableService.GetVariableAll(variableDTORequest, User.Identity.GetToken());
            //    objVariableDTOResponse = variableDTOResponse.First();
            //}
            //else
            //{
            //    ViewBag.ModalGeneralIsNew = "1";
            //}
            //return PartialView(objVariableDTOResponse);
            ViewBag.titleModal = "Agregar";
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterOrUpdateRol(RolDTORequest rolDTORequest)
        {
            rolDTORequest.Description = rolDTORequest.Description == null ? string.Empty : rolDTORequest.Description;

            string response = await _rolService.RegisterRol(rolDTORequest);
            return Json(response);
        }

    }
}
