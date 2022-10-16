using Microsoft.EntityFrameworkCore;
using Sirius.CaesarCipher.Model;

namespace Sirius.CaesarCipher.Database;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<ShiftData> Shifts { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShiftData>()
            .Property(x => x.Date)
            .HasConversion(
                x => x.ToUnixTimeSeconds(),
                x => DateTimeOffset.FromUnixTimeSeconds(x)
            );
        base.OnModelCreating(modelBuilder);
    }
}