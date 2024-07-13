using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADM.Models;

namespace ADM.Interface
{
    public interface ICXCService
    {
        IEnumerable<CXC> ListaCXC(string? Nombre);
        bool CrearUpdCXC(CXC cxc);
        bool EliminarCXC(string Id);


    }
}
