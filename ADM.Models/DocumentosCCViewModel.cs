using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADM.Models
{
    public class DocumentosCCViewModel
    {
        public string PK_Documento_CC { get; set; }
        public string Nombre_Cliente { get; set; }
        public string Nombre_Vendedor { get; set; }
        public DateTime Fecha_Documento { get; set; }
        public DateTime Fecha_Vencimiento { get; set; }
        public decimal Total_XC { get; set; }
        public decimal Saldo_Pendiente { get; set; }
    }
}
