namespace ADM.API.Models
{
    public class ClienteViewModel
    {
        public string PK_Cliente { get; set; }
        public string TipoIdentificacion { get; set; }
        public int Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public bool Estado { get; set; }
        public string CondicionPago { get; set; }
        public string Transporte { get; set; }
        public string Vendedor { get; set; }
        
    }
}
