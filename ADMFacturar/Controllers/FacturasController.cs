using Microsoft.AspNetCore.Mvc;

namespace ADMFacturar.Controllers
{
    public class FacturasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RegistroAnulaciones()
        {
            return View();
        }

        public IActionResult RegistroModificaciones()
        {
            return View();
        }

        public IActionResult AgregarFactura()
        {
            return View();
        }

        public IActionResult Anular()
        {
            return View();
        }

        public IActionResult Imprimir()
        {
            return View();
        }

        public IActionResult EditarFactura()
        {
            return View();
        }
    }
}
