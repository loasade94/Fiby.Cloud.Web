using Fiby.Cloud.Web.Client.Extensions;
using Fiby.Cloud.Web.DTO.Modules.User.Request;
using Fiby.Cloud.Web.Service.Modules.Data.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Client.Controllers
{
    public class AccountController : Controller
    {

        private readonly IUserService _userService;
        private readonly IClaimValue _claimValue;

        public AccountController(IUserService userService,
            IClaimValue claimValue)
        {
            _userService = userService;
            _claimValue = claimValue;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string user, string pass)
        {
            var authDTORequest = new UserDTORequest() { NameUser = user, Password = pass };

            var response = await _userService.LoginUser(authDTORequest);

            if (response == "1")
            {
                UserDTORequest userDTORequest = new UserDTORequest();
                userDTORequest.UserId = 1;

                var listDataUser = await _userService.GetUserLogin(userDTORequest);

                TempData["UserOptions"] = JsonConvert.SerializeObject(listDataUser); ;

                List<Claim> claims = new List<Claim>();

                claims.Add(new Claim(CustomClaimTypes.AplicationAdmin, JsonConvert.SerializeObject(listDataUser)));

                ClaimsIdentity userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                //_claimValue.SetValue(CookieAuthenticationDefaults.AuthenticationScheme, "AplicationAdmin", JsonConvert.SerializeObject(listDataUser));

                ViewBag.Error = "";
                return RedirectToAction("IndexAdminDashboard", "Home", new { id = 1 });
            }

            ViewBag.Error = "Usuario no registrado";
            return View();

        }
    }
}
