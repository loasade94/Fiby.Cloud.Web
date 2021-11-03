using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Client.Areas.Maintenance.Controllers
{
    [Area("Maintenance")]
    public class ProductoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
