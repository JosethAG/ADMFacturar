namespace ADM.Models
{
    public class AbonoCXP
    {
        public string Numero_Recibo { get; set; }
        public string FK_Documento { get; set; }
        public string FK_Proveedor { get; set; }
        public DateTime Fecha_Documento { get; set; }
        public decimal Monto { get; set; }
        public decimal Saldo_Pendiente { get; set; }
        public decimal Monto_Abonado { get; set; }
        public string Tipo_Documento { get; set; }
        public string Banco { get; set; }
        public string? FK_Usuario_Creacion { get; set; }
        public string? FK_Usuario_Modificacion { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public DateTime Fecha_Modificacion { get; set; }


    }
}