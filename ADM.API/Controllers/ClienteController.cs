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
using ADM.API.Models;


namespace ADM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : Controller
    {
        private IClienteService? _ClienteService;

        [HttpGet]
        [Route("Listar")]
        public IEnumerable<Cliente> ListaClientes(string? Nombre)
        {
            DataTable tCliente = null;

            if (Nombre == null)
            {
                tCliente = DBData.List("sp_ListarClientes");
            }
            else
            {
                List<DBParameter> parameters = new List<DBParameter>
                    {
                        new DBParameter("@Nombre", Nombre)
                    };
                tCliente = DBData.List("sp_ListarClientesxNombre", parameters);
            }
            string jsonArticle = JsonConvert.SerializeObject(tCliente);
            var result = JsonProvider.DeserializeSimple<IEnumerable<Cliente>>(jsonArticle);
            return result;
        }

        [HttpGet]
        [Route("Listar2")]
        public IEnumerable<ClienteViewModel> ListaClientes2(string? Nombre)
        {
            DataTable tCliente = DBData.List("sp_ListarVM");
            string jsonArticle = JsonConvert.SerializeObject(tCliente);
            var result = JsonProvider.DeserializeSimple<IEnumerable<ClienteViewModel>>(jsonArticle);
            return result;
        }

        [HttpPost]
        [Route("Crear")]
        public bool CrearCliente(Cliente cliente)
        {
            List<DBParameter> parameters = new List<DBParameter>
            {
                new DBParameter("@P_PK_Cliente", cliente.PK_Cliente),
                new DBParameter("@P_TipoIdentificacion", cliente.TipoIdentificacion),
                new DBParameter("@P_Identificacion", cliente.Identificacion.ToString()),
                new DBParameter("@P_Nombre", cliente.Nombre),
                new DBParameter("@P_Telefono", cliente.Telefono),
                new DBParameter("@P_Correo", cliente.Correo),
                new DBParameter("@P_Provincia", cliente.Provincia),
                new DBParameter("@P_Canton", cliente.Canton),
                new DBParameter("@P_Distrito", cliente.Distrito),
                new DBParameter("@P_OtrasSenas", cliente.OtrasSenas),
                new DBParameter("@P_Estado", cliente.Estado.ToString()),
                new DBParameter("@P_FK_CondicionPago", cliente.FK_CondicionPago.ToString()),
                new DBParameter("@P_FK_Transporte", cliente.FK_Transporte.ToString()),
                new DBParameter("@P_FK_Vendedor", cliente.FK_Vendedor.ToString()),
                new DBParameter("@P_FK_Usuario_Creacion", cliente.FK_Usuario_Creacion.ToString()),
                new DBParameter("@P_FK_Usuario_Modificacion", cliente.FK_Usuario_Modificacion.ToString()),
                new DBParameter("@P_Fecha_Creacion", cliente.Fecha_Creacion.ToString()),
                new DBParameter("@P_Fecha_Modificacion", DateTime.Now.ToString())
            };

            var result = DBData.Execute("sp_InsertarClientes", parameters);

            return result;
        }

        [HttpPut]
        [Route("Actualizar")]
        public bool ActualizarCliente(Cliente cliente)
        {
            List<DBParameter> parameters = new List<DBParameter>
            {
                new DBParameter("@P_PK_Cliente", cliente.PK_Cliente),
                new DBParameter("@P_TipoIdentificacion", cliente.TipoIdentificacion),
                new DBParameter("@P_Identificacion", cliente.Identificacion.ToString()),
                new DBParameter("@P_Nombre", cliente.Nombre),
                new DBParameter("@P_Telefono", cliente.Telefono),
                new DBParameter("@P_Correo", cliente.Correo),
                new DBParameter("@P_Provincia", cliente.Provincia),
                new DBParameter("@P_Canton", cliente.Canton),
                new DBParameter("@P_Distrito", cliente.Distrito),
                new DBParameter("@P_OtrasSenas", cliente.OtrasSenas),
                new DBParameter("@P_Estado", cliente.Estado.ToString()),
                new DBParameter("@P_FK_CondicionPago", cliente.FK_CondicionPago.ToString()),
                new DBParameter("@P_FK_Transporte", cliente.FK_Transporte.ToString()),
                new DBParameter("@P_FK_Vendedor", cliente.FK_Vendedor.ToString()),
                new DBParameter("@P_FK_Usuario_Modificacion", cliente.FK_Usuario_Modificacion.ToString()),
                new DBParameter("@P_Fecha_Modificacion", DateTime.Now.ToString())
            };

            var result = DBData.Execute("sp_ModificarClientes", parameters);

            return result;
        }

        [HttpDelete]
        [Route("Eliminar")]
        public bool EliminarCliente(string Id)
        {
            if (Id == null || Id.Length == 0)
            {
                return false;
            }
            else
            {
                List<DBParameter> parameters = new List<DBParameter>
                {
                    new DBParameter("@P_PK_Cliente", Id)
                };
                
                var result = DBData.Execute("sp_EliminarClientes", parameters);

                return !result;
            }
        }
    }
}
