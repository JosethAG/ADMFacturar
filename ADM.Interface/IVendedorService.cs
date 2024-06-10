using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADM.Models;

namespace ADM.Interface
{
    public interface IVendedorService
    {
        
        IEnumerable<Vendedor> ListaVendedores(string? Nombre);
        bool CrearUpdVendedor(Vendedor Vendedor);
        bool EliminarVendedor(string Id);

    }
}
