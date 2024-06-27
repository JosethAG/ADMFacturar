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
using Microsoft.AspNetCore.Identity.Data;
using System;
using System.Text;
using System.Collections.Generic;


namespace ADM.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SeguridadController : Controller
    {
        private ISeguridadService? _SeguridadService;

        private readonly string connectionString = "Data Source=LOCALHOST\\SQLEXPRESS;Initial Catalog=ADM;Integrated Security=True;";

        [HttpGet]
        [Route("Listar")]
        public IEnumerable<Seguridad> ListaSeguridades(string? Usuario)
        {
            DataTable tSeguridad = null;

            if (Usuario == null)
            {
                tSeguridad = DBData.List("sp_ListarSeguridad");
            }
            else
            {
                List<DBParameter> parameters = new List<DBParameter>
                    {
                        new DBParameter("@Usuario", Usuario)
                    };
                tSeguridad = DBData.List("sp_ListarSeguridadxUsuario", parameters);
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
                DataTable tSeguridad = DBData.List("sp_ObtenerSeguridad", parameters);
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
                new DBParameter("@P_Usuario", Seguridad.Usuario),
                new DBParameter("@P_Correo", Seguridad.Correo),
                new DBParameter("@P_Contra", Seguridad.Contra),
                new DBParameter("@P_Rol", Seguridad.Rol.ToString()), //El rol en la bd es int
                new DBParameter("@P_Estado", Seguridad.Estado.ToString()),
                new DBParameter("@P_FK_Usuario_Creacion", Seguridad.FK_Usuario_Creacion),
                new DBParameter("@P_FK_Usuario_Modificacion", Seguridad.FK_Usuario_Modificacion),
                new DBParameter("@P_Fecha_Creacion", DateTime.Now.ToString("yyyy-MM-dd")),
                new DBParameter("@P_Fecha_Modificacion", DateTime.Now.ToString("yyyy-MM-dd"))
            };

            var result = DBData.Execute("sp_InsertarSeguridad", parameters);

            return result;
        }

        [HttpPost]
        [Route("Actualizar")]
        public bool ActualizarSeguridad(Seguridad Seguridad)
        {
            List<DBParameter> parameters = new List<DBParameter>
            {
                new DBParameter("@P_PK_IdUsuario", Seguridad.PK_IdUsuario.ToString()),
                new DBParameter("@P_Usuario", Seguridad.Usuario),
                new DBParameter("@P_Correo", Seguridad.Correo),
                new DBParameter("@P_Contra", Seguridad.Contra),
                new DBParameter("@P_Rol", Seguridad.Rol.ToString()), //El rol en la bd es int
                new DBParameter("@P_Estado", Seguridad.Estado.ToString()),
                new DBParameter("@P_FK_Usuario_Creacion", Seguridad.FK_Usuario_Creacion),
                new DBParameter("@P_FK_Usuario_Modificacion", Seguridad.FK_Usuario_Modificacion),
                new DBParameter("@P_Fecha_Creacion", DateTime.Now.ToString("yyyy-MM-dd")),
                new DBParameter("@P_Fecha_Modificacion", DateTime.Now.ToString("yyyy-MM-dd"))
            };

            var result = DBData.Execute("sp_ModificarSeguridad", parameters);

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

                var result = DBData.Execute("sp_EliminarSeguridad", parameters);

                return result;
            }
        }

        

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(ADM.Models.LoginRequest request)
        {
            try
            {
                bool isValid = DBData.ExecuteLoginValidation("sp_ValidarCredenciales", request.Usuario, request.Contra);

                if (isValid)
                {
                    return Ok(new { redirectTo = "/Home/Index" });
                }
                else
                {
                    return BadRequest("Usuario o contraseña incorrectos");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }



    }
}
 