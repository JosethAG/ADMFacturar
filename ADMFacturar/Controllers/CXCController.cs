﻿using Microsoft.AspNetCore.Mvc;

namespace ADMFacturar.Controllers
{
    public class CXCController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detalles()
        {
            return View();
        }
        public IActionResult Abono()
        {
            return View();
        }
    }
}