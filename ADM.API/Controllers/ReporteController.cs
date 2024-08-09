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
        private ICXPService? _CXPService;
		private IReportesService? _ReportesService;
		//REPORTES DESDE DASHBOARDS / VISTA INDEX
		[HttpGet]
		[Route("ListarDashboards")]
		public IEnumerable<Reportes> ListaReporte(string? reporte)
		{
			DataTable tReporte = null;

			if (reporte == null)
			{
				tReporte = DBData.List("sp_ListarReportes");
			}
			else
			{
				tReporte = DBData.List("sp_ListarReportes");
			}
			string jsonArticle = JsonConvert.SerializeObject(tReporte);
			var result = JsonProvider.DeserializeSimple<IEnumerable<Reportes>>(jsonArticle);
			return result;
		}


		//REPORTE ARTICULOS / INVENTARIO

		[HttpGet]
		[Route("ReporteArticulos")]
		public IEnumerable<ArticuloViewModel> ListarArticulosVM()
		{
			DataTable tArticulo = DBData.List("sp_ListarArticulosVM");
			string jsonArticle = JsonConvert.SerializeObject(tArticulo);
			var result = JsonProvider.DeserializeSimple<IEnumerable<ArticuloViewModel>>(jsonArticle);
			return result;
		}

        //REPORTE CXP

        [HttpGet]
        [Route("ReporteCXP")]
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
		[Route("ReporteEstadosCuentas")]
		public IActionResult ObtenerEstadosCuentas()
		{
			// Ejecutar el procedimiento almacenado para obtener los datos de facturas
			DataTable dtFacturas = DBData.List("sp_ObtenerDatosFacturas");

			// Convertir el DataTable a JSON
			string jsonFacturas = JsonConvert.SerializeObject(dtFacturas);

			// Deserializar el JSON a una lista de FacturaModel
			var facturas = JsonConvert.DeserializeObject<IEnumerable<FacturaModel>>(jsonFacturas);

			// Obtener el total pendiente
			decimal totalPendiente = 0;
			if (dtFacturas.Rows.Count > 0)
			{
				var totalPendienteRow = dtFacturas.AsEnumerable()
					.Where(row => row.Field<string>("Nombre_Cliente") == "Total")
					.FirstOrDefault();
				if (totalPendienteRow != null)
				{
					totalPendiente = totalPendienteRow.Field<decimal>("Total_Pendiente");
				}
			}

			// Crear el objeto para el resultado final
			var result = new ReporteEstadosCuentasViewModel
			{
				Facturas = facturas,
				TotalPendiente = totalPendiente
			};

			return Ok(result);
		}

		[HttpGet]
		[Route("ReporteVentas")]
		public IActionResult ObtenerVentas()
		{
			DataTable dtVentas = DBData.List("sp_reporteventas");
			string jsonVentas = JsonConvert.SerializeObject(dtVentas);
			var ventas = JsonConvert.DeserializeObject<IEnumerable<FacturaVentaViewModel>>(jsonVentas);

			return Ok(ventas);
		}

		//Reporte VENDEDORES
		[HttpGet]
		[Route("ReporteVendedores")]
		public IActionResult ObtenerVendedores()
		{
			DataTable dtVendedores = DBData.List("sp_ReporteVendedores");
			string jsonVendedores = JsonConvert.SerializeObject(dtVendedores);
			var vendedores = JsonConvert.DeserializeObject<IEnumerable<ReporteVendedoresViewModel>>(jsonVendedores);

			return Ok(vendedores);
		}

		[HttpGet]
		[Route("ReporteDocumentosCC")]
		public IActionResult ObtenerDocumentosCC()
		{
			DataTable dtDocumentosCC = DBData.List("sp_ListarDocumentosCCReporte");
			string jsonDocumentosCC = JsonConvert.SerializeObject(dtDocumentosCC);
			var documentosCC = JsonConvert.DeserializeObject<IEnumerable<DocumentosCCViewModel>>(jsonDocumentosCC);

			return Ok(documentosCC);
		}
	}
}
