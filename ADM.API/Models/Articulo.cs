namespace ADM.API.Models
{
    public class Articulo
    {
        public string CodigoArticulo { get; set; }
        public string NombreArticulo { get; set; }
        public string DescripcionArticulo { get; set; }
        public int Cantidad { get; set; }
        public double Costo { get; set; }
    }
}