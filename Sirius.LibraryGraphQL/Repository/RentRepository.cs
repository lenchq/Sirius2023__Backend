using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Sirius.LibraryGraphQL.Database;
using Sirius.LibraryGraphQL.Interfaces;
using Sirius.LibraryGraphQL.Model;

namespace Sirius.LibraryGraphQL.Repository;

public class RentRepository : IRentRepository
{
    private readonly AppDbContext _ctx;

    public RentRepository(AppDbContext ctx)
    {
        _ctx = ctx;
    }
    
    public async Task<Rent[]> GetRentsAsync(Guid readerId)
    {
        return await _ctx.Rents
            .AsNoTracking()
            .Include(_ => _.Reader)
            .Include(_ => _.Book)
                .ThenInclude(_ => _.Author)
            .Where(_ => _.ReaderId == readerId)
            .ToArrayAsync();
    }
    public async Task<Rent?> GetRentAsync(Guid bookId, Guid readerId)
    {
        return await _ctx.Rents.AsNoTracking()
            .Include(x => x.Book)
            .ThenInclude(x => x!.Author)
            .Include(x => x.Reader)
            .FirstOrDefaultAsync(
                x => x.BookId == bookId 
                     && x.ReaderId == readerId);
    }

    public async Task<Rent> GetRentAsync(Guid rentId)
    {
        return await _ctx.Rents.SingleAsync(x => x.Id == rentId);
    }

    public async Task<Rent> CreateRentAsync(Guid bookId, Guid readerId)
    {
        var book = await _ctx.Books.SingleOrDefaultAsync(x => x.Id == bookId);

        var rent = new Rent
        {
            Id = Guid.NewGuid(),
            BookId = bookId,
            ReaderId = readerId,
            RentDate = DateOnly.FromDateTime(DateTime.UtcNow.Date),
            //SUGGESTION: calculate fineValue
            DayFine = (int)Constants.FineValue,
            RentDurationDays = book!.MaxRentDurationDays!.Value
        };

        var entry = await _ctx.Rents.AddAsync(rent);
        await _ctx.SaveChangesAsync();

        return await _ctx.Rents.AsNoTracking()
            .Include(x => x.Book)
                .ThenInclude(x => x.Author)
            .Include(x => x.Reader)
            .SingleAsync(x => x.Id == entry.Entity.Id);
    }

    public async Task DeleteRentAsync(Guid rentId)
    {
        _ctx.Remove(new Rent{ Id = rentId});
        await _ctx.SaveChangesAsync();
    }

    public async Task DeleteRentAsync(Guid bookId, Guid readerId)
    {
        var rent = await _ctx.Rents.SingleAsync(x => x.BookId == bookId && x.ReaderId == readerId);
        _ctx.Rents.Remove(rent);
    }
}