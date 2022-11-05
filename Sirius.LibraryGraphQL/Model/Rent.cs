using System.ComponentModel.DataAnnotations.Schema;

namespace Sirius.LibraryGraphQL.Model;

public class Rent
{
    public Guid Id { get; set; }
    
    public Guid BookId { get; set; }
    [ForeignKey("BookId")]
    public virtual Book? Book { get; set; }
    
    public Guid ReaderId { get; set; }
    [ForeignKey("ReaderId")]
    public virtual Reader? Reader { get; set; }
    
    public DateOnly RentDate { get; set; }
    public int RentDurationDays { get; set; }
    public int DayFine { get; set; }
}