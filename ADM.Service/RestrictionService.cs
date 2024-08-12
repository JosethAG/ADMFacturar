using ADM.Models.ApiMonster;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ADM.Service;

public interface IRestrictionService
{
    public RestrictionResult Restriction(string prompt);
}


public class RestrictionService : IRestrictionService
{
    private const int MaxCharacterLimit = 100; 
    private const int MinCharacterLimit = 10;  
    private static readonly List<string> RestrictedWords = new List<string> { "casa", "perro", "gato" }; 
    private static readonly string AllowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 .,!?"; 

    public RestrictionResult Restriction(string prompt)
    {
        if (string.IsNullOrWhiteSpace(prompt))
        {
            return new RestrictionResult { IsValid = false, ErrorMessage = "El prompt no puede estar vacío." };
        }

        if (prompt.Length < MinCharacterLimit)
        {
            return new RestrictionResult { IsValid = false, ErrorMessage = $"El prompt debe tener al menos {MinCharacterLimit} caracteres." };
        }

        if (prompt.Length > MaxCharacterLimit)
        {
            return new RestrictionResult { IsValid = false, ErrorMessage = $"El prompt no puede tener más de {MaxCharacterLimit} caracteres." };
        }

        foreach (var word in RestrictedWords)
        {
            if (prompt.Contains(word, StringComparison.OrdinalIgnoreCase))
            {
                return new RestrictionResult { IsValid = false, ErrorMessage = $"El prompt contiene una palabra restringida: {word}" };
            }
        }

        foreach (var ch in prompt)
        {
            if (!AllowedCharacters.Contains(ch))
            {
                return new RestrictionResult { IsValid = false, ErrorMessage = "El prompt contiene caracteres no permitidos." };
            }
        }

        var words = prompt.Split(' ');
        var wordGroups = words.GroupBy(w => w).Where(g => g.Count() > 2);
        if (wordGroups.Any())
        {
            return new RestrictionResult { IsValid = false, ErrorMessage = "El prompt contiene palabras repetidas excesivamente." };
        }

        return new RestrictionResult { IsValid = true, ErrorMessage = string.Empty };
    }

}
