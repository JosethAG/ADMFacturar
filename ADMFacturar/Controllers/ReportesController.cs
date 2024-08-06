using ADM.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ADMFacturar.Controllers
{
    [Authorize(Roles = "Administrador, Usuario")]
    public class ReportesController : Controller
    {
		private readonly HttpClient _httpClient;

		public ReportesController(IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient();
			_httpClient.BaseAddress = new Uri("https://localhost:7270/api");
		}
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

		public async Task<IActionResult> RInventario()
		{
			var resp = await _httpClient.GetAsync("api/Reporte/ReporteArticulos");

			if (resp.IsSuccessStatusCode)
			{
				var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
				var articulos = JsonConvert.DeserializeObject<IEnumerable<Articulo>>(content);
				ViewData["Articulos"] = articulos ?? new List<Articulo>();
				return View("RInventario");
			}

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
