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

        public IActionResult EditarInventario()
        {
            return View();
        }

        public IActionResult DetallesInventario()
        {
            return View();
        }

        public IActionResult EditarArticulo()
        {
            return View();
        }

        public IActionResult EditarArticuloDesdeAgregarArticulos()
        {
            return View();
        }

        public IActionResult DetallesArticulo()
        {
            return View();
        }
    }
}
