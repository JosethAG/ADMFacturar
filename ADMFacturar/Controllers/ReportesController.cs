﻿using Microsoft.AspNetCore.Mvc;

namespace ADMFacturar.Controllers
{
    public class ReportesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
   
    }
}