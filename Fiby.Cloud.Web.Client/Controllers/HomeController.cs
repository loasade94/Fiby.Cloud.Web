using Fiby.Cloud.Web.Client.Extensions;
using Fiby.Cloud.Web.Client.Models;
using Fiby.Cloud.Web.DTO.Modules.User.Request;
using Fiby.Cloud.Web.Service.Modules.Data.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        private readonly IClaimValue _claimValue;
        public HomeController(ILogger<HomeController> logger,
            IUserService userService,
            IClaimValue claimValue)
        {
            _logger = logger;
            _userService = userService;
            _claimValue = claimValue;
        }

        public IActionResult IndexAdminDashboard(int id)
        {
            if (User.Identity.GetProfileId() != "1")
            {
                return RedirectToAction("Logout", "Account");
            }
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

        public IActionResult IndexEmpleadoDashboard(int id)
        {

            if (User.Identity.GetProfileId() != "2")
            {
                return RedirectToAction("Logout", "Account");
            }

            ViewBag.NombreSesion = User.Identity.GetNombre();
            return View();
        }


    }
}
