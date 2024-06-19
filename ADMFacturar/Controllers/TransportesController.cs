﻿using ADM.Models;
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
			return View(transporte);
		}


		public async Task<IActionResult> ActualizarTransporte(int? PK)
		{
			var resp = await _httpClient.GetAsync($"api/Transporte/Obtener/{PK}");

			if (resp.IsSuccessStatusCode)
			{
				var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
				var transporte = JsonConvert.DeserializeObject<Transporte>(content);
				return View("ActualizarTransporte", transporte); // Devuelve 'clientes' en lugar de 'resp'
			}

			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> ActualizarTransporte(Transporte transporte)
		{
			if (ModelState.IsValid)
			{
				var json = JsonConvert.SerializeObject(transporte);
				var content = new StringContent(json, Encoding.UTF8, "application/json");


				var response = await _httpClient.PostAsync("/api/Transporte/Actualizar", content);

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
			return View(transporte);
		}

		[HttpPost]
		public async Task<IActionResult> DesactivarTransporte(string PK)
		{
			if (ModelState.IsValid)
			{
				var content = new StringContent(PK, Encoding.UTF8, "application/json");
				var resp = await _httpClient.PostAsync($"/api/Transporte/Desactivar/{PK}", content);
				string responseContent = await resp.Content.ReadAsStringAsync();
				Console.WriteLine("Response from API: " + responseContent);

				if (resp.IsSuccessStatusCode)
				{
					return RedirectToAction("Index"); // Devuelve 'Transportes' en lugar de 'resp'
				}

				return NotFound();

			}
			return Ok();
		}

	}
}
