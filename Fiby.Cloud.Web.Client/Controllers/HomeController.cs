using Fiby.Cloud.Web.Client.Extensions;
using Fiby.Cloud.Web.Client.Models;
using Fiby.Cloud.Web.DTO.Modules.User.Request;
using Fiby.Cloud.Web.Service.Modules.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Client.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger,
            IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public async Task<IActionResult> IndexAdminDashboard(int id)
        {

            //UserDTORequest userDTORequest = new UserDTORequest();
            //userDTORequest.UserId = id;

            //var listDataUser = await _userService.GetUserLogin(userDTORequest);

            //if (listDataUser != null)
            //{

            //    //ViewBag.listRol = listDataUser.oRol;
            //    //ViewBag.listStore = listDataUser.oStore;
            //    //ViewBag.listUser = listDataUser.oUser;
            //    //ViewBag.listCompany = listDataUser.oCompany;
            //    //ViewBag.listMenu = listDataUser.oListMenu;
            //    //ViewBag.listSubMenu = listDataUser.oListSubMenu;

            //    ViewData["UserOptions"] = listDataUser;
            //}
            ViewBag.NombreSesion = User.Identity.GetNombre();
            return View();
        }
    }
}
