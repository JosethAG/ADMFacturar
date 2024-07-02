using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ADMFacturar.Controllers
{
    [Authorize(Roles = "Administrador, Usuario")]
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

        public IActionResult AgregarNotadeCredito()
        {
            return View();
        }

        public IActionResult DetallesFactura()
        {
            return View();
        }

    }
}
