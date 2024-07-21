namespace ADM.Models
{
    public class FacturaVM
    {
        public string Documento { get; set; }
        public string Fac_Referencia { get; set; }
        public string Fecha { get; set; }       
        public string Cliente { get; set; }
        public string TipoDoc { get; set; }
        public string? Vendedor { get; set; }
        public string? CondicionPago { get; set; }
        public string? Transporte { get; set; }
        public double Total { get; set; }
		public string? Estado { get; set; }

    }
}
