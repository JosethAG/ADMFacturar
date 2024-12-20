﻿using ADM.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ADM.APICliente.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ArticulosController : Controller
	{
		private readonly HttpClient _httpClient;

		public ArticulosController(IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient();
			_httpClient.BaseAddress = new Uri("https://admfacturarapi-drg4cmduhxhhh8b7.centralus-01.azurewebsites.net/api");
		}

		public async Task<IActionResult> Index()
		{
			var resp = await _httpClient.GetAsync("api/Articulo/ListarArticulosVM");

			if (resp.IsSuccessStatusCode)
			{
				var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
				var articulos = JsonConvert.DeserializeObject<IEnumerable<Articulo>>(content);
				ViewData["Articulos"] = articulos ?? new List<Articulo>();
				return View("Index");
			}

			return View();
		}

		public async Task<IActionResult> _CrearArticulo()
		{
            ViewData["Proveedores"] = await ObtenerDatosDeApi<Proveedor>("api/Proveedor/Listar");
            return PartialView("_CrearArticulo");
		}

		[HttpPost]
		public async Task<IActionResult> CrearArticulo(Articulo articulo)
		{
			if (ModelState.IsValid)
			{
				var json = JsonConvert.SerializeObject(articulo);
				var content = new StringContent(json, Encoding.UTF8, "application/json");


				var response = await _httpClient.PostAsync("/api/Articulo/Crear", content);
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
			return View(articulo);
		}

		public async Task<IActionResult> _ActualizarArticulo(string? id)
		{
			var resp = await _httpClient.GetAsync($"api/Articulo/Obtener/{id}");

			if (resp.IsSuccessStatusCode)
			{
                ViewData["Proveedores"] = await ObtenerDatosDeApi<Proveedor>("api/Proveedor/Listar");
                var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
				var articulo = JsonConvert.DeserializeObject<Articulo>(content);
				return PartialView("_ActualizarArticulo", articulo); // Devuelve 'articulos' en lugar de 'resp'
			}

			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> ActualizarArticulo(Articulo articulo)
		{
			if (ModelState.IsValid)
			{
				var json = JsonConvert.SerializeObject(articulo);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				var response = await _httpClient.PostAsync("/api/Articulo/Actualizar", content);

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
			return View(articulo);
		}


		[HttpPost]
		public async Task<IActionResult> DesactivarArticulo(string PK)
		{
			if (ModelState.IsValid)
			{
				var content = new StringContent(PK, Encoding.UTF8, "application/json");
				var resp = await _httpClient.PostAsync($"/api/Articulo/Desactivar/{PK}", content);
				string responseContent = await resp.Content.ReadAsStringAsync();
				Console.WriteLine("Response from API: " + responseContent);

				if (resp.IsSuccessStatusCode)
				{
					return RedirectToAction("Index"); // Devuelve 'articulos' en lugar de 'resp'
				}

				return NotFound();

			}
			return Ok();
		}

		/*Listado para obtener la información de los articulos a editar
		Agregado el 9/7/24 por Joseth ref Angelo Listar clientes*/
		public async Task<IActionResult> ObtenerArt(string PK)
		{
			var respArts = await _httpClient.GetAsync($"api/Articulo/Obtener/{PK}");

			if (respArts.IsSuccessStatusCode)
			{
				var articuloJson = await respArts.Content.ReadAsStringAsync();
				var articulo = JsonConvert.DeserializeObject<Articulo>(articuloJson);
				return Ok(articulo);
			}
			else
			{
				return BadRequest("Error al obtener los articulo");
			}
		}
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
