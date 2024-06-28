using ADM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ADM.APIIngresoMercaderia.Controllers
{
    public class InventarioController : Controller
    {
        private readonly HttpClient _httpClient;

        public InventarioController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7270/api");
        }

        public IActionResult IndexGeneral()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var respProv = await _httpClient.GetAsync("api/Proveedor/Listar");
            if (respProv.IsSuccessStatusCode)
            {

                var dataProv = await respProv.Content.ReadAsStringAsync();
                var proveedores = JsonConvert.DeserializeObject<List<Proveedor>>(dataProv);
                ViewData["Proveedores"] = proveedores ?? new List<Proveedor>();
            }
            else
            {
                ViewData["Proveedores"] = new List<Proveedor>();
            }         

          
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GuardarIngresoMercaderia(string PK)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(PK, Encoding.UTF8, "application/json");
                var resp = await _httpClient.PostAsync($"/api/IngresoMercaderia/Guardar/{PK}", content);
                string responseContent = await resp.Content.ReadAsStringAsync();
                Console.WriteLine("Response from API: " + responseContent);

                if (resp.IsSuccessStatusCode)
                {
                    return RedirectToAction("AgregarInventario"); // Devuelve 'Ingreso' en lugar de 'resp'
                }

                return NotFound();

            }
            return Ok();
        }


        public async Task<IActionResult> AgregarInventario(IngresoMercaderia IngresoMercaderia)
        {
            var respProv = await _httpClient.GetAsync("api/Proveedor/Listar");
            if (respProv.IsSuccessStatusCode)
            {

                var dataProv = await respProv.Content.ReadAsStringAsync();
                var proveedores = JsonConvert.DeserializeObject<List<Proveedor>>(dataProv);
                ViewData["Proveedores"] = proveedores ?? new List<Proveedor>();
            }
            else
            {
                ViewData["Proveedores"] = new List<Proveedor>();
            }


            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(IngresoMercaderia);
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                var response = await _httpClient.PostAsync("/api/IngresoMercaderia/Crear", content);

                // Log the response content for debugging
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response from API: " + responseContent);


                var respArts = await _httpClient.GetAsync($"api/Articulo/ArticuloXDocumento/{IngresoMercaderia.PK_FK_Documento}");

                if (respArts.IsSuccessStatusCode)
                {
                    var dataArts = await respArts.Content.ReadAsStringAsync();
                    var articulos = JsonConvert.DeserializeObject<List<ArticuloViewModel>>(dataArts);
                    ViewData["Articulos"] = articulos ?? new List<ArticuloViewModel>();
                }
                else
                {
                    ViewData["Articulos"] = new List<ArticuloViewModel>();
                }

                if (response.IsSuccessStatusCode)
                {

                    return View("AgregarInventario");

                }
                else
                {
                    return View();
                }

            }
            return View(IngresoMercaderia);
        }


        [HttpPost]
        public async Task<IActionResult> EliminarArticulo(string PK_FK_Documento, string PK_Articulo)
        {
            if (ModelState.IsValid)
            {
                // Crear el contenido JSON con ambos parámetros
                var jsonContent = JsonConvert.SerializeObject(new { PK_FK_Documento, PK_Articulo });
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Enviar la solicitud POST a la API
                var resp = await _httpClient.PostAsync($"/api/IngresoMercaderia/Eliminar/{PK_FK_Documento}/{PK_Articulo}", content);
                string responseContent = await resp.Content.ReadAsStringAsync();
                Console.WriteLine("Response from API: " + responseContent);

                if (resp.IsSuccessStatusCode)
                {
                    return RedirectToAction("AgregarInventario");
                }

                return NotFound();
            }

            return Ok();
        }

   
        public async Task<IActionResult> ListarArticulos()
        {


            var respArts = await _httpClient.GetAsync($"api/Articulo/Listar/");

            if (respArts.IsSuccessStatusCode)
            {
                var articulosJson = await respArts.Content.ReadAsStringAsync();
                var articulos = JsonConvert.DeserializeObject<List<Articulo>>(articulosJson);
                return Json(articulos);
            }
            else
            {
                return BadRequest("Error al obtener los artículos");
            }

        }


        public IActionResult IndexSalida()
        {
            return View();
        }
   

        public async Task<IActionResult> ListarIngresos()
        
        
        {
            var resp = await _httpClient.GetAsync($"api/IngresoMercaderia/ListarIngresoMercaderiasVM");

            if (resp.IsSuccessStatusCode)
            {
                var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
                var ingresos = JsonConvert.DeserializeObject<IEnumerable<IngresoMercaderiaViewModel>>(content);
                return Json(ingresos); // Devuelve 'Ingreso' en lugar de 'resp'
            }

            else
            {
                return BadRequest("Error al obtener los ingresos");
            }
        }




        //----------------Inicio Codigo para Salida de Mercaderia-------------------
        public async Task<IActionResult> ListarSalidas()


        {
            var resp = await _httpClient.GetAsync($"api/SalidaMercaderia/ListarSalidaMercaderia");

            if (resp.IsSuccessStatusCode)
            {
                var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
                var salidas = JsonConvert.DeserializeObject<IEnumerable<SalidaMercaderia>>(content);
                return Json(salidas); // Devuelve 'Ingreso' en lugar de 'resp'
            }

            else
            {
                return BadRequest("Error al obtener las salidas");
            }
        }

        public async Task<IActionResult> SalidaInventario(SalidaMercaderia SalidaMercaderia)
        {  

            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(SalidaMercaderia);
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                var response = await _httpClient.PostAsync("/api/SalidaMercaderia/Crear", content);

                // Log the response content for debugging
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response from API: " + responseContent);


                var respArts = await _httpClient.GetAsync($"api/Articulo/ArticuloXDocumentoSalida/{SalidaMercaderia.PK_FK_Documento}");

                if (respArts.IsSuccessStatusCode)
                {
                    var dataArts = await respArts.Content.ReadAsStringAsync();
                    var articulos = JsonConvert.DeserializeObject<List<ArticuloViewModel>>(dataArts);
                    ViewData["Articulos"] = articulos ?? new List<ArticuloViewModel>();
                }
                else
                {
                    ViewData["Articulos"] = new List<ArticuloViewModel>();
                }

                if (response.IsSuccessStatusCode)
                {

                    return View("SalidaInventario");

                }
                else
                {
                    return View();
                }

            }
            return View(SalidaMercaderia);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarArticuloSalida(string PK_FK_Documento, string PK_Articulo)
        {
            if (ModelState.IsValid)
            {
                // Crear el contenido JSON con ambos parámetros
                var jsonContent = JsonConvert.SerializeObject(new { PK_FK_Documento, PK_Articulo });
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Enviar la solicitud POST a la API
                var resp = await _httpClient.PostAsync($"/api/SalidaMercaderia/Eliminar/{PK_FK_Documento}/{PK_Articulo}", content);
                string responseContent = await resp.Content.ReadAsStringAsync();
                Console.WriteLine("Response from API: " + responseContent);

                if (resp.IsSuccessStatusCode)
                {
                    return RedirectToAction("SalidaInventario");
                }

                return NotFound();
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> GuardarSalidaMercaderia(string PK)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(PK, Encoding.UTF8, "application/json");
                var resp = await _httpClient.PostAsync($"/api/SalidaMercaderia/Guardar/{PK}", content);
                string responseContent = await resp.Content.ReadAsStringAsync();
                Console.WriteLine("Response from API: " + responseContent);

                if (resp.IsSuccessStatusCode)
                {
                    return RedirectToAction("SalidaInventario"); // Devuelve 'Ingreso' en lugar de 'resp'
                }

                return NotFound();

            }
            return Ok();
        }




        //----------------Fin Codigo para Salida de Mercaderia-------------------

    }
}
