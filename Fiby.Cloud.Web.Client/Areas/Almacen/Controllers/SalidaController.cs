using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Client.Areas.Almacen.Controllers
{
    [Area("Almacen")]
    public class SalidaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
