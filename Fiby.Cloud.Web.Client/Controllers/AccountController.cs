using Fiby.Cloud.Web.Client.Extensions;
using Fiby.Cloud.Web.DTO.Modules.User.Request;
using Fiby.Cloud.Web.Service.Modules.Data.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

            string flagLoginExternal = string.Empty;
            try
            {
                //flagLoginExternal = _configuration["ExternalLoginFlag"];
                //var test = User.Identity.GetProfileId();
                flagLoginExternal = "0";
            }
            catch (Exception ex)
            {
                flagLoginExternal = "0";
            }

            //if (flagLoginExternal.Equals("1"))
            //{
            //    return RedirectToAction("LogInMicrosoft", "Account");
            //}
            ViewBag.FlagLoginExternal = flagLoginExternal;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string user, string pass)
        {
            var authDTORequest = new UserDTORequest() { NameUser = user, Password = pass };

            //var response = await _userService.LoginUser(authDTORequest);
            var response = await _userService.LoginUserNew(authDTORequest);

            if (response != "0")
            {
                UserDTORequest userDTORequest = new UserDTORequest();
                userDTORequest.UserId = int.Parse(response);

                //var listDataUser = await _userService.GetUserLogin(userDTORequest);
                var listDataUser = await _userService.GetUserLoginNew(userDTORequest);

                //TempData["UserOptions"] = JsonConvert.SerializeObject(listDataUser);
                //TempData["CompanyId"] = JsonConvert.SerializeObject(listDataUser.oCompany.CompanyId.ToString());

                List<Claim> claims = new List<Claim>();

                claims.Add(new Claim(CustomClaimTypes.AplicationAdmin, JsonConvert.SerializeObject(listDataUser)));
                claims.Add(new Claim(CustomClaimTypes.CompanyId, JsonConvert.SerializeObject(listDataUser.oCompany.CompanyId.ToString())));
                claims.Add(new Claim(CustomClaimTypes.ProfileId, listDataUser.oUser.RolId.ToString()));
                claims.Add(new Claim(CustomClaimTypes.Profile, JsonConvert.SerializeObject(listDataUser.oRol.Description.ToString())));
                claims.Add(new Claim(CustomClaimTypes.Nombre, JsonConvert.SerializeObject(listDataUser.oUser.Names.ToString())));
                claims.Add(new Claim(CustomClaimTypes.IdEmpleado, listDataUser.oUser.IdEmpleado.ToString()));

                ClaimsIdentity userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                //_claimValue.SetValue(CookieAuthenticationDefaults.AuthenticationScheme, "AplicationAdmin", JsonConvert.SerializeObject(listDataUser));

                ViewBag.Error = "";

                if (listDataUser.oRol.RolId == 1)
                {
                    return RedirectToAction("IndexAdminDashboard", "Home");
                }
                else
                {
                    return RedirectToAction("IndexEmpleadoDashboard", "Home");
                }


            }

            ViewBag.Error = "Usuario no registrado";
            return View();

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
