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
    public class IngresoMercaderiaController : Controller
    {
        private IIngresoMercaderiaService? _IngresoMercaderiaService;

        [HttpGet]
        [Route("Listar")]
        public IEnumerable<IngresoMercaderia> ListaIngresoMercaderias(string? Nombre)
        {
            DataTable tIngresoMercaderia = null;

            if (Nombre == null)
            {
                tIngresoMercaderia = DBData.List("sp_ListarIngresoMercaderia");
            }
            else
            {
                List<DBParameter> parameters = new List<DBParameter>
                    {
                        new DBParameter("@Nombre", Nombre)
                    };
                tIngresoMercaderia = DBData.List("sp_ListarIngresoMercaderiaxNombre", parameters);
            }
            string jsonArticle = JsonConvert.SerializeObject(tIngresoMercaderia);
            var result = JsonProvider.DeserializeSimple<IEnumerable<IngresoMercaderia>>(jsonArticle);
            return result;
        }

        [HttpGet]
        [Route("Obtener/{PK}")]
        public IngresoMercaderia ObtenerIngresoMercaderia(string? PK) 
        {
            if (PK != null)
            {
				List<DBParameter> parameters = new List<DBParameter>
					{
						new DBParameter("@PK_IngresoMercaderia", PK)
					};
				DataTable tIngresoMercaderia = DBData.List("sp_ObtenerIngresoMercaderia", parameters);
                string jsonArticle = JsonConvert.SerializeObject(tIngresoMercaderia);
                var result = JsonProvider.DeserializeSimple<IEnumerable<IngresoMercaderia>>(jsonArticle);


                return result.FirstOrDefault();
            }
            else
            {
                return new IngresoMercaderia();
            }
		}


        [HttpGet]
        [Route("ListarIngresoMercaderiasVM")]
        public IEnumerable<IngresoMercaderiaViewModel> ListaIngresoMercaderiasVM()
        {
            DataTable tIngresoMercaderia = DBData.List("sp_ListarIngresoMercaderiaVM");
            string jsonArticle = JsonConvert.SerializeObject(tIngresoMercaderia);
            var result = JsonProvider.DeserializeSimple<IEnumerable<IngresoMercaderiaViewModel>>(jsonArticle);
            return result;
        }

        [HttpPost]
        [Route("Crear")]
        public bool CrearIngresoMercaderia(IngresoMercaderia IngresoMercaderia)
        {
			// Log received data for debugging
			Console.WriteLine("Received IngresoMercaderia: " + JsonConvert.SerializeObject(IngresoMercaderia));

			List<DBParameter> parameters = new List<DBParameter>
            {
                new DBParameter("@PK_FK_Documento", IngresoMercaderia.PK_FK_Documento),
                new DBParameter("@PK_FK_Proveedor", IngresoMercaderia.PK_FK_Proveedor.ToString()),
                new DBParameter("@FK_CondicionPago", IngresoMercaderia.FK_CondicionPago.ToString()),
                new DBParameter("@Fecha_Compra", IngresoMercaderia.Fecha_Compra.ToString("yyyy-MM-dd")),
                new DBParameter("@PK_FK_Articulo", IngresoMercaderia.PK_FK_Articulo),
                new DBParameter("@Cantidad", IngresoMercaderia.Cantidad.ToString()),
                new DBParameter("@Costo", IngresoMercaderia.Costo.ToString()),
                new DBParameter("@FK_Usuario_Creacion", IngresoMercaderia.FK_Usuario_Creacion.ToString()),
                new DBParameter("@FK_Usuario_Modificacion", IngresoMercaderia.FK_Usuario_Modificacion.ToString()),

            };

            var result = DBData.Execute("sp_InsertarActualizarIngresoMercaderia", parameters);

			// Log the result of the database operation
			Console.WriteLine("Database operation result: " + result);

			return result;
        }


        [HttpPost]
        [Route("Guardar/{PK}")]
        public bool GuardarIngresoMercaderia(string PK)
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

                var result = DBData.Execute("sp_GuardarIngresoMercaderia", parameters);

                return result;
            }
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

                var result = DBData.Execute("sp_EliminarArticuloIngreso", parameters);

                return result;
            }
        }

        [HttpGet]
        [Route("ListarArticulosVM")]
        public IEnumerable<ArticuloViewModel> ListarArticulosVM()
        {
            DataTable tArticulo = DBData.List("sp_ListarArticulosVM");
            string jsonArticle = JsonConvert.SerializeObject(tArticulo);
            var result = JsonProvider.DeserializeSimple<IEnumerable<ArticuloViewModel>>(jsonArticle);
            return result;
        }


        [HttpPost]
        [Route("EliminarIngreso/{PK_FK_Documento}")]
        public bool EliminarIngreso(string PK_FK_Documento)
        {
            if (string.IsNullOrEmpty(PK_FK_Documento))
            {
                return false;
            }
            else
            {
                List<DBParameter> parameters = new List<DBParameter>

        {
            new DBParameter("@PK_FK_Documento", PK_FK_Documento),
        };

                var result = DBData.Execute("sp_EliminarIngreso", parameters);

                return result;
            }
        }




        //Metodos Api para salida de Mercaderia


        //[HttpGet]
        //[Route("ListarSalidaMercaderia")]
        //public IEnumerable<SalidaMercaderia> ListaSalidaMercaderia()
        //{
        //    DataTable tSalidaMercaderia = DBData.List("sp_ListarSalidaMercaderia");
        //    string jsonArticle = JsonConvert.SerializeObject(tSalidaMercaderia);
        //    var result = JsonProvider.DeserializeSimple<IEnumerable<SalidaMercaderia>>(jsonArticle);
        //    return result;
        //}


    }
}
