using ADM.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ADM.Api.Cliente.Controllers
{
	public class VendedoresController : Controller
	{
		private readonly HttpClient _httpClient;

		public VendedoresController(IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient();
			_httpClient.BaseAddress = new Uri("https://localhost:7270/api");
		}

		public async Task<IActionResult> Index()
		{
			{
				var resp = await _httpClient.GetAsync("api/Vendedor/Listar");

				if (resp.IsSuccessStatusCode)
				{
					var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
					var vendedores = JsonConvert.DeserializeObject<IEnumerable<Vendedor>>(content);
					return View("Index", vendedores);
				}

				return View(new List<Vendedor>());
			}
		}

		public IActionResult CrearVendedor()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CrearVendedor(Vendedor vendedor)
		{
			if (ModelState.IsValid)
			{
				var json = JsonConvert.SerializeObject(vendedor);
				var content = new StringContent(json, Encoding.UTF8, "application/json");


				var response = await _httpClient.PostAsync("/api/Vendedor/Crear", content);

				if (response.IsSuccessStatusCode)
				{
					return RedirectToAction("Index");
				}
				else
				{
					return View();
				}

			}
			return View(vendedor);
		}

	}
}
