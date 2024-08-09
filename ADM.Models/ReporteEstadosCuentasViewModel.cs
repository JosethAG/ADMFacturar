using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADM.Models
{
    public class ReporteEstadosCuentasViewModel
    {
        public IEnumerable<FacturaModel> Facturas { get; set; }
        public decimal TotalPendiente { get; set; }
    }
}