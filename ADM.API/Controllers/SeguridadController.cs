using ADM.API;
using ADM.Models;
using Microsoft.AspNetCore.Mvc;
using ADM.Architectur;
using ADM.Architecture;
using Newtonsoft.Json;
using System;
using System.Data;


namespace ADM.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SeguridadController : Controller
    {

        [HttpGet]
        [Route("Listar")]
        public IEnumerable<Usuario> ListaUsuarios(string? Usuario)
        {
            DataTable tUsuario = null;

            if (Usuario == null)
            {
                tUsuario = DBData.List("sp_ListarUsuario");
            }
            else
            {
                List<DBParameter> parameters = new List<DBParameter>
                    {
                        new DBParameter("@Nombre", Usuario)
                    };
                tUsuario = DBData.List("sp_ListarUsuarioxUsuario", parameters);
            }
            string jsonArticle = JsonConvert.SerializeObject(tUsuario);
            var result = JsonProvider.DeserializeSimple<IEnumerable<Usuario>>(jsonArticle);
            return result;
        }

        [HttpGet]
        [Route("Obtener/{PK}")]
        public Usuario ObtenerSeguridad(string? PK)
        {
            if (PK != null)
            {
                List<DBParameter> parameters = new List<DBParameter>
                    {
                        new DBParameter("@PK_IdUsuario", PK)
                    };
                DataTable tUsuario = DBData.List("sp_ObtenerUsuario", parameters);
                string jsonArticle = JsonConvert.SerializeObject(tUsuario);
                var result = JsonProvider.DeserializeSimple<IEnumerable<Usuario>>(jsonArticle);


                return result.FirstOrDefault();
            }
            else
            {
                return new Usuario();
            }
        }

        [HttpPost]
        [Route("Crear")]
        public bool CrearSeguridad(Usuario usuario)
        {
            List<DBParameter> parameters = new List<DBParameter>
            {
                new DBParameter("@P_PK_IdUsuario", usuario.PK_IdUsuario.ToString()),
                new DBParameter("@P_Nombre", usuario.Nombre),
                new DBParameter("@P_Correo", usuario.Correo),
                new DBParameter("@P_Contra", usuario.Contra),
                new DBParameter("@P_Rol", usuario.Rol), 
                new DBParameter("@P_Estado", usuario.Estado.ToString()),
                new DBParameter("@P_FK_Usuario_Creacion", usuario.FK_Usuario_Creacion),
                new DBParameter("@P_FK_Usuario_Modificacion", usuario.FK_Usuario_Modificacion),
                new DBParameter("@P_Fecha_Creacion", DateTime.Now.ToString("yyyy-MM-dd")),
                new DBParameter("@P_Fecha_Modificacion", DateTime.Now.ToString("yyyy-MM-dd"))
            };

            var result = DBData.Execute("sp_InsertarUsuario", parameters);

            return result;
        }

        [HttpPost]
        [Route("Actualizar")]
        public bool ActualizarSeguridad(Usuario usuario)
        {
            List<DBParameter> parameters = new List<DBParameter>
            {
                new DBParameter("@P_PK_IdUsuario", usuario.PK_IdUsuario.ToString()),
                new DBParameter("@P_Nombre", usuario.Nombre),
                new DBParameter("@P_Correo", usuario.Correo),
                new DBParameter("@P_Contra", usuario.Contra),
                new DBParameter("@P_Rol", usuario.Rol), //El rol en la bd es int
                new DBParameter("@P_Estado", usuario.Estado.ToString()),
                new DBParameter("@P_FK_Usuario_Creacion", usuario.FK_Usuario_Creacion),
                new DBParameter("@P_FK_Usuario_Modificacion", usuario.FK_Usuario_Modificacion),
                new DBParameter("@P_Fecha_Creacion", DateTime.Now.ToString("yyyy-MM-dd")),
                new DBParameter("@P_Fecha_Modificacion", DateTime.Now.ToString("yyyy-MM-dd"))
            };

            var result = DBData.Execute("sp_ModificarUsuario", parameters);

            return result;
        }

        [HttpPost]
        [Route("Desactivar/{PK}")]
        public bool DesactivarSeguridad(string PK)
        {
            if (PK == null || PK.Length == 0)
            {
                return false;
            }
            else
            {
                List<DBParameter> parameters = new List<DBParameter>
                {
                    new DBParameter("@P_PK_IdUsuario", PK)
                };

                var result = DBData.Execute("sp_EliminarUsuario", parameters);

                return result;
            }
        }

        

        [HttpPost]
        [Route("Login")]
        public IEnumerable<Usuario> Login([FromBody] LoginRequestDto request)
        {

            if (request.Correo != null && request.Contra != null)
            {
                List<DBParameter> parameters = new List<DBParameter>
                    {
                        new DBParameter("@Correo", request.Correo),
                        new DBParameter("@Contra", request.Contra)
                    };
                DataTable tUsuario = DBData.List("sp_ValidarCredenciales", parameters);
                string jsonUsuario = JsonConvert.SerializeObject(tUsuario);
                var result = JsonProvider.DeserializeSimple<IEnumerable<Usuario>>(jsonUsuario);

                return result;
            }
            else
            {
                return null;
            }
            
        }

    }
}
 