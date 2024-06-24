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

		public async Task<IActionResult> Index()
		{
			var resp = await _httpClient.GetAsync("api/IngresoMercaderia/ListarIngresoMercaderiasVM");

			if (resp.IsSuccessStatusCode)
			{
				var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
				var Ingreso = JsonConvert.DeserializeObject<IEnumerable<IngresoMercaderiaViewModel>>(content);
				return View("Index", Ingreso); // Devuelve 'Ingreso' en lugar de 'resp'
			}

			return View(new List<IngresoMercaderiaViewModel>());
		}


		[HttpPost]
		public async Task<IActionResult> CrearIngresoMercaderia(IngresoMercaderia IngresoMercaderia)
		{
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
                    return RedirectToAction("Index"); // Devuelve 'Ingreso' en lugar de 'resp'
                }

                return NotFound();

            }
            return Ok();
        }



        //public async Task<IActionResult> ActualizarIngresoMercaderia(string? PK)
        //{
        //	var respProv = await _httpClient.GetAsync("api/Direccion/Listar/Provincia");
        //	if (respProv.IsSuccessStatusCode)
        //	{
        //		var dataProv = await respProv.Content.ReadAsStringAsync();
        //		var provincias = JsonConvert.DeserializeObject<List<Provincia>>(dataProv);
        //		ViewData["Provincias"] = provincias ?? new List<Provincia>();
        //	}
        //	else
        //	{
        //		ViewData["Provincias"] = new List<Provincia>();
        //	}

        //	var respCanton = await _httpClient.GetAsync("api/Direccion/Listar/Canton");
        //	if (respCanton.IsSuccessStatusCode)
        //	{
        //		var dataCanton = await respCanton.Content.ReadAsStringAsync();
        //		var cantones = JsonConvert.DeserializeObject<List<Canton>>(dataCanton);
        //		ViewData["Cantones"] = cantones ?? new List<Canton>();
        //	}
        //	else
        //	{
        //		ViewData["Cantones"] = new List<Canton>();
        //	}

        //	var respDist = await _httpClient.GetAsync("api/Direccion/Listar/Distrito");
        //	if (respDist.IsSuccessStatusCode)
        //	{
        //		var dataDist = await respDist.Content.ReadAsStringAsync();
        //		var distritos = JsonConvert.DeserializeObject<List<Distrito>>(dataDist);
        //		ViewData["Distritos"] = distritos ?? new List<Distrito>();
        //	}
        //	else
        //	{
        //		ViewData["Distritos"] = new List<Distrito>();
        //	}

        //	var resp = await _httpClient.GetAsync($"api/IngresoMercaderia/Obtener/{PK}");

        //	if (resp.IsSuccessStatusCode)
        //	{
        //		var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
        //		var IngresoMercaderia = JsonConvert.DeserializeObject<IngresoMercaderia>(content);
        //		return View("ActualizarIngresoMercaderia", IngresoMercaderia); // Devuelve 'Ingreso' en lugar de 'resp'
        //	}

        //	return NotFound();
        //}

        //[HttpPost]
        //public async Task<IActionResult> ActualizarIngresoMercaderia(IngresoMercaderia IngresoMercaderia)
        //{
        //	if (ModelState.IsValid)
        //	{
        //		var json = JsonConvert.SerializeObject(IngresoMercaderia);
        //		var content = new StringContent(json, Encoding.UTF8, "application/json");


        //		var response = await _httpClient.PostAsync("/api/IngresoMercaderia/Actualizar", content);

        //		// Log the response content for debugging
        //		string responseContent = await response.Content.ReadAsStringAsync();
        //		Console.WriteLine("Response from API: " + responseContent);

        //		if (response.IsSuccessStatusCode)
        //		{

        //			return RedirectToAction("Index");
        //		}
        //		else
        //		{
        //			return View();
        //		}

        //	}
        //	return View(IngresoMercaderia);
        //}

        //[HttpPost]
        //public async Task<IActionResult> DesactivarIngresoMercaderia(string PK)
        //{
        //	if (ModelState.IsValid)
        //	{
        //		var content = new StringContent(PK, Encoding.UTF8, "application/json");
        //		var resp = await _httpClient.PostAsync($"/api/IngresoMercaderia/Desactivar/{PK}", content);
        //		string responseContent = await resp.Content.ReadAsStringAsync();
        //		Console.WriteLine("Response from API: " + responseContent);

        //		if (resp.IsSuccessStatusCode)
        //		{
        //			return RedirectToAction("Index"); // Devuelve 'Ingreso' en lugar de 'resp'
        //		}

        //		return NotFound();

        //	}
        //	return Ok();
        //}
        public async Task<IActionResult> AgregarInventario()
        {
			
            return View();  // Pasa el modelo a la vista
        }

    }
}
