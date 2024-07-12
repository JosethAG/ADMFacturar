using ADM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ADMFacturar.Controllers
{
    [Authorize(Roles = "Administrador, Usuario")]
    public class CXPController : Controller
    {
        private readonly HttpClient _httpClient;

        public CXPController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7270/api");
        }

        public async Task<IActionResult> Index()
        {
            var resp = await _httpClient.GetAsync("api/CXP/Listar");

            if (resp.IsSuccessStatusCode)
            {
                var content = await resp.Content.ReadAsStringAsync();
                var cxp = JsonConvert.DeserializeObject<IEnumerable<CXP>>(content);
                ViewData["CXP"] = cxp ?? new List<CXP>();
                
                return View("Index");
            }

            return View();
        }

        public async Task<IActionResult> Detalles(string PK)
        {
            var resp = await _httpClient.GetAsync($"api/CXP/ListarAbonos/{PK}");

            if (resp.IsSuccessStatusCode)
            {
                var content = await resp.Content.ReadAsStringAsync();
                var abonos = JsonConvert.DeserializeObject<IEnumerable<AbonoCXP>>(content);
                ViewData["Abonos"] = abonos ?? new List<AbonoCXP>();
                return View("Detalles");
            }

            return View();
        }

        public async Task<IActionResult> Abono(string PK)
        {
            var resp = await _httpClient.GetAsync($"api/CXP/Obtener/{PK}");

            if (resp.IsSuccessStatusCode)
            {
                var content = await resp.Content.ReadAsStringAsync();
                var abono = JsonConvert.DeserializeObject<AbonoCXP>(content);
                ViewData["Abono"] = abono ?? new AbonoCXP(); // Inicializar un nuevo objeto si no hay datos
                return View("Abono", abono);
            }

            return View();
        }

    
        [HttpPost]
        public async Task<IActionResult> CrearAbono(AbonoCXP abono)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(abono);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync("api/CXP/Crear", content);
                    string responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                                TempData["SuccessMessage"] = responseContent;
                                return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, responseContent);
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
