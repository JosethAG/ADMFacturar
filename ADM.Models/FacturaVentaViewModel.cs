using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADM.Models
{
    public class FacturaVentaViewModel
    {
        public string FacturaID { get; set; }
        public DateTime FacturaFecha { get; set; }
        public string ClienteNombre { get; set; }
        public string VendedorNombre { get; set; }
        public decimal FacturaDescuento { get; set; }
        public decimal FacturaTotal { get; set; }
    }
}
