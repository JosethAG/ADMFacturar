using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADM.Models.ApiMonster;

public class RestrictionResult
{
    public bool IsValid { get; set; }
    public string ErrorMessage { get; set; }
}
