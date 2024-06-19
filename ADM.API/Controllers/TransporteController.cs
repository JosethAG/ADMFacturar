using ADM.Architectur;
using ADM.Architecture;
using ADM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ADM.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TransporteController : ControllerBase
	{
		[HttpGet]
		[Route("Listar")]
		public IEnumerable<Transporte> ListarTransportes(string? Nombre)
		{
			//DataTable tTransporte = DBData.List("sp_ListarTransportes");
			//string jsonTransportes = JsonConvert.SerializeObject(tTransporte);
			//var result = JsonProvider.DeserializeSimple<IEnumerable<Transporte>>(jsonTransportes);
			//return result;

			DataTable tTransporte = null;

			if (Nombre == null)
			{
				tTransporte = DBData.List("sp_ListarTransportes");
			}
			else
			{
				List<DBParameter> parameters = new List<DBParameter>
					{
						new DBParameter("@Descripcion", Nombre)
					};
				tTransporte = DBData.List("sp_ListarTransportesxNombre", parameters);
			}
			string jsonArticle = JsonConvert.SerializeObject(tTransporte);
			var result = JsonProvider.DeserializeSimple<IEnumerable<Transporte>>(jsonArticle);
			return result;
		}

		[HttpGet]
		[Route("Obtener/{PK}")]
		public Transporte ObtenerTransporte(string? PK)
		{
			if (PK != null)
			{
				List<DBParameter> parameters = new List<DBParameter>
					{
						new DBParameter("@PK_Medio_Transporte", PK)
					};
				DataTable tTransporte = DBData.List("sp_ObtenerTransporte", parameters);
				string jsonArticle = JsonConvert.SerializeObject(tTransporte);
				var result = JsonProvider.DeserializeSimple<IEnumerable<Transporte>>(jsonArticle);


				return result.FirstOrDefault();
			}
			else
			{
				return new Transporte();
			}
		}

		[HttpPost]
		[Route("Crear")]
		public bool CrearTransporte(Transporte transporte)
		{
			List<DBParameter> parameters = new List<DBParameter>
		{
			new DBParameter("@P_PK_Medio_Transporte", transporte.PK_Medio_Transporte.ToString()),
			new DBParameter("@P_Descripcion", transporte.Descripcion),
			new DBParameter("@P_Estado", transporte.Estado.ToString()),
			new DBParameter("@P_FK_Usuario_Creacion", transporte.FK_Usuario_Creacion),
			new DBParameter("@P_FK_Usuario_Modificacion", transporte.FK_Usuario_Modificacion),
			new DBParameter("@P_Fecha_Creacion", DateTime.Now.ToString("yyyy-MM-dd")),
			new DBParameter("@P_Fecha_Modificacion", DateTime.Now.ToString("yyyy-MM-dd"))
		};

			var result = DBData.Execute("sp_InsertarTransportes", parameters);

			return result;
		}

		[HttpPost]
		[Route("Actualizar")]
		public bool ActualizarTransporte(Transporte transporte)
		{
			List<DBParameter> parameters = new List<DBParameter>
		{
			new DBParameter("@P_PK_Medio_Transporte", transporte.PK_Medio_Transporte.ToString()),
			new DBParameter("@P_Descripcion", transporte.Descripcion),
			new DBParameter("@P_Estado", transporte.Estado.ToString()),
			new DBParameter("@P_FK_Usuario_Creacion", transporte.FK_Usuario_Creacion),
			new DBParameter("@P_FK_Usuario_Modificacion", transporte.FK_Usuario_Modificacion),
			new DBParameter("@P_Fecha_Creacion", DateTime.Now.ToString("yyyy-MM-dd")),
			new DBParameter("@P_Fecha_Modificacion", DateTime.Now.ToString("yyyy-MM-dd"))
		};

			var result = DBData.Execute("sp_ModificarTransportes", parameters);

			return result;
		}

		[HttpPost]
		[Route("Desactivar/{PK}")]
		public bool DesactivarTransporte(string PK)
		{
			if (PK == null || PK.Length == 0)
			{
				return false;
			}
			else
			{
				List<DBParameter> parameters = new List<DBParameter>
				{
					new DBParameter("@P_PK_Medio_Transporte", PK)
				};

				var result = DBData.Execute("sp_EliminarTransportes", parameters);

				return result;
			}
		}
	}
}
