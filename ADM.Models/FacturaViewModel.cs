namespace ADM.Models
{
    public class FacturaViewModel
    {
        public Encabezado Encabezado { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
        public List<ProductoViewModel> Productos { get; set; }
    }
}
