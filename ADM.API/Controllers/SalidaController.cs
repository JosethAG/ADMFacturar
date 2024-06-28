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
    public class SalidaMercaderiaController : Controller
    {
        private ISalidaMercaderiaService? _SalidaMercaderiaService;



        [HttpGet]
        [Route("ListarArticulosVM")]
        public IEnumerable<ArticuloViewModel> ListarArticulosVM()
        {
            DataTable tArticulo = DBData.List("sp_ListarArticulosVM");
            string jsonArticle = JsonConvert.SerializeObject(tArticulo);
            var result = JsonProvider.DeserializeSimple<IEnumerable<ArticuloViewModel>>(jsonArticle);
            return result;
        }


        //Metodos Api para salida de Mercaderia


        [HttpGet]
        [Route("ListarSalidaMercaderia")]
        public IEnumerable<SalidaMercaderia> ListaSalidaMercaderia()
        {
            DataTable tSalidaMercaderia = DBData.List("sp_ListarSalidaMercaderia");
            string jsonArticle = JsonConvert.SerializeObject(tSalidaMercaderia);
            var result = JsonProvider.DeserializeSimple<IEnumerable<SalidaMercaderia>>(jsonArticle);
            return result;
        }

        [HttpPost]
        [Route("Crear")]
        public bool CrearSalidaMercaderia(SalidaMercaderia SalidaMercaderia)
        {
            // Log received data for debugging
            Console.WriteLine("Received SalidaMercaderia: " + JsonConvert.SerializeObject(SalidaMercaderia));

            List<DBParameter> parameters = new List<DBParameter>
            {
                new DBParameter("@PK_FK_Documento", SalidaMercaderia.PK_FK_Documento),
                new DBParameter("@Fecha_Salida", SalidaMercaderia.Fecha_Salida.ToString("yyyy-MM-dd")),
                new DBParameter("@Tipo_Salida", SalidaMercaderia.Tipo_Salida.ToString()),
                new DBParameter("@Observaciones", SalidaMercaderia.Observaciones.ToString()),
                new DBParameter("@PK_FK_Articulo", SalidaMercaderia.PK_FK_Articulo),
                new DBParameter("@Cantidad", SalidaMercaderia.Cantidad.ToString()),
                new DBParameter("@FK_Usuario_Creacion", SalidaMercaderia.FK_Usuario_Creacion.ToString()),
                new DBParameter("@FK_Usuario_Modificacion", SalidaMercaderia.FK_Usuario_Modificacion.ToString()),

            };

            var result = DBData.Execute("sp_InsertarActualizarSalidaMercaderia", parameters);

            // Log the result of the database operation
            Console.WriteLine("Database operation result: " + result);

            return result;
        }

        [HttpPost]
        [Route("Eliminar/{PK_FK_Documento}/{PK_Articulo}")]
        public bool EliminarArticulo(string PK_FK_Documento, string PK_Articulo)
        {
            if (string.IsNullOrEmpty(PK_FK_Documento) || string.IsNullOrEmpty(PK_Articulo))
            {
                return false;
            }
            else
            {
                List<DBParameter> parameters = new List<DBParameter>

        {
            new DBParameter("@PK_FK_Documento", PK_FK_Documento),
            new DBParameter("@PK_FK_Articulo", PK_Articulo)
        };

                var result = DBData.Execute("sp_EliminarArticuloSalida", parameters);

                return result;
            }
        }

        [HttpPost]
        [Route("Guardar/{PK}")]
        public bool GuardarSalidaMercaderia(string PK)
        {
            if (PK == null || PK.Length == 0)
            {
                return false;
            }
            else
            {
                List<DBParameter> parameters = new List<DBParameter>
                {
                    new DBParameter("@PK_FK_Documento", PK)
                };

                var result = DBData.Execute("sp_GuardarSalidaMercaderia", parameters);

                return result;
            }
        }




    }
}
