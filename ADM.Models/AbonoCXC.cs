namespace ADM.Models
{
    public class AbonoCXC
    {
        public string Numero_Abono { get; set; }
        public string FK_Documento_CC { get; set; }
        public string FK_Cliente { get; set; }
        public DateTime Fecha_Documento { get; set; }
        public double Monto_Total { get; set; }
        public double Saldo_Pendiente { get; set; }
        public double Monto_Abonado { get; set; }
        public string Tipo_Pago { get; set; }
        public string Banco { get; set; }
        public DateTime Fecha_Abono { get; set; }  // Nueva propiedad Fecha_Abono
        public string? FK_Usuario_Creacion { get; set; }
        public string? FK_Usuario_Modificacion { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public DateTime Fecha_Modificacion { get; set; }


    }
}