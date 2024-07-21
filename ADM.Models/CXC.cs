namespace ADM.Models
{
    public class CXC
    {
        public string PK_Documento_CC { get; set; }
        public string FK_Cliente { get; set; }
        public string NombreCliente { get; set; }
        public DateTime Fecha_Documento { get; set; }
        public DateTime Fecha_Vencimiento { get; set; }
        public decimal Total_XC { get; set; }
        public decimal Saldo_Pendiente { get; set; }
        public string? Estado { get; set; }
        public string? FK_Usuario_Creacion { get; set; }
        public string? FK_Usuario_Modificacion { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public DateTime Fecha_Modificacion { get; set; }


    }
}
