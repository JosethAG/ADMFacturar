using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADM.Models
{
    public class FacturaModel
    {
        public int PK_Factura { get; set; }
        public string Nombre_Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Fecha_Vencimiento { get; set; }
        public string Estado { get; set; }
        public decimal Total { get; set; }
        public decimal Saldo_Pendiente { get; set; }
    }
}
