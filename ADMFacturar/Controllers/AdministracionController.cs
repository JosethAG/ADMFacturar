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

        public IActionResult ListProvedores()
        {
            return View();
        }

        public IActionResult ListArticulos()
        {
            return View();
        }

        public IActionResult ListVendedores()
        {
            return View();
        }
        public IActionResult ListTransportes()
        {
            return View();
        }

    }
}
