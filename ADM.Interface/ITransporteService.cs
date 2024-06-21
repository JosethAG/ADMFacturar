using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADM.Models;

namespace ADM.Interface
{
    public interface ITransporteService
    {
        IEnumerable<Proveedor> ListarTransportes(string? Descripcion);
        bool CrearUpdTransporte(Transporte Transporte);
        bool EliminarTransporte(string Id);
    }
}
