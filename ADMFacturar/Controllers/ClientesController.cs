using ADM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using NuGet.Configuration;

namespace ADM.APICliente.Controllers
{
    [Authorize(Roles = "Administrador, Usuario")]
    public class ClientesController : Controller
	{
		private readonly HttpClient _httpClient;

		public ClientesController(IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient();
			_httpClient.BaseAddress = new Uri("https://admfacturarapi-drg4cmduhxhhh8b7.centralus-01.azurewebsites.net/api");
		}

		/*
		 * Metodo de inicio 
		 * Modificación 13/07/24 por Joseth Araya
		 * Reducción de código sobre ViewData con metodo de ObtenerDatosDeApi
		 */
		public async Task<IActionResult> Index()
		{
			var resp = await _httpClient.GetAsync("api/Cliente/ListarClientesVM");

			if (resp.IsSuccessStatusCode)
			{
				var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
				var clientes = JsonConvert.DeserializeObject<IEnumerable<Cliente>>(content);
				ViewData["Clientes"] = clientes ?? new List<Cliente>();
				ViewData["Provincias"] = await ObtenerDatosDeApi<Provincia>("api/Direccion/Listar/Provincia");
				ViewData["Cantones"] = await ObtenerDatosDeApi<Canton>("api/Direccion/Listar/Canton");
				ViewData["Distritos"] = await ObtenerDatosDeApi<Distrito>("api/Direccion/Listar/Distrito");


				return View("Index");
			}

			return View();
		}

		/*
		 * Metodo de Crear 
		 * Modificación 13/07/24 por Joseth Araya
		 * Reducción de código sobre ViewData con metodo de ObtenerDatosDeApi
		 */
		public async Task<IActionResult> CrearCliente()
		{
			ViewData["Provincias"] = await ObtenerDatosDeApi<Provincia>("api/Direccion/Listar/Provincia");
			ViewData["Cantones"] = await ObtenerDatosDeApi<Canton>("api/Direccion/Listar/Canton");
			ViewData["Distritos"] = await ObtenerDatosDeApi<Distrito>("api/Direccion/Listar/Distrito");

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

				// Log the response content for debugging
				string responseContent = await response.Content.ReadAsStringAsync();
				Console.WriteLine("Response from API: " + responseContent);

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

		/*
		 * Metodo de Actualizar 
		 * Modificación 13/07/24 por Joseth Araya
		 * Reducción de código sobre ViewData con metodo de ObtenerDatosDeApi
		 */
		public async Task<IActionResult> ActualizarCliente(string? PK)
		{
			ViewData["Provincias"] = await ObtenerDatosDeApi<Provincia>("api/Direccion/Listar/Provincia");
			ViewData["Cantones"] = await ObtenerDatosDeApi<Canton>("api/Direccion/Listar/Canton");
			ViewData["Distritos"] = await ObtenerDatosDeApi<Distrito>("api/Direccion/Listar/Distrito");

			var resp = await _httpClient.GetAsync($"api/Cliente/Obtener/{PK}");

			if (resp.IsSuccessStatusCode)
			{
				var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
				var cliente = JsonConvert.DeserializeObject<Cliente>(content);
				return View("ActualizarCliente", cliente); // Devuelve 'clientes' en lugar de 'resp'
			}

			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> ActualizarCliente(Cliente cliente)
		{
			if (ModelState.IsValid)
			{
				var json = JsonConvert.SerializeObject(cliente);
				var content = new StringContent(json, Encoding.UTF8, "application/json");


				var response = await _httpClient.PostAsync("/api/Cliente/Actualizar", content);

				// Log the response content for debugging
				string responseContent = await response.Content.ReadAsStringAsync();
				Console.WriteLine("Response from API: " + responseContent);

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

		[HttpPost]
		public async Task<IActionResult> DesactivarCliente(string PK)
		{
			if (ModelState.IsValid)
			{
				var content = new StringContent(PK, Encoding.UTF8, "application/json");
				var resp = await _httpClient.PostAsync($"/api/Cliente/Desactivar/{PK}", content);
				string responseContent = await resp.Content.ReadAsStringAsync();
				Console.WriteLine("Response from API: " + responseContent);

				if (resp.IsSuccessStatusCode)
				{
					return RedirectToAction("Index"); // Devuelve 'clientes' en lugar de 'resp'
				}

				return NotFound();

			}
			return Ok();
		}

        public async Task<IActionResult> ListarClientes()
        {
            var respArts = await _httpClient.GetAsync($"api/Cliente/Listar/");

            if (respArts.IsSuccessStatusCode)
            {
                var clienteJson = await respArts.Content.ReadAsStringAsync();
                var clientes = JsonConvert.DeserializeObject<List<Cliente>>(clienteJson);
                return Json(clientes);
            }
            else
            {
                return BadRequest("Error al obtener los clientes");
            }

        }

		/*Listado para obtener la información de los articulos a editar
		Agregado el 13/7/24 por Joseth Araya ref Angelo Delgado Listar clientes*/
		public async Task<IActionResult> ObtenerCliente(string PK)
		{
			var respArts = await _httpClient.GetAsync($"api/Cliente/Obtener/{PK}");

			if (respArts.IsSuccessStatusCode)
			{
				var clienteJson = await respArts.Content.ReadAsStringAsync();
				var cliente = JsonConvert.DeserializeObject<Cliente>(clienteJson);
				return Ok(cliente);
			}
			else
			{
				return BadRequest("Error al obtener los cliente");
			}
		}

		/* 
		 * Metodo encargado de obtener toda la información, generado para obtener listas para viewbags
		 * Agregado el 13/7/24 por Joseth Araya
	    */
		private async Task<List<T>> ObtenerDatosDeApi<T>(string endpoint)
		{
			var response = await _httpClient.GetAsync(endpoint);
			if (response.IsSuccessStatusCode)
			{
				var data = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<List<T>>(data) ?? new List<T>();
			}
			return new List<T>();
		}
	}

}
