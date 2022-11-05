using Microsoft.EntityFrameworkCore;
using Sirius.LibraryGraphQL.Model;

namespace Sirius.LibraryGraphQL.Database;

public sealed class AppDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Rent> Rents{ get; set; }
    public DbSet<Reader> Readers{ get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasOne<Author>(_ => _.Author)
            .WithMany(_ => _.Books)
            .HasForeignKey(_ => _.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Author>()
            .HasMany<Book>(_ => _.Books)
            .WithOne(_ => _.Author)
            .HasForeignKey(_ => _.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Reader>()
            .HasMany<Rent>(_ => _.Rents)
            .WithOne(_ => _.Reader)
            .HasForeignKey(_ => _.ReaderId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}