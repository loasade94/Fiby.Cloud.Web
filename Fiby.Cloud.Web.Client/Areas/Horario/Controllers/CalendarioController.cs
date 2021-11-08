using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Client.Areas.Horario.Controllers
{
    [Area("Horario")]
    public class CalendarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
