﻿using ADM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ADM.APIProveedor.Controllers
{
    [Authorize(Roles = "Administrador, Usuario")]
    public class ProveedoresController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProveedoresController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://admfacturarapi-drg4cmduhxhhh8b7.centralus-01.azurewebsites.net/api");
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

				return View();
			}
		}

		public IActionResult _CrearProveedor()
        {
            return PartialView("_CrearProveedor");
        }

        [HttpPost]
        public async Task<IActionResult> CrearProveedor(Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(proveedor);
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                var response = await _httpClient.PostAsync("/api/Proveedor/Crear", content);
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
            return View(proveedor);
        }


		public async Task<IActionResult> _ActualizarProveedor(int? id)
		{
			var resp = await _httpClient.GetAsync($"api/Proveedor/Obtener/{id}");

			if (resp.IsSuccessStatusCode)
			{
				var content = await resp.Content.ReadAsStringAsync(); 
				var proveedor = JsonConvert.DeserializeObject<Proveedor>(content);
				return PartialView("_ActualizarProveedor", proveedor); 
			}

			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> ActualizarProveedor(Proveedor Proveedor)
		{
			if (ModelState.IsValid)
			{
				var json = JsonConvert.SerializeObject(Proveedor);
				var content = new StringContent(json, Encoding.UTF8, "application/json");


				var response = await _httpClient.PostAsync("/api/Proveedor/Actualizar", content);

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
			return View(Proveedor);
		}

		[HttpPost]
		public async Task<IActionResult> DesactivarProveedor(string PK)
		{
			if (ModelState.IsValid)
			{
				var content = new StringContent(PK, Encoding.UTF8, "application/json");
				var resp = await _httpClient.PostAsync($"/api/Proveedor/Desactivar/{PK}", content);
				string responseContent = await resp.Content.ReadAsStringAsync();
				Console.WriteLine("Response from API: " + responseContent);

				if (resp.IsSuccessStatusCode)
				{
					return RedirectToAction("Index"); // Devuelve 'Proveedors' en lugar de 'resp'
				}

				return NotFound();

			}
			return Ok();
		}


		/*Listado para obtener la información del proveedor a editar
		Agregado el 19/7/24 por Joseth Araya */
		public async Task<IActionResult> ObtenerProveedor(string PK)
		{
			var respArts = await _httpClient.GetAsync($"api/Proveedor/Obtener/{PK}");

			if (respArts.IsSuccessStatusCode)
			{
				var proveedorJson = await respArts.Content.ReadAsStringAsync();
				var proveedor = JsonConvert.DeserializeObject<Proveedor>(proveedorJson);
				return Ok(proveedor);
			}
			else
			{
				return BadRequest("Error al obtener los proveedor");
			}
		}

	}
}
