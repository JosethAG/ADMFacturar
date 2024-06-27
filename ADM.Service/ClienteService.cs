using ADM.Architectur;
using ADM.Architecture;
using ADM.Interface;
using ADM.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data;

namespace ADM.Service
{
    public class ClienteService
    {
        private IConfiguration _config;

        public ClienteService(IConfiguration config) 
        {
            _config = config;
        }

        //public async Task<IEnumerable<Cliente>> ListaClientes(string? Nombre)
        //{
            
        //}


        bool CrearUpdCliente(Cliente cliente)
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
                new DBParameter("@P_Fecha_Modificacion", cliente.Fecha_Modificacion.ToString())
            };

            var result = DBData.Execute("sp_InsertarModificarClientes", parameters);

            return result;
        }

        bool EliminarCliente(string Id)
        {
            if (Id == null || Id.Length == 0)
            {
                return false;
            }
            else
            {
                List<DBParameter> parameters = new List<DBParameter>
                {
                    new DBParameter("@", Id)
                };

                return DBData.Execute("", parameters);
            }
        }



    }
}
