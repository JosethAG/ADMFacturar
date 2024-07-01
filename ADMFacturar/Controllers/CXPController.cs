using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ADMFacturar.Controllers
{
    [Authorize(Roles = "Administrador, Usuario")]
    public class CXPController : Controller
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
