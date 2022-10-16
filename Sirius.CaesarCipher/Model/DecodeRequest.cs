using System.ComponentModel.DataAnnotations;

namespace Sirius.CaesarCipher.Model;

public class DecodeRequest
{
    [Required]
    [RegularExpression(@"[a-zA-Z0-9].+")]
    public string? Message { get; set; }
    [Range(-100,26)]
    public int Rot { get; set; }
}