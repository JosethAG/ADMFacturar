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
                var cxps = JsonConvert.DeserializeObject<IEnumerable<CXP>>(content);

                // Aquí es donde se realiza la unión con proveedores para obtener el nombre
                var cxpsWithSupplierNames = cxps.Select(async cxp =>
                {
                    var proveedorResp = await _httpClient.GetAsync($"api/Proveedor/Obtener/{cxp.FK_Proveedor}");
                    if (proveedorResp.IsSuccessStatusCode)
                    {
                        var proveedorContent = await proveedorResp.Content.ReadAsStringAsync();
                        var proveedor = JsonConvert.DeserializeObject<Proveedor>(proveedorContent);
                        cxp.NombreProveedor = proveedor.Nombre;
                    }
                    return cxp;
                }).Select(t => t.Result).ToList();

                ViewData["CXP"] = cxpsWithSupplierNames ?? new List<CXP>();

                return View("Index");
            }

            return View();
        }

        [HttpGet]
        [Route("api/Proveedor/Obtener/{id}")]
        public async Task<Proveedor> ObtenerProveedor(int id)
        {
            var resp = await _httpClient.GetAsync($"api/Proveedor/{id}");

            if (resp.IsSuccessStatusCode)
            {
                var content = await resp.Content.ReadAsStringAsync();
                var proveedor = JsonConvert.DeserializeObject<Proveedor>(content);
                return proveedor;
            }

            return null;
        }


        public async Task<IActionResult> Detalles(string PK)
        {
            var resp = await _httpClient.GetAsync($"api/CXP/ListarAbonos/{PK}");

            if (resp.IsSuccessStatusCode)
            {
                var content = await resp.Content.ReadAsStringAsync();
                var abonos = JsonConvert.DeserializeObject<IEnumerable<AbonoCXP>>(content);
                return View(abonos);
            }

            return View();
        }

        public async Task<IActionResult> Abono(string PK)
        {
            var resp = await _httpClient.GetAsync($"api/CXP/Obtener/{PK}");

            if (resp.IsSuccessStatusCode)
            {
                var content = await resp.Content.ReadAsStringAsync();
                ViewData["CXP"] = JsonConvert.DeserializeObject<CXP>(content);
                return PartialView("Abono");
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
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EliminarAbono(string Numero_Recibo, string FK_Documento)
        {
            if (ModelState.IsValid)
            {
                // Crear el contenido JSON con ambos parámetros
                var jsonContent = JsonConvert.SerializeObject(new { Numero_Recibo, FK_Documento });
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Enviar la solicitud POST a la API
                var resp = await _httpClient.PostAsync($"/api/CXP/Eliminar/{Numero_Recibo}/{FK_Documento}", content);
                string responseContent = await resp.Content.ReadAsStringAsync();
                Console.WriteLine("Response from API: " + responseContent);

                if (resp.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Abono eliminado exitosamente.";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, responseContent);
                return View(); // Retorna a la vista actual en caso de error
            }

            return View(); // Retorna a la vista actual en caso de error en el modelo
        }

    }
}
