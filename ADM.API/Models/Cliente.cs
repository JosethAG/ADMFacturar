namespace ADM.API.Models
{
    public class Cliente
    {
        public string IdCliente { get; set; }
        public string TipoIdentificacion { get; set; }
        public int NumeroIdentificacion { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }
        public string OtrasSenas { get; set; }
        public bool Estado { get; set; }
    }
}
