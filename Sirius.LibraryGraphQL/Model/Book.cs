using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sirius.LibraryGraphQL.Model;

public class Book
{
    [Key]
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int? MaxRentDurationDays { get; set; }
    public string? CoverUrl { get; set; }
    
    [Required]
    public Guid? AuthorId { get; set; }
    [ForeignKey("AuthorId")]
    public Author? Author { get; set; }
}