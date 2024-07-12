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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ADM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CXCController : Controller
    {
        private ICXCService? _CXCService;

        [HttpGet]
        [Route("Listar")]
        public IEnumerable<CXC> ListaCXPs(string? Nombre)
        {
            DataTable tCXP = null;

            if (Nombre == null)
            {
                tCXP = DBData.List("sp_ListarDocumentosCC");
            }
            else
            {
                List<DBParameter> parameters = new List<DBParameter>
                    {
                        new DBParameter("@Nombre", Nombre)
                    };
                tCXP = DBData.List("sp_ListarDocumentosCCxCliente", parameters);
            }
            string jsonArticle = JsonConvert.SerializeObject(tCXP);
            var result = JsonProvider.DeserializeSimple<IEnumerable<CXC>>(jsonArticle);
            return result;
        }


        [HttpGet]
        [Route("Obtener/{PK}")]
        public CXC ObtenerCXC(string? PK)
        {
            if (PK != null)
            {
                List<DBParameter> parameters = new List<DBParameter>
                    {
                        new DBParameter("@PK_Documento_CC", PK)
                    };
                DataTable tCXC = DBData.List("sp_ObtenerDocumentoCC", parameters);
                string jsonArticle = JsonConvert.SerializeObject(tCXC);
                var result = JsonProvider.DeserializeSimple<IEnumerable<CXC>>(jsonArticle);


                return result.FirstOrDefault();
            }
            else
            {
                return new CXC();
            }
        }

        [HttpGet]
        [Route("ListarAbonos/{PK_Documento_CC}")]
        public IEnumerable<AbonoCXC> ListarAbonosPorDocumentoCC(string PK_Documento_CC)
        {
            // Añadir un log para verificar el valor del parámetro
            Console.WriteLine($"Valor de PK_Documento_CC: {PK_Documento_CC}");

            // Crear y verificar los parámetros
            List<DBParameter> parameters = new List<DBParameter>
            {
                new DBParameter("@FK_Documento_CC", PK_Documento_CC)  // Cambia aquí a "@FK_Documento"
            };

            // Ejecutar el procedimiento almacenado
            DataTable tAbonos = DBData.List("sp_ListarAbonosxDocumentoCC", parameters);

            // Verificar si el DataTable es null
            if (tAbonos == null)
            {
                Console.WriteLine("El DataTable tAbonos es null.");
                return Enumerable.Empty<AbonoCXC>();
            }

            // Verificar si el DataTable tiene filas
            if (tAbonos.Rows.Count == 0)
            {
                Console.WriteLine("El DataTable tAbonos no tiene filas.");
                return Enumerable.Empty<AbonoCXC>();
            }

            // Serializar el DataTable a JSON
            string jsonAbonos = JsonConvert.SerializeObject(tAbonos);
            Console.WriteLine($"JSON serializado: {jsonAbonos}");

            // Deserializar el JSON a IEnumerable<AbonoCXP>
            var result = JsonProvider.DeserializeSimple<IEnumerable<AbonoCXC>>(jsonAbonos);
            return result;
        }

        [HttpPost]
        [Route("Crear")]
        public IActionResult CrearAbono([FromBody] AbonoCXC abono)
        {
            if (ModelState.IsValid)
            {
                List<DBParameter> parameters = new List<DBParameter>
            {
                new DBParameter("@Numero_Abono", abono.Numero_Abono), // Asegúrate de que coincida con el nombre del parámetro en tu procedimiento almacenado
                new DBParameter("@FK_Documento_CC", abono.FK_Documento_CC),
                new DBParameter("@Monto_Abonado", abono.Monto_Abonado.ToString()), // Asegúrate de que DBParameter maneje bien el tipo decimal
                new DBParameter("@Tipo_Pago", abono.Tipo_Pago),
                new DBParameter("@Banco", abono.Banco)
            };

                try
                {
                    // Ejecución del procedimiento almacenado
                    int result = DBData.ExecuteCC("sp_InsertarAbonoXC", parameters);

                    // Verificación del resultado
                    if (result == 1)
                    {
                        // Abono exitoso
                        return Ok(1);
                    }
                    else if (result == 3)
                    {
                        // Saldo pendiente es menor o igual a cero
                        return BadRequest(3);
                    }
                    else if (result == 4)
                    {
                        // Monto abonado es un numero negativo
                        return BadRequest(4);
                    }
                    else if (result == 5)
                    {
                        // Saldo pendiente es igual a 0
                        return BadRequest(5);
                    }
                    else if (result == 2)
                    {
                        // Monto abonado es mayor que el saldo pendiente
                        return BadRequest(2);
                    }
                    else
                    {
                        // Error desconocido al insertar el abono en la base de datos
                        return StatusCode(500, "Error desconocido al insertar el abono en la base de datos.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return StatusCode(500, "Error al insertar el abono en la base de datos. " + ex.Message);
                }
            }

            return BadRequest(ModelState);
        }
    }
}

