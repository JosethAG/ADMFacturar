using ADM.Architectur;
using ADM.Architecture;
using ADM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace ADM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransporteController : ControllerBase
    {
        [HttpGet]
        [Route("Listar")]
        public IEnumerable<Transporte> ListarTransportes()
        {
            DataTable tTransporte = DBData.List("sp_ListarTransportes");
            string jsonTransportes = JsonConvert.SerializeObject(tTransporte);
            var result = JsonProvider.DeserializeSimple<IEnumerable<Transporte>>(jsonTransportes);
            return result;
        }

        [HttpPost]
        [Route("Crear")]
        public bool CrearTransporte(Transporte transporte)
        {
            List<DBParameter> parameters = new List<DBParameter>
        {
            new DBParameter("@P_PK_Medio_Transporte", transporte.PK_Medio_Transporte.ToString()),
            new DBParameter("@P_Descripcion", transporte.Descripcion),
            new DBParameter("@P_Estado", transporte.Estado.ToString()),
            new DBParameter("@P_FK_Usuario_Creacion", transporte.FK_Usuario_Creacion),
            new DBParameter("@P_FK_Usuario_Modificacion", transporte.FK_Usuario_Modificacion),
            new DBParameter("@P_Fecha_Creacion", transporte.Fecha_Creacion.ToString()),
            new DBParameter("@P_Fecha_Modificacion", transporte.Fecha_Modificacion.ToString())
        };

            var result = DBData.Execute("sp_InsertarModificarTransportes", parameters);

            return result;
        }

        [HttpDelete]
        [Route("Eliminar")]
        public bool EliminarTransporte(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }

            List<DBParameter> parameters = new List<DBParameter>
        {
            new DBParameter("@P_PK_Medio_Transporte", id)
        };

            var result = DBData.Execute("sp_EliminarTransporte", parameters);

            return result;
        }
    }
}
