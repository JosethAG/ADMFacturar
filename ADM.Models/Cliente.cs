namespace ADM.Models
{
    public class Cliente
    {
        public string PK_Cliente { get; set; }
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
        public int FK_CondicionPago { get; set; }
        public int FK_Transporte { get; set; }
        public int FK_Vendedor { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
