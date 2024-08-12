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
using ADM.Models.ApiMonster;

namespace ADM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketingController : Controller
    {
        private IMarketingService? _MarketingService;

        [HttpGet]
        [Route("Listar")]
        public IEnumerable<Correo> ListaCorreos()
        {
            DataTable tCorreo = null;
            tCorreo = DBData.List("sp_ListarCorreos");
            string jsonArticle = JsonConvert.SerializeObject(tCorreo);
            var result = JsonProvider.DeserializeSimple<IEnumerable<Correo>>(jsonArticle);
            return result;
        }

        [HttpGet]
        [Route("ListarGC")]
        public IEnumerable<GrupoCorreo> ListaGrupoCorreos()
        {
            DataTable tCorreo = null;
            tCorreo = DBData.List("sp_ListarGC");
            string jsonArticle = JsonConvert.SerializeObject(tCorreo);
            var result = JsonProvider.DeserializeSimple<IEnumerable<GrupoCorreo>>(jsonArticle);
            return result;
        }

        [HttpGet]
        [Route("ObtenerGC/{Id}")]
        public GrupoCorreo ListaGrupoCorreos(string? Id)
        {
            List<DBParameter> parameters = new List<DBParameter>
                    {
                        new DBParameter("@Id", Id)
                    };
            DataTable tGC = DBData.List("sp_ObtenerGrupoCorreo", parameters);

            string jsonGC = JsonConvert.SerializeObject(tGC);
            var result = JsonProvider.DeserializeSimple<IEnumerable<GrupoCorreo>>(jsonGC);
            GrupoCorreo rest = result.FirstOrDefault();
            return rest;
        }



        [HttpPost]
        [Route("Crear")]
        public bool CrearCorreo(Correo Correo)
        {
            // Log received data for debugging
            Console.WriteLine("Received Correo: " + JsonConvert.SerializeObject(Correo));

            List<DBParameter> parameters = new List<DBParameter>
            {
                new DBParameter("@P_Destinatario", Correo.Destinatario),
                new DBParameter("@P_Asunto", Correo.Asunto),
                new DBParameter("@P_Contenido", Correo.Contenido)

            };

            var result = DBData.Execute("sp_InsertarEnvCorreo", parameters);

            // Log the result of the database operation
            Console.WriteLine("Database operation result: " + result);

            return result;
        }

        [HttpPost]
        [Route("CrearGC")]
        public bool CrearGC(GrupoCorreo GC)
        {
            // Log received data for debugging
            Console.WriteLine("Grupo recibido: " + JsonConvert.SerializeObject(GC));

            List<DBParameter> parameters = new List<DBParameter>
            {
                new DBParameter("@Name", GC.Name),
                new DBParameter("@Correos", GC.Correos)

            };

            var result = DBData.Execute("sp_CrearGrupoCorreo", parameters);

            // Log the result of the database operation
            Console.WriteLine("Database operation result: " + result);

            return result;
        }


        [HttpPost]
        [Route("ActualizarGC")]
        public bool ActualizarGC(GrupoCorreo GC)
        {
            // Log received data for debugging
            Console.WriteLine("Grupo recibido: " + JsonConvert.SerializeObject(GC));

            List<DBParameter> parameters = new List<DBParameter>
            {
                new DBParameter("@Id", GC.Id.ToString()),
                new DBParameter("@Name", GC.Name),
                new DBParameter("@Correos", GC.Correos)

            };

            var result = DBData.Execute("sp_ActualizarGrupoCorreo", parameters);

            // Log the result of the database operation
            Console.WriteLine("Database operation result: " + result);

            return result;
        }

        [HttpPost]
        [Route("EliminarGC/{ID}")]
        public bool EliminarGC(string ID)
        {
            if (ID == null || ID.Length == 0)
            {
                return false;
            }
            else
            {
                List<DBParameter> parameters = new List<DBParameter>
                {
                    new DBParameter("@Id", ID)
                };

                var result = DBData.Execute("sp_EliminarGrupoCorreos", parameters);

                return result;
            }
        }

        /*Sección de Generado de imágenes*/

        [HttpGet]
        [Route("ListarIMG")]
        public IEnumerable<ImagePrompt> ListarIMG()
        {
            DataTable tImg = null;
            tImg = DBData.List("sp_ListarIMG");
            string jsonImg = JsonConvert.SerializeObject(tImg);
            var result = JsonProvider.DeserializeSimple<IEnumerable<ImagePrompt>>(jsonImg);
            return result;
        }


        [HttpPost]
        [Route("CrearIMG")]
        public bool CrearIMG(ImagePrompt ImgPro)
        {
            // Log received data for debugging
            Console.WriteLine("Grupo recibido: " + JsonConvert.SerializeObject(ImgPro));

            List<DBParameter> parameters = new List<DBParameter>
            {
                new DBParameter("@Prompt", ImgPro.Prompt),
                new DBParameter("@Img", ImgPro.Img)

            };

            var result = DBData.Execute("sp_CrearIMG", parameters);

            // Log the result of the database operation
            Console.WriteLine("Database operation result: " + result);

            return result;
        }

    }

}
