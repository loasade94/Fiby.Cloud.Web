﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fiby.Cloud.Web.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiby.Cloud.Web.Client.Areas.Horario.Controllers
{
    [Authorize]
    [Area("Horario")]
    public class ServicioSemanaController : Controller
    {
        private readonly IEmpleadoService _empleadoService;

        public ServicioSemanaController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.ListaEmpleados = await _empleadoService.GetEmpleadoAll();
            return View();
        }
    }
}