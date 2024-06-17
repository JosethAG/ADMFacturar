using ADM.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ADM.Api.Cliente.Controllers
{
	public class ProveedoresController : Controller
	{
		private readonly HttpClient _httpClient;

		public ProveedoresController(IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient();
			_httpClient.BaseAddress = new Uri("https://localhost:7270/api");
		}

		public async Task<IActionResult> Index()
		{
			{
				var resp = await _httpClient.GetAsync("api/Proveedor/Listar");

				if (resp.IsSuccessStatusCode)
				{
					var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
					var proveedores = JsonConvert.DeserializeObject<IEnumerable<Proveedor>>(content);
					return View("Index", proveedores);
				}

				return View(new List<Proveedor>());
			}
		}

		public IActionResult CrearProveedor()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CrearProveedor(Proveedor proveedor)
		{
			if (ModelState.IsValid)
			{
				var json = JsonConvert.SerializeObject(proveedor);
				var content = new StringContent(json, Encoding.UTF8, "application/json");


				var response = await _httpClient.PostAsync("/api/Transporte/Crear", content);

				if (response.IsSuccessStatusCode)
				{
					return RedirectToAction("Index");
				}
				else
				{
					return View();
				}

			}
			return View(proveedor);
		}

	}
}
