using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADM.Models
{
    public class ReporteVendedoresViewModel
    {
        public string PK_Factura { get; set; }
        public DateTime Fecha { get; set; }
        public string Nombre_Cliente { get; set; }
        public string Nombre_Vendedor { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
        public decimal Comision { get; set; }
    }
}
