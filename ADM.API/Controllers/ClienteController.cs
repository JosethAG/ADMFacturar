using ADM.API.Models;
using System.Data.SqlClient;
using System.Data;
using ADM.API.Conn;

namespace ADM.API.Controllers
{
    public class ClienteController
    {
        public List<Cliente> ClienteList()
        {
            var CltList = new List<Cliente>();

            var cnn = new Connection();

            using (var conn = new SqlConnection(cnn.GetConSQL()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_ListaCliente", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CltList.Add(new Cliente()
                        {
                            IdCliente = dr["PK_Cliente"].ToString(),
                            TipoIdentificacion = dr["TipoIdentificacion"].ToString(),
                            NumeroIdentificacion = Convert.ToInt32(dr["Identificacion"]),
                            Nombre = dr["Nombre"].ToString(),
                            Telefono = dr["Telefono"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            Provincia = dr["Provincia"].ToString(),
                            Canton = dr["Canton"].ToString(),
                            Distrito = dr["Distrito"].ToString(),
                            OtrasSenas = dr["OtrasSenas"].ToString(),
                            Estado = Convert.ToBoolean(dr["Estado"])
                        });
                    }
                }
            }
            return CltList;
        }
    }
}
