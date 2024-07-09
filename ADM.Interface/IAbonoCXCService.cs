using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADM.Models;

namespace ADM.Interface
{
    public interface IAbonoCXCService
    {
        IEnumerable<AbonoCXC> ListaAbono(string? Nombre);
        bool CrearUpdAbono(AbonoCXC abonocxp);
        bool EliminarAbono(string Id);

    }
}
