namespace ADM.Models
{
    public class Vendedor
    {
        public int PK_Vendedor { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public bool Estado { get; set; }
        public string FK_Usuario_Creacion { get; set; }
        public string FK_Usuario_Modificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}