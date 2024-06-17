using ADM.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ADM.Api.Cliente.Controllers
{
	public class ClientesController : Controller
	{
		private readonly HttpClient _httpClient;

		public ClientesController(IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient();
			_httpClient.BaseAddress = new Uri("https://localhost:7270/api");
		}

		public async Task<IActionResult> Index()
		{
			var resp = await _httpClient.GetAsync("api/Cliente/ListarClientesVM");

			if (resp.IsSuccessStatusCode)
			{
				var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
				var clientes = JsonConvert.DeserializeObject<IEnumerable<ClienteViewModel>>(content);
				return View("Index", clientes); // Devuelve 'clientes' en lugar de 'resp'
			}

			return View(new List<ClienteViewModel>());
		}

		public IActionResult CrearCliente() 
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CrearCliente(Cliente cliente)
		{
			if (ModelState.IsValid)
			{
				var json = JsonConvert.SerializeObject(cliente);
				var content = new StringContent(json, Encoding.UTF8, "application/json");


				var response = await _httpClient.PostAsync("/api/Cliente/Crear", content);

				if (response.IsSuccessStatusCode)
				{
					return RedirectToAction("Index");
				}
				else
				{
					return View();
				}

			}
			return View(cliente);
		}

	}
}
