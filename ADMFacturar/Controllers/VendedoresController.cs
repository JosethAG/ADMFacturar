﻿using ADM.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ADM.APICliente.Controllers
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
					ViewData["Vendedores"] = vendedores ?? new List<Vendedor>();
					return View("Index");
				}

				return View();
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
			return View(vendedor);
		}

		public async Task<IActionResult> ActualizarVendedor(int? PK)
		{
			var resp = await _httpClient.GetAsync($"api/Vendedor/Obtener/{PK}");

			if (resp.IsSuccessStatusCode)
			{
				var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
				var vendedor = JsonConvert.DeserializeObject<Vendedor>(content);
				return View("ActualizarVendedor", vendedor); // Devuelve 'vendedor' en lugar de 'resp'
			}

			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> ActualizarVendedor(Vendedor vendedor)
		{
			if (ModelState.IsValid)
			{
				var json = JsonConvert.SerializeObject(vendedor);
				var content = new StringContent(json, Encoding.UTF8, "application/json");


				var response = await _httpClient.PostAsync("/api/Vendedor/Actualizar", content);

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
			return View(vendedor);
		}

		[HttpPost]
		public async Task<IActionResult> DesactivarVendedor(string PK)
		{
			if (ModelState.IsValid)
			{
				var content = new StringContent(PK, Encoding.UTF8, "application/json");
				var resp = await _httpClient.PostAsync($"/api/Vendedor/Desactivar/{PK}", content);
				string responseContent = await resp.Content.ReadAsStringAsync();
				Console.WriteLine("Response from API: " + responseContent);

				if (resp.IsSuccessStatusCode)
				{
					return RedirectToAction("Index"); // Devuelve 'Vendedores' en lugar de 'resp'
				}

				return NotFound();

			}
			return Ok();
		}

	}
}