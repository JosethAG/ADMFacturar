using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADM.Models
{
    public class ProductoLinea
    {
        public int Cantidad { get; set; }
        public string Codigo { get; set; }
        public string Articulo { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public decimal Total { get; set; }
    }
}
