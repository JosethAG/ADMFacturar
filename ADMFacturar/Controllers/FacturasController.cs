using ADM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

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


        public async Task<IActionResult> GenerarPDF(string consecutivo)
        {

            List<ProductoLinea> products = new List<ProductoLinea>();

            var respPL = await _httpClient.GetAsync($"api/Facturas/ListarProductosL/{consecutivo}");
            if (respPL.IsSuccessStatusCode)
            {
                var dataProv = await respPL.Content.ReadAsStringAsync();
                var JsonPL = JsonConvert.DeserializeObject<List<ProductoLinea>>(dataProv);
                products = JsonPL ?? new List<ProductoLinea>();
            }
            else
            {
                products = new List<ProductoLinea>();
            }


            EncabezadoFactura encabezado = new EncabezadoFactura();

            var respEF = await _httpClient.GetAsync($"api/Facturas/ListarEncabezado/{consecutivo}");
            if (respEF.IsSuccessStatusCode)
            {
                var dataEF = await respEF.Content.ReadAsStringAsync();
                var JsonEF = JsonConvert.DeserializeObject<EncabezadoFactura>(dataEF);
                encabezado = JsonEF ?? new EncabezadoFactura();
            }
            else
            {
                encabezado = new EncabezadoFactura();
            }

            // Datos adicionales para la nueva sección
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);

                    page.Content().Column(column =>
                    {
                        // Columna derecha - Fecha
                        column.Item().PaddingTop(20).AlignRight().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(40);
                                columns.ConstantColumn(60);
                                columns.ConstantColumn(50);
                            });

                            table.Cell().Border(1).AlignCenter().Text("Día").FontSize(10).Bold();
                            table.Cell().Border(1).AlignCenter().Text("Mes").FontSize(10).Bold();
                            table.Cell().Border(1).AlignCenter().Text("Año").FontSize(10).Bold();

                            table.Cell().Border(1).AlignCenter().Text($"13").FontSize(10);
                            table.Cell().Border(1).AlignCenter().Text("Agosto").FontSize(10);
                            table.Cell().Border(1).AlignCenter().Text("2024").FontSize(10);
                        });

                        // Información del negocio y la imagen
                        column.Item().PaddingTop(20).Row(row =>
                        {
                            // Información del negocio
                            row.RelativeItem().Column(innerColumn =>
                            {
                                innerColumn.Item().Text("MANUEL ANTONIO DELGADO AGUERO").FontSize(16).Bold();
                                innerColumn.Item().Text("Cédula Jurídica: 204620779").FontSize(12);
                                innerColumn.Item().Text("Teléfono: 61767777").FontSize(12);
                                innerColumn.Item().Text("Correo: manueldelgadocr@hotmail.com").FontSize(12);
                                innerColumn.Item().Text("Dirección: Alajuela, Alajuela, La Trinidad").FontSize(12);
                            });

                            // Imagen
                            row.ConstantItem(100).AlignMiddle().Column(innerColumn =>
                            {
                                var imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"C:\Users\joset\Source\Repos\JosethAG\ADMFacturar\ConsoleApp1\IMPORTACIONES_MDA.png");
                                if (imagePath != null)
                                {
                                    innerColumn.Item().Image(imagePath); // Ajusta la imagen
                                }
                                else
                                {
                                    innerColumn.Item().Text("Imagen no encontrada").FontSize(12).Italic().FontColor(Colors.Red.Medium);
                                }
                            });
                        });

                        // Barra de pedido y número de factura
                        column.Item().PaddingTop(20).Background(Colors.Blue.Medium).Padding(5).Row(row =>
                        {
                            row.RelativeItem().AlignLeft().Text($"N° FACTURA: {consecutivo}")
                                .FontSize(14).Bold().FontColor(Colors.White);
                        });

                        // Cuadro de información del cliente y vendedor
                        column.Item().Padding(10).Row(infoRow =>
                        {
                            // Información del cliente
                            infoRow.RelativeItem().Column(clientColumn =>
                            {
                                clientColumn.Item().Text($"Cliente: {encabezado.Cliente}");
                                clientColumn.Item().Text($"Nombre: {encabezado.Nombre}");
                                clientColumn.Item().Text($"Identificación: {encabezado.Identificacion}");
                                clientColumn.Item().Text($"Correo: {encabezado.Correo}");
                                clientColumn.Item().Text($"Teléfono: {encabezado.Telefono}");
                            });

                            // Información del vendedor
                            infoRow.RelativeItem().Column(vendorColumn =>
                            {
                                vendorColumn.Item().Text($"Condición de Pago: {encabezado.CondicionPago}");
                                vendorColumn.Item().Text($"Vendedor: {encabezado.Vendedor}");
                                vendorColumn.Item().Text($"Transporte: {encabezado.Transporte}");
                            });
                        });

                        // Tabla de productos
                        column.Item().PaddingTop(20).Border(1).Table(table =>
                        {
                            // Definición de columnas
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(40);  // Cantidad
                                columns.ConstantColumn(100); // Código
                                columns.RelativeColumn();    // Descripción
                                columns.ConstantColumn(80);  // Precio
                                columns.ConstantColumn(80);  // Total
                            });

                            // Encabezado
                            table.Header(header =>
                            {
                                header.Cell().Background(Colors.Blue.Medium).AlignCenter().Text("Cant").FontColor(Colors.White).FontSize(12).Bold();
                                header.Cell().Background(Colors.Blue.Medium).AlignCenter().Text("Codigo").FontColor(Colors.White).FontSize(12).Bold();
                                header.Cell().Background(Colors.Blue.Medium).AlignCenter().Text("Descripción").FontColor(Colors.White).FontSize(12).Bold();
                                header.Cell().Background(Colors.Blue.Medium).AlignCenter().Text("Precio").FontColor(Colors.White).FontSize(12).Bold();
                                header.Cell().Background(Colors.Blue.Medium).AlignCenter().Text("Total").FontColor(Colors.White).FontSize(12).Bold();
                            });

                            // Filas de productos
                            foreach (var product in products)
                            {
                                table.Cell().AlignCenter().Text(product.Cantidad.ToString());
                                table.Cell().AlignCenter().Text(product.Codigo);
                                table.Cell().AlignCenter().Text(product.Descripcion);
                                table.Cell().AlignCenter().Text(product.Precio.ToString());
                                table.Cell().AlignCenter().Text(product.Total.ToString());
                            }
                            table.Cell().PaddingBottom(3);
                        });

                        // Nueva sección: Observaciones con QR y resumen
                        column.Item().Border(1).Row(row =>
                        {
                            row.RelativeItem().Column(innerColumn =>
                            {

                            });

                            // Resumen de Totales y QR
                            row.ConstantItem(200).Column(summaryColumn =>
                            {
                                summaryColumn.Item().Row(row =>
                                {
                                    row.RelativeItem().Text("Subtotal").Bold();
                                    row.ConstantItem(100).AlignRight().Text($"{encabezado.Subtotal}").Bold();
                                });

                                summaryColumn.Item().Row(row =>
                                {
                                    row.RelativeItem().Text("IVA").Bold();
                                    row.ConstantItem(100).AlignRight().Text($"{encabezado.Impuesto}").Bold();
                                });

                                summaryColumn.Item().Row(row =>
                                {
                                    row.RelativeItem().Text("Descuento").Bold();
                                    row.ConstantItem(100).AlignRight().Text($"{encabezado.Descuento}").Bold();
                                });

                                summaryColumn.Item().Row(row =>
                                {
                                    row.RelativeItem().Text("Total").Bold().FontSize(12);
                                    row.ConstantItem(100).AlignRight().Text($"{encabezado.Total}").FontSize(12).Bold();
                                });

                            });
                        });

                        // Cuentas a depositar y texto de resolución
                        column.Item().Border(1).Row(row =>
                        {
                            row.RelativeItem().PaddingLeft(5).Column(innerColumn =>
                            {
                                innerColumn.Item().Text("Cuentas a depositar:").Bold();

                                innerColumn.Item().Text("Banco Nacional:\nCta IBAN: CR94015100220010795717");
                                innerColumn.Item().Text("Banco de Costa Rica:\nCta IBAN: CR510152020205005717928");
                                innerColumn.Item().Text("BAC Credomatic:\nCta IBAN: CR49010200009544076311");
                            });
                        });
                    });
                });
            });

            // use the following invocation

            Stream stream = new MemoryStream(document.GeneratePdf());

            return File(stream, "application/pdf", $"Factura{consecutivo}.pdf");
        }


    }
}
