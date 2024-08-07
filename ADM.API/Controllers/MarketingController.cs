﻿using System.Data.SqlClient;
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


    }

}
