using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADM.Models
{
    public class Usuario
    {
        public int PK_IdUsuario {  get; set; }
        public string Nombre {  get; set; }
        public string Correo { get; set; }
        public string Contra { get; set; }
        public string Rol { get; set; }
        public bool? Estado { get; set; }
        public string? FK_Usuario_Creacion { get; set; }
        public string? FK_Usuario_Modificacion { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
