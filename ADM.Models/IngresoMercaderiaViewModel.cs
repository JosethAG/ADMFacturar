namespace ADM.Models
{
    public class IngresoMercaderiaViewModel
    {
        public string PK_FK_Documento { get; set; }
		public string PK_FK_Proveedor { get; set; }
		public string FK_CondicionPago { get; set; }
		public string Fecha_Compra { get; set; }
		public double Total { get; set; }
		public string Estado { get; set; }

    }
}
