using ADM.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ADMFacturar.Controllers
{
    [Authorize(Roles = "Administrador, Usuario")]
    public class ReportesController : Controller
    {
		private readonly HttpClient _httpClient;

		public ReportesController(IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient();
			_httpClient.BaseAddress = new Uri("https://admfacturarapi-drg4cmduhxhhh8b7.centralus-01.azurewebsites.net/api");
		}
		public async Task<IActionResult> Index()
		{
			{
				var resp = await _httpClient.GetAsync("api/Reporte/ListarDashboards");

				if (resp.IsSuccessStatusCode)
				{
					var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
					var Reportes = JsonConvert.DeserializeObject<IEnumerable<Reportes>>(content);
					ViewData["Reportes"] = Reportes ?? new List<Reportes>();
					return View("Index");
				}

				return View();
			}
		}
		public async Task<IActionResult> RVentas()
		{
			var response = await _httpClient.GetAsync("api/Reporte/ReporteVentas");

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<IEnumerable<FacturaVentaViewModel>>(content);

				// Pasar los datos a la vista como modelo
				return View(result);
			}

			// Manejo de errores si la llamada a la API falla
			return View(new List<FacturaVentaViewModel>());
		}
        public async Task<IActionResult> RKardex()
        {
            var response = await _httpClient.GetAsync("api/Reporte/ReporteKardex");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<KardexViewModel>>(content);

                // Pasar los datos a la vista como modelo
                return View(result);
            }

            // Manejo de errores si la llamada a la API falla
            return View(new List<KardexViewModel>());
        }

        public async Task<IActionResult> RInventario()
		{
			var resp = await _httpClient.GetAsync("api/Reporte/ReporteArticulos");

			if (resp.IsSuccessStatusCode)
			{
				var content = await resp.Content.ReadAsStringAsync(); //Lee la respuesta del API
				var articulos = JsonConvert.DeserializeObject<IEnumerable<Articulo>>(content);
				ViewData["Articulos"] = articulos ?? new List<Articulo>();
				return View("RInventario");
			}

			return View();
		}

		public async Task<IActionResult> RVendedores()
		{
			var response = await _httpClient.GetAsync("api/Reporte/ReporteVendedores");

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<IEnumerable<ReporteVendedoresViewModel>>(content);

				// Pasar los datos a la vista como modelo
				return View(result);
			}

			// Manejo de errores si la llamada a la API falla
			return View(new List<ReporteVendedoresViewModel>());
		}


		public async Task<IActionResult> RCxC()
		{
			var response = await _httpClient.GetAsync("api/Reporte/ReporteDocumentosCC");

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<IEnumerable<DocumentosCCViewModel>>(content);

				// Pasar los datos a la vista como modelo
				return View(result);
			}

			// Manejo de errores si la llamada a la API falla
			return View(new List<DocumentosCCViewModel>());
		}


		public async Task<IActionResult> RCxP()
        {
            var resp = await _httpClient.GetAsync("api/Reporte/ReporteCXP");

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

                return View("RCxP");
            }

            return View();
        }

		public async Task<IActionResult> REstadosCuentas()
		{
			// Llamada al endpoint de la API para obtener los estados de cuentas
			var response = await _httpClient.GetAsync("api/Reporte/ReporteEstadosCuentas");

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<ReporteEstadosCuentasViewModel>(content);

				// Pasar los datos a la vista
				ViewData["Facturas"] = result.Facturas;
				ViewData["TotalPendiente"] = result.TotalPendiente;
				return View();
			}

			// Manejo de errores si la llamada a la API falla
			ViewData["Facturas"] = new List<FacturaModel>();
			ViewData["TotalPendiente"] = 0;
			return View();
		}
	}
}
