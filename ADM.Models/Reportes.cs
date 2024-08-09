using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADM.Models
{
	public class Reportes
	{
		public int idReporte { get; set; }
		public decimal TotalVenta { get; set; }
		public decimal TotalCobro { get; set; }
		public decimal TotalGanancia { get; set; }
		public decimal TotalCosto { get; set; }
		public decimal TotalDescuentos { get; set; }
		public decimal TotalNotaCredito { get; set; }
		public decimal TotalCompras { get; set; }
	}
}
