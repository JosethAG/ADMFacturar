﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ADMFacturar.Controllers
{
    public class MarketingController : Controller
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