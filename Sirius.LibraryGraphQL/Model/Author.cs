using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sirius.LibraryGraphQL.Model;

public class Author
{
    [Key]
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? PhotoUrl { get; set; }
    public DateOnly? BirthDate { get; set; }
    public DateOnly? DeathDate { get; set; }
    
    public virtual ICollection<Book>? Books { get; set; }
    [NotMapped]
    public int AvailableBooksCount { get; set; }
}