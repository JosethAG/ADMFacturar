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
using Microsoft.AspNetCore.Http.HttpResults;

namespace ADM.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    public class ArticuloController : Controller
	{
		private IArticuloService? _ArticuloService;

		[HttpGet]
		[Route("Listar")]
		public IEnumerable<Articulo> ListaArticulos(string? Nombre)
		{
			DataTable tArticulo = null;

			if (Nombre == null)
			{
				tArticulo = DBData.List("sp_ListarArticulos");
			}
			else
			{
				List<DBParameter> parameters = new List<DBParameter>
					{
						new DBParameter("@Descripcion", Nombre)
					};
				tArticulo = DBData.List("sp_ListarArticulosxNombre", parameters);
			}
			string jsonArticle = JsonConvert.SerializeObject(tArticulo);
			var result = JsonProvider.DeserializeSimple<IEnumerable<Articulo>>(jsonArticle);
			return result;
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

		[HttpGet]
		[Route("ArticuloXDocumento/{id}")]
		public IEnumerable<ArticuloViewModel> ArticuloXDocumento(string? id)
		{
			List<DBParameter> parameters = new List<DBParameter>
					{
						new DBParameter("@PK_FK_Documento", id)
					};
			DataTable tArticulo = DBData.List("sp_ListarIngresoPorDoc", parameters);

			string jsonArticle = JsonConvert.SerializeObject(tArticulo);
			var result = JsonProvider.DeserializeSimple<IEnumerable<ArticuloViewModel>>(jsonArticle);
			return result;
		}
        [HttpGet]
        [Route("ArticuloXDocumentoSalida/{id}")]
        public IEnumerable<ArticuloViewModel> ArticuloXDocumentoSalida(string? id)
        {
            List<DBParameter> parameters = new List<DBParameter>
                    {
                        new DBParameter("@PK_FK_Documento", id)
                    };
            DataTable tArticulo = DBData.List("sp_ListarSalidaPorDoc", parameters);

            string jsonArticle = JsonConvert.SerializeObject(tArticulo);
            var result = JsonProvider.DeserializeSimple<IEnumerable<ArticuloViewModel>>(jsonArticle);
            return result;
        }

        [HttpGet]
		[Route("Obtener/{PK}")]
		public Articulo ObtenerArticulo(string? PK)
		{
			if (PK != null)
			{
				List<DBParameter> parameters = new List<DBParameter>
					{
						new DBParameter("@PK_Articulo", PK)
					};
				DataTable tArticulo = DBData.List("sp_ObtenerArticulo", parameters);
				string jsonArticle = JsonConvert.SerializeObject(tArticulo);
				var result = JsonProvider.DeserializeSimple<IEnumerable<Articulo>>(jsonArticle);
				Articulo art = result.FirstOrDefault();

				return art;
			}
			else
			{
				return new Articulo();
			}
		}

		[HttpPost]
		[Route("Crear")]
		public bool CrearArticulo(Articulo articulo)
		{
			List<DBParameter> parameters = new List<DBParameter>
			{
				new DBParameter("@P_PK_Articulo", articulo.PK_Articulo),
				new DBParameter("@P_Descripcion", articulo.Descripcion),
				new DBParameter("@P_Codigo_Barras", articulo.Codigo_Barras),
				new DBParameter("@P_FK_Proveedor", articulo.FK_Proveedor),
				new DBParameter("@P_Cantidad", articulo.Cantidad.ToString()),
				new DBParameter("@P_Costo", articulo.Costo.ToString()),
				new DBParameter("@P_Precio", articulo.Precio.ToString()),
				new DBParameter("@P_Estado", articulo.Estado.ToString()),
				new DBParameter("@P_FK_Usuario_Creacion", articulo.FK_Usuario_Creacion),
				new DBParameter("@P_FK_Usuario_Modificacion", articulo.FK_Usuario_Modificacion),
				new DBParameter("@P_Fecha_Creacion", DateTime.Now.ToString("yyyy-MM-dd")),
				new DBParameter("@P_Fecha_Modificacion", DateTime.Now.ToString("yyyy-MM-dd"))
			};

			var result = DBData.Execute("sp_InsertarArticulos", parameters);

			return result;
		}


		[HttpPost]
		[Route("Actualizar")]
		public bool ActualizarArticulo(Articulo articulo)
		{
			List<DBParameter> parameters = new List<DBParameter>
			{
				new DBParameter("@P_PK_Articulo", articulo.PK_Articulo),
				new DBParameter("@P_Descripcion", articulo.Descripcion),
				new DBParameter("@P_Codigo_Barras", articulo.Codigo_Barras),
				new DBParameter("@P_FK_Proveedor", articulo.FK_Proveedor),
				new DBParameter("@P_Cantidad", articulo.Cantidad.ToString()),
				new DBParameter("@P_Costo", articulo.Costo.ToString()),
				new DBParameter("@P_Precio", articulo.Precio.ToString()),
				new DBParameter("@P_Estado", articulo.Estado.ToString()),
				new DBParameter("@P_FK_Usuario_Modificacion", articulo.FK_Usuario_Modificacion.ToString()),
				new DBParameter("@P_Fecha_Modificacion", DateTime.Now.ToString("yyyy-MM-dd"))
			};

			var result = DBData.Execute("sp_ModificarArticulos", parameters);

			return result;
		}


		[HttpPost]
		[Route("Desactivar/{PK}")]
		public bool DesactivarArticulo(string PK)
		{
			if (PK == null || PK.Length == 0)
			{
				return false;
			}
			else
			{
				List<DBParameter> parameters = new List<DBParameter>
				{
					new DBParameter("@P_PK_Articulo", PK)
				};

				var result = DBData.Execute("sp_EliminarArticulos", parameters);

				return result;
			}
		}
	}
}

