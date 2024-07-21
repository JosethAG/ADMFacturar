using ADM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ADMFacturar.Controllers
{
    [Authorize(Roles = "Administrador, Usuario")]

    public class FacturasController : Controller
    {
        private readonly HttpClient _httpClient;

        public FacturasController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7270/api");
        }
        public async Task<IActionResult> Index()
        {
            var respProv = await _httpClient.GetAsync("api/Vendedor/Listar");
            if (respProv.IsSuccessStatusCode)
            {

                var dataProv = await respProv.Content.ReadAsStringAsync();
                var vendedores = JsonConvert.DeserializeObject<List<Vendedor>>(dataProv);
                ViewData["Vendedores"] = vendedores ?? new List<Vendedor>();
            }
            else
            {
                ViewData["Vendedores"] = new List<Vendedor>();
            }

            return View();
        }

        public async Task<IActionResult> ListarFacturas()


        {
            var resp = await _httpClient.GetAsync($"api/Facturas/Listar");

            if (resp.IsSuccessStatusCode)
            {
                var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
                var facturas = JsonConvert.DeserializeObject<IEnumerable<FacturaVM>>(content);
                return Json(facturas); // Devuelve 'Ingreso' en lugar de 'resp'
            }

            else
            {
                return BadRequest("Error al obtener los ingresos");
            }
        }


        public IActionResult RegistroAnulaciones()
        {
            return View();
        }

        public IActionResult RegistroModificaciones()
        {
            return View();
        }

        public async Task<IActionResult> AgregarFactura()
        {
            // Concatenamos el valor "01" al final de la URL de la API
            var restConc = await _httpClient.GetAsync("api/Facturas/ObtenerConsecutivo/01");
            if (restConc.IsSuccessStatusCode)
            {
                var dataConc = await restConc.Content.ReadAsStringAsync();

                // Verifica el formato del JSON devuelto
                var consecutivo = JsonConvert.DeserializeObject<ConsecutivoViewModel>(dataConc);

                // Manejo la respuesta
                ViewData["Consecutivo"] = consecutivo;
            }
            else
            {
                ViewData["Consecutivo"] = new List<ConsecutivoViewModel>();
            }
            var respProv = await _httpClient.GetAsync("api/Transporte/Listar");
            if (respProv.IsSuccessStatusCode)
            {

                var dataProv = await respProv.Content.ReadAsStringAsync();
                var transportes = JsonConvert.DeserializeObject<List<Transporte>>(dataProv);
                ViewData["Transporte"] = transportes ?? new List<Transporte>();
            }
            else
            {
                ViewData["Transporte"] = new List<Transporte>();
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GuardarFactura([FromBody] FacturaViewModel factura)
        {

            if (factura == null)
            {
                return BadRequest(new { success = false, message = "Factura no proporcionada" });
            }


            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(factura);
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                var response = await _httpClient.PostAsync("api/Facturas/Crear", content);
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response from API: " + responseContent);


                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Error al guardar la factura en la API" });
                }

            }
            return View();

        }


        public async Task<IActionResult> ObtenerProducto(string producto)
        {
            if (string.IsNullOrEmpty(producto))
            {
                return BadRequest(new { message = "El producto es requerido." });
            }

            try
            {
                var resp = await _httpClient.GetAsync($"api/Facturas/ObtenerProducto/{producto}");

                if (resp.IsSuccessStatusCode)
                {
                    var content = await resp.Content.ReadAsStringAsync();
                    var productoData = JsonConvert.DeserializeObject<ProductoViewModel>(content);
                    return Json(productoData);
                }
                else
                {
                    return StatusCode((int)resp.StatusCode, new { message = "Error al obtener datos del producto desde la API externa" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error interno del servidor: {ex.Message}" });
            }
        }


        public async Task<IActionResult> GenerarNC(string? consecutivo)
        {

            // Concatenamos el valor "01" al final de la URL de la API
            var restConc = await _httpClient.GetAsync("api/Facturas/ObtenerConsecutivo/02");
            if (restConc.IsSuccessStatusCode)
            {
                var dataConc = await restConc.Content.ReadAsStringAsync();

                // Verifica el formato del JSON devuelto
                var consec = JsonConvert.DeserializeObject<ConsecutivoViewModel>(dataConc);

                // Manejo la respuesta
                ViewData["Consecutivo"] = consec;
            }
            else
            {
                ViewData["Consecutivo"] = new List<ConsecutivoViewModel>();
            }

            var resp = await _httpClient.GetAsync($"api/Facturas/Obtener/{consecutivo}");

            if (resp.IsSuccessStatusCode)
            {
                var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
                var factura = JsonConvert.DeserializeObject<FacturaViewModel>(content);
                return View("AgregarNC", factura); // Devuelve 'clientes' en lugar de 'resp'
            }

            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> GuardarNC([FromBody] FacturaViewModel factura)
        {

            if (factura == null)
            {
                return BadRequest(new { success = false, message = "Factura no proporcionada" });
            }


            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(factura);
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                var response = await _httpClient.PostAsync("api/Facturas/CrearNC", content);
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response from API: " + responseContent);


                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Error al guardar la factura en la API" });
                }

            }
            return View();

        }


        public async Task<IActionResult> DetallesNC(string? documento)
        {

    
            var resp = await _httpClient.GetAsync($"api/Facturas/Obtener/{documento}");

            if (resp.IsSuccessStatusCode)
            {
                var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
                var factura = JsonConvert.DeserializeObject<FacturaViewModel>(content);
                return View("DetallesNC", factura); // Devuelve 'clientes' en lugar de 'resp'
            }

            return NotFound();

        }

        public async Task<IActionResult> DetallesFactura(string? documento)
        {

            var respProv = await _httpClient.GetAsync("api/Transporte/Listar");
            if (respProv.IsSuccessStatusCode)
            {

                var dataProv = await respProv.Content.ReadAsStringAsync();
                var transportes = JsonConvert.DeserializeObject<List<Transporte>>(dataProv);
                ViewData["Transporte"] = transportes ?? new List<Transporte>();
            }
            else
            {
                ViewData["Transporte"] = new List<Transporte>();
            }



            var resp = await _httpClient.GetAsync($"api/Facturas/Obtener/{documento}");

            if (resp.IsSuccessStatusCode)
            {
                var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
                var factura = JsonConvert.DeserializeObject<FacturaViewModel>(content);
                return View("DetallesFactura", factura); // Devuelve 'clientes' en lugar de 'resp'
            }

            return NotFound();

        }


        public IActionResult Anular()
        {
            return View();
        }

        public IActionResult Imprimir()
        {
            return View();
        }

        public IActionResult EditarFactura()
        {
            return View();
        }

        public IActionResult AgregarNC()
        {
            return View();
        }

      


    }
}
