using ADM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ADMFacturar.Controllers
{
    [Authorize(Roles = "Administrador, Usuario")]
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
            var resp = await _httpClient.GetAsync("api/Cliente/ListarClientesVM");

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

        //-----------------------------------------------------------------------------------------------------

        public async Task<IActionResult> ListArticulos()
        {
            var resp = await _httpClient.GetAsync("api/Articulo/ListarArticulosVM");

            if (resp.IsSuccessStatusCode)
            {
                var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
                var articulos = JsonConvert.DeserializeObject<IEnumerable<ArticuloViewModel>>(content);
                return View("listArticulos", articulos); // Devuelve 'articulos' en lugar de 'resp'
            }

            return View(new List<ArticuloViewModel>());
        }

        public async Task<IActionResult> ListVendedores()
        {
            {
                var resp = await _httpClient.GetAsync("api/Vendedor/Listar");

                if (resp.IsSuccessStatusCode)
                {
                    var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
                    var vendedores = JsonConvert.DeserializeObject<IEnumerable<Vendedor>>(content);
                    return View("ListVendedores", vendedores);
                }

                return View(new List<Vendedor>());
            }
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
