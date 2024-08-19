using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADM.Models
{
    public class EncabezadoFactura
    {
        public string Cliente { get; set; }
        public string Nombre { get; set; }
        public int Identificacion { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string CondicionPago { get; set; }
        public string Vendedor { get; set; }
        public string Transporte { get; set; }
        public DateTime Fecha { get; set; }
        public Decimal Subtotal { get; set; }
        public Decimal Descuento { get; set; }
        public Decimal Impuesto { get; set; }
        public Decimal Total { get; set; }
    }
}
