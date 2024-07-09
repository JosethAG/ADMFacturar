using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADM.Models;

namespace ADM.Interface
{
    public interface ICXPService
    {
        IEnumerable<CXP> ListaCXP(string? Nombre);
        bool CrearUpdCXP(CXP cxp);
        bool EliminarCXP(string Id);


    }
}
