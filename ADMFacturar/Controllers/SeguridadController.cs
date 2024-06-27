﻿using ADM.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System;


namespace ADMFacturar.Controllers
{
    public class SeguridadController : Controller
    {
        private readonly HttpClient _httpClient;
        public SeguridadController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7270/api");
        }

        public async Task<IActionResult> Index()
        {
            {
                var resp = await _httpClient.GetAsync("api/Seguridad/Listar");

                if (resp.IsSuccessStatusCode)
                {
                    var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
                    var seguridades = JsonConvert.DeserializeObject<IEnumerable<Seguridad>>(content);
                    return View("Index", seguridades);
                }

                return View(new List<Seguridad>());
            }
        }

        public IActionResult CrearSeguridad()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearSeguridad(Seguridad seguridad)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(seguridad);
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                var response = await _httpClient.PostAsync("/api/Seguridad/Crear", content);

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
            return View(seguridad);
        }

        public async Task<IActionResult> ActualizarSeguridad(int? PK)
        {
            var resp = await _httpClient.GetAsync($"api/Seguridad/Obtener/{PK}");

            if (resp.IsSuccessStatusCode)
            {
                var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
                var seguridad = JsonConvert.DeserializeObject<Seguridad>(content);
                return View("ActualizarSeguridad", seguridad); // Devuelve 'seguridad' en lugar de 'resp'
            }

            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> ActualizarSeguridad(Seguridad seguridad)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(seguridad);
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                var response = await _httpClient.PostAsync("/api/Seguridad/Actualizar", content);

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
            return View(seguridad);
        }

        [HttpPost]
        public async Task<IActionResult> DesactivarSeguridad(string PK)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(PK, Encoding.UTF8, "application/json");
                var resp = await _httpClient.PostAsync($"/api/Seguridad/Desactivar/{PK}", content);
                string responseContent = await resp.Content.ReadAsStringAsync();
                Console.WriteLine("Response from API: " + responseContent);

                if (resp.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index"); // Devuelve 'Seguridad' en lugar de 'resp'
                }

                return NotFound();

            }
            return Ok();
        }

        public IActionResult Login()
        {
            return View();
        }

        

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (var response = await _httpClient.PostAsync("/api/Seguridad/Login", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<dynamic>(responseContent);

                        if (result.redirectTo != null && result.redirectTo == "/Home/Index")
                        {
                            Console.WriteLine("Login exitoso. Redireccionando a la página principal.");
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            Console.WriteLine("Error en las credenciales. No se pudo iniciar sesión.");
                            ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos.");
                            return View("Login");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error al conectar con el servidor API.");
                        ModelState.AddModelError(string.Empty, "Datos Incorrectos, ingréselos de nuevo.");
                        return View("Login");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al realizar la solicitud de login: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Error interno del servidor.");
                return View("Login");
            }
        }
    }
}
