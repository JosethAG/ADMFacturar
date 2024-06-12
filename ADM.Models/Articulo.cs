namespace ADM.Models
{
    public class Articulo
    {
        public string PK_Articulo { get; set; }
        public string Descripcion { get; set; }
        public string Codigo_Barras { get; set; }
        public string FK_Proveedor { get; set; }
        public int Cantidad { get; set; }
        public double Costo { get; set; }
        public double Precio { get; set; }
        public bool Estado { get; set; }
        public string FK_Usuario_Creacion { get; set; }
        public string? FK_Usuario_Modificacion { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }

    }
}