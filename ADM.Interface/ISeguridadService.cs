using ADM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADM.Interface
{
    public interface ISeguridadService
    {
        IEnumerable<Seguridad> ListaSeguridades(string? Usuario);
        bool CrearUpdSeguridad(Seguridad Seguridad);
        bool EliminarSeguridad(string Id);
    }
}
