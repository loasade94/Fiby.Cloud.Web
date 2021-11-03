using Fiby.Cloud.Web.DTO.Modules.Sale.Request.Serie;
using Fiby.Cloud.Web.DTO.Modules.Sale.Response.Serie;
using Fiby.Cloud.Web.Service.Modules.Data.Interfaces.Sale;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Client.Areas.Sale.Controllers
{
    public class SerieController : Controller
    {

        private readonly ISerieService _serieService;

        [Area("Sale")]
        public IActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<List<SerieDTOResponse>> GetSerieAll(SerieDTORequest serieDTORequest)
        {
            serieDTORequest.Description = serieDTORequest.Description == null ? string.Empty : serieDTORequest.Description;
            //classificationDTORequest.StatusReg = classificationDTORequest.StatusReg == null ? string.Empty : classificationDTORequest.StatusReg;

            var model = await _serieService.GetSerieAll(serieDTORequest);
            return model;
        }
    }
}
