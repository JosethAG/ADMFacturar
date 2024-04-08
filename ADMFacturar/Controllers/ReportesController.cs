using Microsoft.AspNetCore.Mvc;

namespace ADMFacturar.Controllers
{
    public class ReportesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RVentas()
        {
            return View();
        }
        public IActionResult REstadosCuentas()
        {
            return View();
        }
        public IActionResult RCardex()
        {
            return View();
        }
        public IActionResult RInventario()
        {
            return View();
        }
        public IActionResult RVendedores()
        {
            return View();
        }
        public IActionResult RCxC()
        {
            return View();
        }
        public IActionResult RCxP()
        {
            return View();
        }
    }
}
