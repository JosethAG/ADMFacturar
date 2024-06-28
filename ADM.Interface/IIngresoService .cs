using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADM.Models;

namespace ADM.Interface
{
    public interface ISalidaMercaderiaService
    {
        IEnumerable<SalidaMercaderia> ListaSalidaMercaderias(string? Nombre);
        bool CrearUpdSalidaMercaderia(SalidaMercaderia SalidaMercaderia);
        bool EliminarSalidaMercaderia(string Id);

    }
}
