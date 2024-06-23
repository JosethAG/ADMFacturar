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

        //[HttpPost]
        //[Route("Actualizar")]
        //public bool ActualizarIngresoMercaderia(IngresoMercaderia IngresoMercaderia)
        //{
        //    List<DBParameter> parameters = new List<DBParameter>
        //    {
        //        new DBParameter("@P_PK_IngresoMercaderia", IngresoMercaderia.PK_IngresoMercaderia),
        //        new DBParameter("@P_TipoIdentificacion", IngresoMercaderia.TipoIdentificacion),
        //        new DBParameter("@P_Identificacion", IngresoMercaderia.Identificacion.ToString()),
        //        new DBParameter("@P_Nombre", IngresoMercaderia.Nombre),
        //        new DBParameter("@P_Telefono", IngresoMercaderia.Telefono),
        //        new DBParameter("@P_Correo", IngresoMercaderia.Correo),
        //        new DBParameter("@P_Provincia", IngresoMercaderia.Provincia),
        //        new DBParameter("@P_Canton", IngresoMercaderia.Canton),
        //        new DBParameter("@P_Distrito", IngresoMercaderia.Distrito),
        //        new DBParameter("@P_OtrasSenas", IngresoMercaderia.OtrasSenas),
        //        new DBParameter("@P_Estado", IngresoMercaderia.Estado.ToString()),
        //        new DBParameter("@P_FK_CondicionPago", IngresoMercaderia.FK_CondicionPago.ToString()),
        //        new DBParameter("@P_FK_Transporte", IngresoMercaderia.FK_Transporte.ToString()),
        //        new DBParameter("@P_FK_Vendedor", IngresoMercaderia.FK_Vendedor.ToString()),
        //        new DBParameter("@P_FK_Usuario_Modificacion", IngresoMercaderia.FK_Usuario_Modificacion.ToString()),
        //        new DBParameter("@P_Fecha_Modificacion", DateTime.Now.ToString("yyyy-MM-dd"))
        //    };

        //    var result = DBData.Execute("sp_ModificarIngresoMercaderias", parameters);

        //    return result;
        //}

        [HttpPost]
        [Route("Desactivar/{PK}")]
        public bool DesactivarIngresoMercaderia(string PK)
        {
            if (PK == null || PK.Length == 0)
            {
                return false;
            }
            else
            {
                List<DBParameter> parameters = new List<DBParameter>
                {
                    new DBParameter("@P_PK_IngresoMercaderia", PK)
                };
                
                var result = DBData.Execute("sp_EliminarIngresoMercaderias", parameters);

                return result;
            }
        }
    }
}
