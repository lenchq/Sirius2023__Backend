using System.ComponentModel.DataAnnotations;

namespace Sirius.CaesarCipher.Model;

public class EncodeRequest
{
    [Required]
    [RegularExpression(@"[a-zA-Z0-9].+")]
    public string Message { get; set; }
    [Range(0,3000)]
    public int Rot { get; set; }
}