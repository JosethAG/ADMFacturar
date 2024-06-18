using ADM.Architectur;
using ADM.Architecture;
using ADM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace ADM.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DireccionController : ControllerBase
	{
		[HttpGet]
		[Route("Listar/Provincia")]
		public IEnumerable<Provincia> ListaProvincia()
		{
			DataTable Dtable = DBData.List("sp_ListarProvincias");
			string json = JsonConvert.SerializeObject(Dtable);
			var result = JsonProvider.DeserializeSimple<IEnumerable<Provincia>>(json);
			return result;
		}

		[HttpGet]
		[Route("Listar/Canton")]
		public IEnumerable<Canton> ListaCanton()
		{
			DataTable Dtable = DBData.List("sp_ListarCantones");
			string json = JsonConvert.SerializeObject(Dtable);
			var result = JsonProvider.DeserializeSimple<IEnumerable<Canton>>(json);
			return result;
		}

		[HttpGet]
		[Route("Listar/Distrito")]
		public IEnumerable<Distrito> ListaDistrito()
		{
			DataTable Dtable = DBData.List("sp_ListarDistritos");
			string json = JsonConvert.SerializeObject(Dtable);
			var result = JsonProvider.DeserializeSimple<IEnumerable<Distrito>>(json);
			return result;
		}


	}
}
