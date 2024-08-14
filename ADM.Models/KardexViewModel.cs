using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADM.Models
{
    public class KardexViewModel
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Articulo { get; set; }
        public string? Descripcion { get; set; }
        public string Movimiento { get; set; }
        public string TipoMovimiento { get; set; }
        public string? Documento { get; set; }
        public decimal CantidadMovimiento { get; set; }
        public decimal Existencia { get; set; }
    }
}
