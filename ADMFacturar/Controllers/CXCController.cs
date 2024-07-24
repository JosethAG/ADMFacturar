using ADM.Models;
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
                // Aquí es donde se realiza la unión con proveedores para obtener el nombre
                var cxpsWithSupplierNames = cxp.Select(async cxp =>
                {
                    var clienteResp = await _httpClient.GetAsync($"api/Cliente/Obtener/{cxp.FK_Cliente}");
                    if (clienteResp.IsSuccessStatusCode)
                    {
                        var clienteContent = await clienteResp.Content.ReadAsStringAsync();
                        var cliente = JsonConvert.DeserializeObject<Cliente>(clienteContent);
                        cxp.NombreCliente = cliente.Nombre;
                    }
                    return cxp;
                }).Select(t => t.Result).ToList();
                ViewData["CXC"] = cxp ?? new List<CXC>();
                return View("Index");
            }

            return View();
        }

        [HttpGet]
        [Route("api/Cliente/Obtener/{id}")]
        public async Task<Cliente> ObtenerCliente(int id)
        {
            var resp = await _httpClient.GetAsync($"api/Cliente/{id}");

            if (resp.IsSuccessStatusCode)
            {
                var content = await resp.Content.ReadAsStringAsync();
                var cliente = JsonConvert.DeserializeObject<Cliente>(content);
                return cliente;
            }

            return null;
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
        ViewData["CXC"] = JsonConvert.DeserializeObject<CXC>(content);
        return PartialView("Abono");
    }

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
        public async Task<IActionResult> EliminarAbono(string Numero_Abono, string FK_Documento_CC)
        {
            if (ModelState.IsValid)
            {
                // Crear el contenido JSON con ambos parámetros
                var jsonContent = JsonConvert.SerializeObject(new { Numero_Abono, FK_Documento_CC });
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Enviar la solicitud POST a la API
                var resp = await _httpClient.PostAsync($"/api/CXC/Eliminar/{Numero_Abono}/{FK_Documento_CC}", content);
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
