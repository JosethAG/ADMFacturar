using Microsoft.AspNetCore.Mvc;

namespace ADMFacturar.Controllers
{
    public class InventarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AgregarInventario()
        {
            return View();
        }

        public IActionResult AgregarArticulos()
        {
            return View();
        }
    }
}
