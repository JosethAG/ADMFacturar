using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADM.Models;

namespace ADM.Interface
{
    public interface IIngresoMercaderiaService
    {
        IEnumerable<IngresoMercaderia> ListaIngresoMercaderias(string? Nombre);
        bool CrearUpdIngresoMercaderia(IngresoMercaderia IngresoMercaderia);
        bool EliminarIngresoMercaderia(string Id);

    }
}
