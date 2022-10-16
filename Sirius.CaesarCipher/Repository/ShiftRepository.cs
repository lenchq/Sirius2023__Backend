using Microsoft.EntityFrameworkCore;
using Sirius.CaesarCipher.Database;
using Sirius.CaesarCipher.Interfaces;
using Sirius.CaesarCipher.Model;

namespace Sirius.CaesarCipher.Repository;

public sealed class ShiftRepository : IShiftRepository
{
    private AppDbContext _context;
    private IDateTimeProvider _timeProvider;
    public ShiftRepository(AppDbContext ctx, IDateTimeProvider dateTimeProvider)
    {
        _context = ctx;
        _timeProvider = dateTimeProvider;
    }

    public async Task AddShiftAsync(int value)
    {
        var date = _timeProvider.UtcNow;
        await _context.Database.EnsureCreatedAsync();
        await _context.SaveChangesAsync();
        _context.Shifts.Add(new ShiftData
        {
            Date = date,
            Shift = value
        });
        await _context.SaveChangesAsync();
    }

    public async Task<ShiftData[]> GetShiftDataAsync()
    {
        var data = _context.Shifts;
        return await data.ToArrayAsync();
    }
}