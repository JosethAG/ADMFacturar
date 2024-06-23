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
                tVendedor = DBData.List("sp_ListarVendedoresxNombre", parameters);
            }
            string jsonArticle = JsonConvert.SerializeObject(tVendedor);
            var result = JsonProvider.DeserializeSimple<IEnumerable<Vendedor>>(jsonArticle);
            return result;
        }

		[HttpGet]
		[Route("Obtener/{PK}")]
		public Vendedor ObtenerVendedor(string? PK)
		{
			if (PK != null)
			{
				List<DBParameter> parameters = new List<DBParameter>
					{
						new DBParameter("@PK_Vendedor", PK)
					};
				DataTable tVendedor = DBData.List("sp_ObtenerVendedor", parameters);
				string jsonArticle = JsonConvert.SerializeObject(tVendedor);
				var result = JsonProvider.DeserializeSimple<IEnumerable<Vendedor>>(jsonArticle);


				return result.FirstOrDefault();
			}
			else
			{
				return new Vendedor();
			}
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
				new DBParameter("@P_Fecha_Creacion", DateTime.Now.ToString("yyyy-MM-dd")),
				new DBParameter("@P_Fecha_Modificacion", DateTime.Now.ToString("yyyy-MM-dd"))
			};

            var result = DBData.Execute("sp_InsertarVendedores", parameters);

            return result;
        }

        [HttpPost]
        [Route("Actualizar")]
        public bool ActualizarVendedor(Vendedor Vendedor)
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
				new DBParameter("@P_Fecha_Creacion", DateTime.Now.ToString("yyyy-MM-dd")),
				new DBParameter("@P_Fecha_Modificacion", DateTime.Now.ToString("yyyy-MM-dd"))
			};

            var result = DBData.Execute("sp_ModificarVendedores", parameters);

            return result;
        }

        [HttpPost]
		[Route("Desactivar/{PK}")]
		public bool DesactivarVendedor(string PK)
        {
            if (PK == null || PK.Length == 0)
            {
                return false;
            }
            else
            {
                List<DBParameter> parameters = new List<DBParameter>
                {
                    new DBParameter("@P_PK_Vendedor", PK)
                };

                var result = DBData.Execute("sp_EliminarVendedores", parameters);

                return result;
            }
        }
    }
}