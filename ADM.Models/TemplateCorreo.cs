using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADM.Models;

public class TemplateCorreo
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Asunto { get; set; }
    public string Contenido { get; set; }
}
