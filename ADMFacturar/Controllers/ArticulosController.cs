using ADM.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ADM.Api.Cliente.Controllers
{
	public class ArticulosController : Controller
	{
		private readonly HttpClient _httpClient;

		public ArticulosController(IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient();
			_httpClient.BaseAddress = new Uri("https://localhost:7270/api");
		}

		public async Task<IActionResult> Index()
		{
			var resp = await _httpClient.GetAsync("api/Articulo/ListarArticulosVM");

			if (resp.IsSuccessStatusCode)
			{
				var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
				var articulos = JsonConvert.DeserializeObject<IEnumerable<ArticuloViewModel>>(content);
				return View("Index", articulos); // Devuelve 'articulos' en lugar de 'resp'
			}

			return View(new List<ArticuloViewModel>());
		}

		public IActionResult CrearArticulo()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CrearArticulo(Articulo articulo)
		{
			if (ModelState.IsValid)
			{
				var json = JsonConvert.SerializeObject(articulo);
				var content = new StringContent(json, Encoding.UTF8, "application/json");


				var response = await _httpClient.PostAsync("/api/Articulo/Crear", content);

				if (response.IsSuccessStatusCode)
				{
					return RedirectToAction("Index");
				}
				else
				{
					return View();
				}

			}
			return View(articulo);
		}

	}
}
