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
    public class ArticuloController : Controller
    {
        private IArticuloService? _ArticuloService;

        [HttpGet]
        [Route("Listar")]
        public IEnumerable<Articulo> ListaArticulos(string? Nombre)
        {
            DataTable tArticulo = null;

            if (Nombre == null)
            {
                tArticulo = DBData.List("sp_ListarArticulos");
            }
            else
            {
                List<DBParameter> parameters = new List<DBParameter>
                    {
                        new DBParameter("@Nombre", Nombre)
                    };
                tArticulo = DBData.List("sp_ListarArticulosxNombre", parameters);
            }
            string jsonArticle = JsonConvert.SerializeObject(tArticulo);
            var result = JsonProvider.DeserializeSimple<IEnumerable<Articulo>>(jsonArticle);
            return result;
        }

        [HttpPost]
        [Route("Crear")]
        public bool CrearArticulo(Articulo articulo)
        {
            List<DBParameter> parameters = new List<DBParameter>
            {
                new DBParameter("@P_PK_Articulo", articulo.PK_Articulo),
                new DBParameter("@P_Nombre", articulo.Nombre),
                new DBParameter("@P_Descripcion", articulo.Descripcion),
                new DBParameter("@P_Codigo_Barras", articulo.Codigo_Barras),
                new DBParameter("@P_FK_Proveedor", articulo.FK_Proveedor),
                new DBParameter("@P_Cantidad", articulo.Cantidad.ToString()),
                new DBParameter("@P_Costo", articulo.Costo.ToString()),
                new DBParameter("@P_Precio_Unitario", articulo.Precio_Unitario.ToString())
            };

            var result = DBData.Execute("sp_InsertarModificarArticulos", parameters);

            return result;
        }
        

        [HttpPut]
        [Route("Actualizar")]
        public bool ActualizarArticulo(Articulo articulo)
        {
            List<DBParameter> parameters = new List<DBParameter>
            {
                new DBParameter("@P_PK_Articulo", articulo.PK_Articulo),
                new DBParameter("@P_Nombre", articulo.Nombre),
                new DBParameter("@P_Descripcion", articulo.Descripcion),
                new DBParameter("@P_Codigo_Barras", articulo.Codigo_Barras),
                new DBParameter("@P_FK_Proveedor", articulo.FK_Proveedor),
                new DBParameter("@P_Cantidad", articulo.Cantidad.ToString()),
                new DBParameter("@P_Costo", articulo.Costo.ToString()),
                new DBParameter("@P_Precio_Unitario", articulo.Precio_Unitario.ToString())
            };

            var result = DBData.Execute("sp_InsertarModificarArticulos", parameters);

            return result;
        }

        [HttpDelete]
        [Route("Eliminar")]
        public bool EliminarArticulo(string Id)
        {
            if (Id == null || Id.Length == 0)
            {
                return false;
            }
            else
            {
                List<DBParameter> parameters = new List<DBParameter>
                {
                    new DBParameter("@P_PK_Articulo", Id)
                };

                var result = DBData.Execute("sp_EliminarArticulos", parameters);

                return !result;
            }
        }
    }
}
