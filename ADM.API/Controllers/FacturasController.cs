using System.Data.SqlClient;
using System.Data;
using ADM.API;
using ADM.Models;
using ADM.Service;
using Microsoft.AspNetCore.Mvc;
using ADM.Interface;
using ADM.Architectur;
using ADM.Architecture;
using Newtonsoft.Json;


namespace ADM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : Controller
    {
        //private IFacturasService? _FacturasService;

        [HttpGet]
        [Route("Listar")]
        public IEnumerable<FacturaVM> ListarFacturas(string? Nombre)
        {
            DataTable tFacturas = null;

            if (Nombre == null)
            {
                tFacturas = DBData.List("sp_ListarFacturas");
            }
            else
            {
                List<DBParameter> parameters = new List<DBParameter>
                          {
                              new DBParameter("@Nombre", Nombre)
                          };
                tFacturas = DBData.List("sp_ListarFacturas", parameters);
            }
            string jsonArticle = JsonConvert.SerializeObject(tFacturas);
            var result = JsonProvider.DeserializeSimple<IEnumerable<FacturaVM>>(jsonArticle);
            return result;
        }

        [HttpGet]
        [Route("ObtenerConsecutivo/{PK}")]
        public ConsecutivoViewModel ObtenerConsecutivo(string PK)
        {
            if (!string.IsNullOrEmpty(PK))
            {
                List<DBParameter> parameters = new List<DBParameter>
        {
            new DBParameter("@PK_Consecutivo", PK)
        };
                DataTable tConsecutivo = DBData.List("sp_ObtenerConsecutivo", parameters);
                string jsonArticle = JsonConvert.SerializeObject(tConsecutivo);
                var result = JsonProvider.DeserializeSimple<IEnumerable<ConsecutivoViewModel>>(jsonArticle);

                return result.FirstOrDefault();
            }
            else
            {
                return new ConsecutivoViewModel();
            }
        }

        [HttpPost]
        [Route("Crear")]
        public bool CrearFactura([FromBody] FacturaViewModel factura)
        {
            // Log received data for debugging
            Console.WriteLine("Received Factura: " + JsonConvert.SerializeObject(factura));

            // Parameters for the Factura table
            List<DBParameter> facturaParameters = new List<DBParameter>
        {
            new DBParameter("@PK_Factura", factura.Encabezado.Consecutivo),
            new DBParameter("@FK_Cliente", factura.Encabezado.Cliente),
            new DBParameter("@FK_Condicion_Pago", factura.Encabezado.CondicionPago.ToString()),
            new DBParameter("@Transporte", factura.Encabezado.Transporte.ToString()),
            new DBParameter("@Subtotal", factura.Subtotal.ToString()),
            new DBParameter("@Descuento", factura.Descuento.ToString()),
            new DBParameter("@Impuesto", factura.Impuesto.ToString()),
            new DBParameter("@Total", factura.Total.ToString()),
        };

            var result = DBData.Execute("sp_InsertarFactura", facturaParameters);

            // If inserting the factura is successful, insert the products
            if (!result)
            {
                foreach (var producto in factura.Productos)
                {
                    List<DBParameter> productoParameters = new List<DBParameter>
                {
                    new DBParameter("@PK_FK_Factura", factura.Encabezado.Consecutivo),
                    new DBParameter("@FK_Articulo", producto.Producto),
                    new DBParameter("@Cantidad", producto.Cantidad.ToString()),
                    new DBParameter("@Precio", producto.precioUnitario.ToString()),
                };

                    var productoResult = DBData.Execute("sp_InsertarFacturaLinea", productoParameters);

                    if (productoResult)
                    {
                        // Log and return false if any product insertion fails
                        Console.WriteLine("Failed to insert product: " + JsonConvert.SerializeObject(producto));
                        return false;
                    }
                }
            }
            else
            {
                // Log and return false if inserting the factura fails
                Console.WriteLine("Failed to insert factura");
                return false;
            }

            // Log the result of the database operation
            Console.WriteLine("Database operation result: " + !result);

            return !result;
        }

        [HttpPost]
        [Route("CrearNC")]
        public bool CrearNC([FromBody] FacturaViewModel factura)
        {
            // Log received data for debugging
            Console.WriteLine("Received Factura: " + JsonConvert.SerializeObject(factura));

            // Parameters for the Factura table
            List<DBParameter> facturaParameters = new List<DBParameter>
        {
            new DBParameter("@PK_Factura", factura.Encabezado.Consecutivo),
            new DBParameter("@Fac_Original", factura.Encabezado.facturaOriginal),
            new DBParameter("@FK_Cliente", factura.Encabezado.Cliente),
            new DBParameter("@Comentario", factura.Encabezado.comentario),
            new DBParameter("@Motivo", factura.Encabezado.motivo),
            new DBParameter("@Total", factura.Total.ToString()),


        };

            var result = DBData.Execute("sp_InsertarNC", facturaParameters);

            // If inserting the factura is successful, insert the products
            if (!result)
            {
                foreach (var producto in factura.Productos)
                {
                    List<DBParameter> productoParameters = new List<DBParameter>
                {
                    new DBParameter("@PK_FK_Factura", factura.Encabezado.Consecutivo),
                    new DBParameter("@FK_Articulo", producto.Producto),
                    new DBParameter("@Cantidad", producto.Cantidad.ToString()),
                };

                    var productoResult = DBData.Execute("sp_InsertarNCLinea", productoParameters);

                    if (productoResult)
                    {
                        // Log and return false if any product insertion fails
                        Console.WriteLine("Failed to insert product: " + JsonConvert.SerializeObject(producto));
                        return false;
                    }
                }
            }
            else
            {
                // Log and return false if inserting the factura fails
                Console.WriteLine("Failed to insert factura");
                return false;
            }

            // Log the result of the database operation
            Console.WriteLine("Database operation result: " + !result);

            return !result;
        }




        [HttpGet]
        [Route("ObtenerProducto/{producto}")]
        public ProductoViewModel ObtenerProducto(string? producto)
        {
            if (producto != null)
            {
                List<DBParameter> parameters = new List<DBParameter>
                    {
                        new DBParameter("@PK_Articulo", producto)
                    };
                DataTable tProducto = DBData.List("sp_obtenerProductoFacturacion", parameters);
                string jsonArticle = JsonConvert.SerializeObject(tProducto);
                var result = JsonProvider.DeserializeSimple<IEnumerable<ProductoViewModel>>(jsonArticle);


                return result.FirstOrDefault();
            }
            else
            {
                return new ProductoViewModel();
            }
        }

        [HttpGet]
        [Route("Obtener/{consecutivo}")]
        public ActionResult<FacturaViewModel> ObtenerFactura(string consecutivo)
        {
            // Log received consecutivo for debugging
            Console.WriteLine("Received Consecutivo: " + consecutivo);

            // Query for Encabezado
            List<DBParameter> encabezadoParameters = new List<DBParameter>
        {
            new DBParameter("@PK_Factura", consecutivo)
        };

            var encabezadoTable = DBData.List("sp_ObtenerFacturaEncabezado", encabezadoParameters);

            if (encabezadoTable == null || encabezadoTable.Rows.Count == 0)
            {
                return NotFound("Factura no encontrada");
            }

            var encabezadoRow = encabezadoTable.Rows[0];
            var encabezado = new Encabezado
            {
                Consecutivo = encabezadoRow["PK_Factura"].ToString(),
                Cliente = encabezadoRow["FK_Cliente"].ToString(),
                CondicionPago = encabezadoRow["FK_Condicion_Pago"].ToString(),
                Transporte = encabezadoRow["Transporte"].ToString(),
                facturaOriginal = encabezadoRow["facturaOriginal"].ToString(),
                comentario = encabezadoRow["comentario"].ToString(),
                motivo = encabezadoRow["motivo"].ToString(),

            };

            // Query for Productos
            List<DBParameter> productoParameters = new List<DBParameter>
        {
            new DBParameter("@PK_FK_Factura", consecutivo)
        };

            var productosTable = DBData.List("sp_ObtenerFacturaProductos", productoParameters);

            var productos = productosTable.AsEnumerable().Select(productoRow => new ProductoViewModel
            {
                Producto = productoRow["FK_Articulo"].ToString(),
                Cantidad = int.Parse(productoRow["Cantidad"].ToString()),
                precioUnitario = decimal.Parse(productoRow["Precio"].ToString())
            }).ToList();

            // Query for Subtotal, Descuento, and Total
            List<DBParameter> totalParameters = new List<DBParameter>
        {
            new DBParameter("@PK_Factura", consecutivo)
        };

            var totalTable = DBData.List("sp_ObtenerFacturaTotales", totalParameters);

            if (totalTable == null || totalTable.Rows.Count == 0)
            {
                return NotFound("Totales no encontrados para la factura");
            }

            var totalRow = totalTable.Rows[0];

            var facturaViewModel = new FacturaViewModel
            {
                Encabezado = encabezado,
                Subtotal = decimal.Parse(totalRow["Subtotal"].ToString()),
                Descuento = decimal.Parse(totalRow["Descuento"].ToString()),
                Total = decimal.Parse(totalRow["Total"].ToString()),
                Impuesto = decimal.Parse(totalRow["Impuesto"].ToString()),
                Productos = productos
            };

            return Ok(facturaViewModel);
        }


        [HttpGet]
        [Route("ListarProductosL/{consecutivo}")]
        public IEnumerable<ProductoLinea> ListarProductosL(string? consecutivo)
        {
            if (consecutivo != null)
            {
                List<DBParameter> parameters = new List<DBParameter>
                    {
                        new DBParameter("@consecutivo", consecutivo)
                    };
                DataTable tProducto = DBData.List("sp_rep_factura_linea", parameters);
                string jsonArticle = JsonConvert.SerializeObject(tProducto);
                var result = JsonProvider.DeserializeSimple<IEnumerable<ProductoLinea>>(jsonArticle);

                return result;
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        [Route("ListarEncabezado/{consecutivo}")]
        public EncabezadoFactura ListarEncabezado(string? consecutivo)
        {
            if (consecutivo != null)
            {
                List<DBParameter> parameters = new List<DBParameter>
                    {
                        new DBParameter("@consecutivo", consecutivo)
                    };
                DataTable tProducto = DBData.List("sp_rep_factura", parameters);
                string jsonArticle = JsonConvert.SerializeObject(tProducto);
                var result = JsonProvider.DeserializeSimple<IEnumerable<EncabezadoFactura>>(jsonArticle);

                return result.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
    }

}

