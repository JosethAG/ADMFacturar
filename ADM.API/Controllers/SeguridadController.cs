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
        public IEnumerable<Seguridad> ListaSeguridades(string? Usuario)
        {
            DataTable tSeguridad = null;

            if (Usuario == null)
            {
                tSeguridad = DBData.List("sp_ListarUsuario");
            }
            else
            {
                List<DBParameter> parameters = new List<DBParameter>
                    {
                        new DBParameter("@Nombre", Usuario)
                    };
                tSeguridad = DBData.List("sp_ListarUsuarioxUsuario", parameters);
            }
            string jsonArticle = JsonConvert.SerializeObject(tSeguridad);
            var result = JsonProvider.DeserializeSimple<IEnumerable<Seguridad>>(jsonArticle);
            return result;
        }

        [HttpGet]
        [Route("Obtener/{PK}")]
        public Seguridad ObtenerSeguridad(string? PK)
        {
            if (PK != null)
            {
                List<DBParameter> parameters = new List<DBParameter>
                    {
                        new DBParameter("@PK_IdUsuario", PK)
                    };
                DataTable tSeguridad = DBData.List("sp_ObtenerUsuario", parameters);
                string jsonArticle = JsonConvert.SerializeObject(tSeguridad);
                var result = JsonProvider.DeserializeSimple<IEnumerable<Seguridad>>(jsonArticle);


                return result.FirstOrDefault();
            }
            else
            {
                return new Seguridad();
            }
        }

        [HttpPost]
        [Route("Crear")]
        public bool CrearSeguridad(Seguridad Seguridad)
        {
            List<DBParameter> parameters = new List<DBParameter>
            {
                new DBParameter("@P_PK_IdUsuario", Seguridad.PK_IdUsuario.ToString()),
                new DBParameter("@P_Nombre", Seguridad.Usuario),
                new DBParameter("@P_Correo", Seguridad.Correo),
                new DBParameter("@P_Contra", Seguridad.Contra),
                new DBParameter("@P_Rol", Seguridad.Rol.ToString()), //El rol en la bd es int
                new DBParameter("@P_Estado", Seguridad.Estado.ToString()),
                new DBParameter("@P_FK_Usuario_Creacion", Seguridad.FK_Usuario_Creacion),
                new DBParameter("@P_FK_Usuario_Modificacion", Seguridad.FK_Usuario_Modificacion),
                new DBParameter("@P_Fecha_Creacion", DateTime.Now.ToString("yyyy-MM-dd")),
                new DBParameter("@P_Fecha_Modificacion", DateTime.Now.ToString("yyyy-MM-dd"))
            };

            var result = DBData.Execute("sp_InsertarUsuario", parameters);

            return result;
        }

        [HttpPost]
        [Route("Actualizar")]
        public bool ActualizarSeguridad(Seguridad Seguridad)
        {
            List<DBParameter> parameters = new List<DBParameter>
            {
                new DBParameter("@P_PK_IdUsuario", Seguridad.PK_IdUsuario.ToString()),
                new DBParameter("@P_Nombre", Seguridad.Usuario),
                new DBParameter("@P_Correo", Seguridad.Correo),
                new DBParameter("@P_Contra", Seguridad.Contra),
                new DBParameter("@P_Rol", Seguridad.Rol.ToString()), //El rol en la bd es int
                new DBParameter("@P_Estado", Seguridad.Estado.ToString()),
                new DBParameter("@P_FK_Usuario_Creacion", Seguridad.FK_Usuario_Creacion),
                new DBParameter("@P_FK_Usuario_Modificacion", Seguridad.FK_Usuario_Modificacion),
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
 