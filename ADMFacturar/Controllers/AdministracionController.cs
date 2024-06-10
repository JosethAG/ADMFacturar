using Microsoft.AspNetCore.Mvc;

namespace ADMFacturar.Controllers
{
    public class AdministracionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ListClientes()
        {
            return View();
        }
  
        public IActionResult ListProvedores()
        {
            return View();
        }

        public IActionResult ListArticulos()
        {
            return View();
        }

        public IActionResult ListVendedores()
        {
            return View();
        }
        public IActionResult ListTransportes()
        {
            return View();
        }

    }
}
