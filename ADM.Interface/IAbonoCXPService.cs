using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADM.Models;

namespace ADM.Interface
{
    public interface IAbonoCXPService
    {
        IEnumerable<AbonoCXP> ListaAbono(string? Nombre);
        bool CrearUpdAbono(AbonoCXP abonocxp);
        bool EliminarAbono(string Id);

    }
}
