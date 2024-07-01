namespace ADM.Models
{
    public class SalidaMercaderia
    {
        public string PK_FK_Documento { get; set; }
		public DateTime Fecha_Salida { get; set; }
        public string Tipo_Salida { get; set; }
        public string Observaciones { get; set; }
        public string? PK_FK_Articulo { get; set; }
		public int? Cantidad { get; set; }
		public string? Estado { get; set; }
		public string? FK_Usuario_Creacion { get; set; }
        public string? FK_Usuario_Modificacion { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
