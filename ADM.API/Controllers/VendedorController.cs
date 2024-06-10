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
    public class VendedorController : Controller
    {
        private IVendedorService? _VendedorService;

        [HttpGet]
        [Route("Listar")]
        public IEnumerable<Vendedor> ListaVendedores(string? Nombre)
        {
            DataTable tVendedor = null;

            if (Nombre == null)
            {
                tVendedor = DBData.List("sp_ListarVendedores");
            }
            else
            {
                List<DBParameter> parameters = new List<DBParameter>
                    {
                        new DBParameter("@Nombre", Nombre)
                    };
                tVendedor = DBData.List("sp_ListarVendedoressxNombre", parameters);
            }
            string jsonArticle = JsonConvert.SerializeObject(tVendedor);
            var result = JsonProvider.DeserializeSimple<IEnumerable<Vendedor>>(jsonArticle);
            return result;
        }

        [HttpPost]
        [Route("Crear")]
        public bool CrearVendedor(Vendedor Vendedor)
        {
            List<DBParameter> parameters = new List<DBParameter>
            {
                new DBParameter("@P_PK_Vendedor", Vendedor.PK_Vendedor.ToString()),
                new DBParameter("@P_Nombre", Vendedor.Nombre),
                new DBParameter("@P_Telefono", Vendedor.Telefono),
                new DBParameter("@P_Correo", Vendedor.Correo),
                new DBParameter("@P_Estado", Vendedor.Estado.ToString()),
                new DBParameter("@P_FK_Usuario_Creacion", Vendedor.FK_Usuario_Creacion),
                new DBParameter("@P_FK_Usuario_Modificacion", Vendedor.FK_Usuario_Modificacion),
                new DBParameter("@P_Fecha_Creacion", Vendedor.FechaCreacion.ToString()),
                new DBParameter("@P_Fecha_Modificacion", Vendedor.FechaModificacion.ToString())
            };

            var result = DBData.Execute("sp_InsertarModificarVendedores", parameters);

            return result;
        }

        [HttpDelete]
        [Route("Eliminar")]
        public bool EliminarVendedor(string Id)
        {
            if (Id == null || Id.Length == 0)
            {
                return false;
            }
            else
            {
                List<DBParameter> parameters = new List<DBParameter>
                {
                    new DBParameter("@P_PK_Vendedor", Id)
                };

                var result = DBData.Execute("sp_EliminarVendedores", parameters);

                return !result;
            }
        }
    }
}