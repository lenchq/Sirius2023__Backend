using System.ComponentModel.DataAnnotations;

namespace Sirius.CaesarCipher.Model;

public class ShiftData
{
    [Key]
    public Guid Id { get; set; }
    public DateTimeOffset Date { get; set; }
    public int Shift { get; set; }
}