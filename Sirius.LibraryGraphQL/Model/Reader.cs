using System.ComponentModel.DataAnnotations;

namespace Sirius.LibraryGraphQL.Model;

public class Reader
{
    [Key]
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public int? Fines { get; set; }

    public virtual ICollection<Rent> Rents { get; set; }
}