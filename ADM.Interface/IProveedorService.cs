using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADM.Models;

namespace ADM.Interface
{
    public interface IProveedorService
    {
        IEnumerable<Proveedor> ListaProveedors(string? Nombre);
        bool CrearUpdProveedor(Proveedor Proveedor);
        bool EliminarProveedor(string Id);

    }
}
