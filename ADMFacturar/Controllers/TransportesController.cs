using ADM.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ADM.APICliente.Controllers
{
	public class TransportesController : Controller
	{
		private readonly HttpClient _httpClient;

		public TransportesController(IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient();
			_httpClient.BaseAddress = new Uri("https://localhost:7270/api");
		}

		public async Task<IActionResult> Index()
		{
			{
				var resp = await _httpClient.GetAsync("api/Transporte/Listar");

				if (resp.IsSuccessStatusCode)
				{
					var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
					var transportes = JsonConvert.DeserializeObject<IEnumerable<Transporte>>(content);
					return View("Index", transportes);
				}

				return View(new List<Transporte>());
			}
		}

		public IActionResult CrearTransporte()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CrearTransporte(Transporte transporte)
		{
			if (ModelState.IsValid)
			{
				var json = JsonConvert.SerializeObject(transporte);
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
			return View(transporte);
		}

	}
}
