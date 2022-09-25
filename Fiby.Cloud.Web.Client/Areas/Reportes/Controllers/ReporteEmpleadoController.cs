using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiby.Cloud.Web.Client.Areas.Reportes.Controllers
{
    [Authorize]
    [Area("Reportes")]
    public class ReporteEmpleadoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
