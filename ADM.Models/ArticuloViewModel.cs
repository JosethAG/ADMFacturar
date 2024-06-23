namespace ADM.Models
{
    public class ArticuloViewModel
    {
        public string PK_Articulo { get; set; }
        public string Descripcion { get; set; }
        public string? Codigo_Barras { get; set; }
        public string? FK_Proveedor { get; set; }
        public int Cantidad { get; set; }
        public double Costo { get; set; }
        public double Precio { get; set; }
        public bool? Estado { get; set; }
    }
}
