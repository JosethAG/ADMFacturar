using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADM.Models;

namespace ADM.Interface
{
    public interface IClienteService
    {
        IEnumerable<Cliente> ListaClientes(string? Nombre);
        bool CrearUpdCliente(Cliente cliente);
        bool EliminarCliente(string Id);

    }
}
