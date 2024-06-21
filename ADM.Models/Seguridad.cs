namespace ADM.Models
{
    public class Seguridad
    {
        public int PK_IdUsuario { get; set; } //Podria ser long en lugar de int
        public string Usuario { get; set; }
        public string Correo { get; set; }
        public string Contra { get; set; }
        public int? Rol { get; set; }
        public bool Estado { get; set; }
        public string FK_Usuario_Creacion { get; set; }
        public string? FK_Usuario_Modificacion { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
