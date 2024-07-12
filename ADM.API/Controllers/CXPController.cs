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
    public class CXPController : Controller
    {
        private ICXPService? _CXPService;

        [HttpGet]
        [Route("Listar")]
        public IEnumerable<CXP> ListaCXPs(string? Nombre)
        {
            DataTable tCXP = null;

            if (Nombre == null)
            {
                tCXP = DBData.List("sp_ListarDocumentosCP");
            }
            else
            {
                List<DBParameter> parameters = new List<DBParameter>
                    {
                        new DBParameter("@Nombre", Nombre)
                    };
                tCXP = DBData.List("sp_ListarDocumentosCPxProveedor", parameters);
            }
            string jsonArticle = JsonConvert.SerializeObject(tCXP);
            var result = JsonProvider.DeserializeSimple<IEnumerable<CXP>>(jsonArticle);
            return result;
        }


        [HttpGet]
        [Route("Obtener/{PK}")]
        public CXP ObtenerCXP(string? PK)
        {
            if (PK != null)
            {
                List<DBParameter> parameters = new List<DBParameter>
                    {
                        new DBParameter("@PK_Documento", PK)
                    };
                DataTable tCXP = DBData.List("sp_ObtenerDocumentoCP", parameters);
                string jsonArticle = JsonConvert.SerializeObject(tCXP);
                var result = JsonProvider.DeserializeSimple<IEnumerable<CXP>>(jsonArticle);


                return result.FirstOrDefault();
            }
            else
            {
                return new CXP();
            }
        }

        [HttpGet]
        [Route("ListarAbonos/{PK_Documento}")]
        public IEnumerable<AbonoCXP> ListarAbonosPorDocumento(string PK_Documento)
        {
            // Añadir un log para verificar el valor del parámetro
            Console.WriteLine($"Valor de PK_Documento: {PK_Documento}");

            // Crear y verificar los parámetros
            List<DBParameter> parameters = new List<DBParameter>
            {
                new DBParameter("@FK_Documento", PK_Documento)  // Cambia aquí a "@FK_Documento"
            };

            // Ejecutar el procedimiento almacenado
            DataTable tAbonos = DBData.List("sp_ListarAbonosxDocumento", parameters);

            // Verificar si el DataTable es null
            if (tAbonos == null)
            {
                Console.WriteLine("El DataTable tAbonos es null.");
                return Enumerable.Empty<AbonoCXP>();
            }

            // Verificar si el DataTable tiene filas
            if (tAbonos.Rows.Count == 0)
            {
                Console.WriteLine("El DataTable tAbonos no tiene filas.");
                return Enumerable.Empty<AbonoCXP>();
            }

            // Serializar el DataTable a JSON
            string jsonAbonos = JsonConvert.SerializeObject(tAbonos);
            Console.WriteLine($"JSON serializado: {jsonAbonos}");

            // Deserializar el JSON a IEnumerable<AbonoCXP>
            var result = JsonProvider.DeserializeSimple<IEnumerable<AbonoCXP>>(jsonAbonos);
            return result;
        }

        [HttpPost]
        [Route("Crear")]
        public IActionResult CrearAbono([FromBody] AbonoCXP abono)
        {
            if (ModelState.IsValid)
            {
                List<DBParameter> parameters = new List<DBParameter>
            {
                new DBParameter("@Numero_Recibo", abono.Numero_Recibo), // Asegúrate de que coincida con el nombre del parámetro en tu procedimiento almacenado
                new DBParameter("@FK_Documento", abono.FK_Documento),
                new DBParameter("@Monto_Abonado", abono.Monto_Abonado.ToString()), // Sin convertir a string
                new DBParameter("@Tipo_Documento", abono.Tipo_Documento),
                new DBParameter("@Banco", abono.Banco),
                new DBParameter("@Fecha_Abono", DateTime.Now.ToString("yyyy-MM-dd"))
            };

                try
                {
                    // Ejecución del procedimiento almacenado
                    int result = DBData.ExecuteCP("sp_InsertarAbono", parameters);

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
                    else if (result == 6)
                    {
                        // El Numero_Recibo ya existe para ese documento
                        return BadRequest(6);
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

