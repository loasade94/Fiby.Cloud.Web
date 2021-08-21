﻿using Fiby.Cloud.Web.DTO.Modules.Ple.Request;
using Fiby.Cloud.Web.DTO.Modules.User.Request;
using Fiby.Cloud.Web.DTO.Modules.User.Response;
using Fiby.Cloud.Web.Service.Modules.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiby.Cloud.Web.Client.Extensions;

namespace Fiby.Cloud.Web.Client.Areas.Sale.Controllers
{
    [Area("Sale")]
    public class GeneratorSaleController : Controller
    {

        private readonly IPleService _pleService;

        public GeneratorSaleController(IPleService pleService)
        {
            _pleService = pleService;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        public async Task<FileResult> GetPleAll(PleDTORequest pleDTORequest)
        {
            //pleDTORequest.SequenceCUO = pleDTORequest.Description == null ? string.Empty : pleDTORequest.Description;

            TextWriter tw = new StreamWriter(Path.Combine(Path.GetTempPath(), "plantilla.txt"), true);

            //var model = await _pleService.GetPleAll(pleDTORequest);
            var model = await _pleService.GetPleAllNew(pleDTORequest);

            for (int i = 0; i < model.Count; i++)
            {
                tw.WriteLine(model[i].Line);
            }

            tw.Close();

            //return File(Path.Combine(Path.GetTempPath(), "plantilla.txt"), "text/plain");

            byte[] byteArray = System.IO.File.ReadAllBytes(Path.Combine(Path.GetTempPath(), "plantilla.txt"));

            //var reportName = "Plev1";

            //Response.Headers.Add("Content-Disposition", String.Format("attachment; filename={0}", $"{reportName}_{DateTime.Now.ToString()}.txt"));
            //return new FileContentResult(byteArray, "text/plain");

            //String text = "Hola mundo" + Environment.NewLine + "Hoy es Martes";
            //byte[] result = Encoding.ASCII.GetBytes(text);
            return File(byteArray, "text/plain", "ple.txt");
        }


        [HttpPost]
        public async Task<string> RegistrarPle14100DPorMes(PLE14100DTORequest pLE14100DTORequest)
        {
            string resultado = string.Empty;
            pLE14100DTORequest.IdEmpresa = Int16.Parse(User.Identity.CompanyId());
            try
            {
                resultado = await _pleService.RegistrarPle14100DPorMes(pLE14100DTORequest);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }

            return resultado;
        }


    }
}
