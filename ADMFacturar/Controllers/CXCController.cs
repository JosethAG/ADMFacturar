﻿using ADM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ADMFacturar.Controllers
{
    [Authorize(Roles = "Administrador, Usuario")]
    public class CXCController : Controller
    {
        private readonly HttpClient _httpClient;

        public CXCController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7270/api");
        }

        public async Task<IActionResult> Index()
        {
            var resp = await _httpClient.GetAsync("api/CXC/Listar");

            if (resp.IsSuccessStatusCode)
            {
                var content = await resp.Content.ReadAsStringAsync();
                var cxp = JsonConvert.DeserializeObject<IEnumerable<CXC>>(content);
                ViewData["CXC"] = cxp ?? new List<CXC>();
                return View("Index");
            }

            return View();
        }

        public async Task<IActionResult> Detalles(string PK)
        {
            var resp = await _httpClient.GetAsync($"api/CXC/ListarAbonos/{PK}");

            if (resp.IsSuccessStatusCode)
            {
                var content = await resp.Content.ReadAsStringAsync();
                var abonos = JsonConvert.DeserializeObject<IEnumerable<AbonoCXC>>(content);
                ViewData["AbonosCXC"] = abonos ?? new List<AbonoCXC>();
                return View("Detalles");
            }

            return View();
        }

        public async Task<IActionResult> Abono(string PK)
        {
            var resp = await _httpClient.GetAsync($"api/CXC/Obtener/{PK}");

            if (resp.IsSuccessStatusCode)
            {
                var content = await resp.Content.ReadAsStringAsync();
                var abono = JsonConvert.DeserializeObject<AbonoCXC>(content);
                ViewData["AbonoCXC"] = abono ?? new AbonoCXC(); // Inicializar un nuevo objeto si no hay datos
                return View("Abono", abono);
            }

            return View();
        }

        public IActionResult CrearAbono()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearAbono(AbonoCXC abono)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(abono);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync("api/CXC/Crear", content);
                    string responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        // Verificar el resultado del procedimiento almacenado
                        var resultado = JsonConvert.DeserializeObject<int>(responseContent);
                        switch (resultado)
                        {
                            case 0:
                                ModelState.AddModelError(string.Empty, "Error: Saldo Pendiente es igual o menor a 0.");
                                break;
                            case 2:
                                ModelState.AddModelError(string.Empty, "Error: Monto Abonado es mayor que el Saldo Pendiente.");
                                break;
                            case 1:
                                TempData["SuccessMessage"] = "Abono creado correctamente.";
                                return RedirectToAction("Index");
                            default:
                                ModelState.AddModelError(string.Empty, "Error al guardar los datos.");
                                break;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Error al guardar los datos: " + response.ReasonPhrase + " - " + responseContent);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error en el servidor: " + ex.Message);
                }
            }
            return View(abono);
        }
    }
}
