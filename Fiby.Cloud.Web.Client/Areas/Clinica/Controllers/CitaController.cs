using Fiby.Cloud.Web.Client.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Client.Areas.Clinica.Controllers
{
    public class CitaController : Controller
    {
        [Authorize]
        [Area("Clinica")]
        public async Task<IActionResult> Index()
        {
            if (User.Identity.GetProfileId() == "2")
            {
                ViewBag.Layout = "~/Views/Shared/_LayoutEmpleado.cshtml";
            }
            else
            {
                ViewBag.Layout = "~/Views/Shared/_LayoutAdministrator.cshtml";
            }

            ViewBag.IdPerfil = User.Identity.GetProfileId();

            return View();
        }
    }
}
