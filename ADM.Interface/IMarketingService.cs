using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADM.Models;

namespace ADM.Interface
{
    public interface IMarketingService
    {
        IEnumerable<Correo> ListaCorreos(string? Nombre);
        bool CrearUpdCorreo(Correo Correo);
        bool EliminarCorreo(string Id);

    }
}
