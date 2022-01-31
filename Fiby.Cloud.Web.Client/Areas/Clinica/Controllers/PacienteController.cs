using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Client.Areas.Clinica.Controllers
{
    [Authorize]
    [Area("Clinica")]
    public class PacienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
