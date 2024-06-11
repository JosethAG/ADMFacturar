using System.Data.SqlClient;
using System.Data;
using ADM.API;
using ADM.Models;
using ADM.Service;
using Microsoft.AspNetCore.Mvc;
using ADM.Interface;
using ADM.Architectur;
using ADM.Architecture;
using Newtonsoft.Json;


namespace ADM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : Controller
    {
        private IProveedorService? _ProveedorService;

        [HttpGet]
        [Route("Listar")]
        public IEnumerable<Proveedor> ListaProveedores(string? Nombre)
        {
            DataTable tProveedor = null;

            if (Nombre == null)
            {
                tProveedor = DBData.List("sp_ListarProveedores");
            }
            else
            {
                List<DBParameter> parameters = new List<DBParameter>
                    {
                        new DBParameter("@Nombre", Nombre)
                    };
                tProveedor = DBData.List("sp_ListarProveedoresxNombre", parameters);
            }
            string jsonArticle = JsonConvert.SerializeObject(tProveedor);
            var result = JsonProvider.DeserializeSimple<IEnumerable<Proveedor>>(jsonArticle);
            return result;
        }

        [HttpPost]
        [Route("Crear")]
        public bool CrearProveedor(Proveedor Proveedor)
        {
            List<DBParameter> parameters = new List<DBParameter>
            {
                new DBParameter("@P_PK_Proveedor", Proveedor.PK_Proveedor.ToString()),
                new DBParameter("@P_Nombre", Proveedor.Nombre),
                new DBParameter("@P_Telefono", Proveedor.Telefono),
                new DBParameter("@P_Correo", Proveedor.Correo),
                new DBParameter("@P_Direccion", Proveedor.Direccion),
                new DBParameter("@P_Estado", Proveedor.Estado.ToString()),
                new DBParameter("@P_FK_Usuario_Creacion", Proveedor.FK_Usuario_Creacion),
                new DBParameter("@P_FK_Usuario_Modificacion", Proveedor.FK_Usuario_Modificacion),
                new DBParameter("@P_Fecha_Creacion", Proveedor.FechaCreacion.ToString("yyyy-MM-dd HH:mm:ss")),
                new DBParameter("@P_Fecha_Modificacion", Proveedor.FechaModificacion.ToString("yyyy-MM-dd HH:mm:ss"))
            };

            var result = DBData.Execute("sp_InsertarModificarProveedores", parameters);

            return result;
        }

        [HttpDelete]
        [Route("Eliminar")]
        public bool EliminarProveedor(string Id)
        {
            if (Id == null || Id.Length == 0)
            {
                return false;
            }
            else
            {
                List<DBParameter> parameters = new List<DBParameter>
                {
                    new DBParameter("@P_PK_Proveedor", Id)
                };
                
                var result = DBData.Execute("sp_EliminarProveedores", parameters);

                return !result;
            }
        }
    }
}
