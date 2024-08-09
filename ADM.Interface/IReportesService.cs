using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADM.Models;

namespace ADM.Interface
{
	public interface IReportesService
	{
		IEnumerable<Reportes> ListaReporte(string? reporte);
		bool CrearUpdAbono(Reportes reporte);
		bool EliminarAbono(string Id);
	}
}
