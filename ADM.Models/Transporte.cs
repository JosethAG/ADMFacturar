namespace ADM.Models
{
    public class Transporte
    {
        public int PK_Medio_Transporte { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public string FK_Usuario_Creacion { get; set; }
        public string? FK_Usuario_Modificacion { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}