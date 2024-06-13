using ADM.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ADMFacturar.Controllers
{
    public class AdministracionController : Controller
    {
        private readonly HttpClient _httpClient;

        public AdministracionController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7270/api");
        }
        public IActionResult Index()
        {
            return View();
        }

        //public async Task<IActionResult> ListClientes()
        //{
        //    var resp = await _httpClient.GetAsync("api/Cliente/Listar2");

        //    if (resp.IsSuccessStatusCode)
        //    {
        //        var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
        //        var clientes = JsonConvert.DeserializeObject<IEnumerable<ClienteViewModel>>(content);
        //        return View("listClientes", resp);
        //    }

        //    return View(new List<ClienteViewModel>());
        //}
        public async Task<IActionResult> ListClientes()
        {
            var resp = await _httpClient.GetAsync("api/Cliente/ListarVM");

            if (resp.IsSuccessStatusCode)
            {
                var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
                var clientes = JsonConvert.DeserializeObject<IEnumerable<ClienteViewModel>>(content);
                return View("listClientes", clientes); // Devuelve 'clientes' en lugar de 'resp'
            }

            return View(new List<ClienteViewModel>());
        }

        public async Task<IActionResult> ListProvedores()
        {
			{
				var resp = await _httpClient.GetAsync("api/Proveedor/Listar");

				if (resp.IsSuccessStatusCode)
				{
					var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
					var proveedores = JsonConvert.DeserializeObject<IEnumerable<Proveedor>>(content);
					return View("ListProvedores", proveedores); 
				}

				return View(new List<Proveedor>());
			}
		}

        public IActionResult ListArticulos()
        {
            return View();
        }

        public IActionResult ListVendedores()
        {
            return View();
        }
        public async Task<IActionResult> ListTransportes()
		{
			{
				var resp = await _httpClient.GetAsync("api/Transporte/Listar");

				if (resp.IsSuccessStatusCode)
				{
					var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
					var transportes = JsonConvert.DeserializeObject<IEnumerable<Transporte>>(content);
					return View("ListTransportes", transportes);
				}

				return View(new List<Transporte>());
			}
		}
	}
}
