namespace ADM.Models
{
    public class IngresoMercaderia
    {
        public string PK_FK_Documento { get; set; }
		public int PK_FK_Proveedor { get; set; }
		public int FK_CondicionPago { get; set; }
		public DateTime Fecha_Compra { get; set; }
		public string PK_FK_Articulo { get; set; }
		public int Cantidad { get; set; }
		public double Costo { get; set; }
		public double? Subtotal { get; set; }
		public string? Estado { get; set; }
		public string? FK_Usuario_Creacion { get; set; }
        public string? FK_Usuario_Modificacion { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
