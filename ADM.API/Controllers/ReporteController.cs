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
	public class ReporteController : Controller
	{
		private IArticuloService? _ArticuloService;

		[HttpGet]
		[Route("ReporteArticulos")]
		public IEnumerable<ArticuloViewModel> ListarArticulosVM()
		{
			DataTable tArticulo = DBData.List("sp_ListarArticulosVM");
			string jsonArticle = JsonConvert.SerializeObject(tArticulo);
			var result = JsonProvider.DeserializeSimple<IEnumerable<ArticuloViewModel>>(jsonArticle);
			return result;
		}


	}
}
