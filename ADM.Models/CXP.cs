namespace ADM.Models
{
    public class CXP
    {
        public string PK_Documento { get; set; }
        public string FK_Proveedor { get; set; }
        public DateTime Fecha_Documento { get; set; }
        public DateTime Fecha_Vence { get; set; }
        public string? Observaciones { get; set; }
        public decimal Monto { get; set; }
        public decimal Saldo_Pendiente { get; set; }
        public string? Anulado { get; set; }
        public string? FK_Usuario_Creacion { get; set; }
        public string? FK_Usuario_Modificacion { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public DateTime Fecha_Modificacion { get; set; }


    }
}
