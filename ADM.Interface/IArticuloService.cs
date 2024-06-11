using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADM.Models;

namespace ADM.Interface
{
    public interface IArticuloService
    {
        IEnumerable<Articulo> ListaArticulos(string? Nombre);
        bool CrearUpdArticulo(Articulo articulo);
        bool EliminarArticulo(string Id);
    }
}
