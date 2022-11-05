using Microsoft.EntityFrameworkCore;
using Sirius.LibraryGraphQL.Database;
using Sirius.LibraryGraphQL.Interfaces;
using Sirius.LibraryGraphQL.Model;

namespace Sirius.LibraryGraphQL.Repository;

public class ReaderRepository : IReaderRepository
{
    private AppDbContext _ctx;
    
    public ReaderRepository(AppDbContext ctx)
    {
        _ctx = ctx;
    }
    
    public async Task<Reader> CreateReaderAsync(Reader reader)
    {
        var entry = await _ctx.Readers.AddAsync(reader);
        await _ctx.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<Reader> UpdateReaderAsync(Reader input)
    {
        var reader = await _ctx.Readers
            .Include(x => x.Rents)
            .SingleAsync(x => x.Id == input.Id);

        reader.Email = input.Email ?? reader.Email;
        reader.Name = input.Name ?? reader.Name;
        reader.Fines = input.Fines ?? reader.Fines;

        await _ctx.SaveChangesAsync();
        return reader;
    }

    public async Task<Reader> DeleteReaderAsync(Guid readerId)
    {
        var reader = await _ctx.Readers.SingleAsync(x => x.Id == readerId);
        _ctx.Readers.Remove(reader);
        await _ctx.SaveChangesAsync();
        return reader;
    }

    public async Task<Reader?> GetReaderById(Guid readerId)
    {
        var reader = await _ctx.Readers.SingleOrDefaultAsync(x => x.Id == readerId);
        return reader;
    }

    public async Task<bool> ExistsAsync(Guid readerId)
    {
        return await _ctx.Readers.AnyAsync(x => x.Id == readerId);
    }

    public async Task AddFineAsync(Guid readerId, int fineValue)
    {
        var reader = await _ctx.Readers.SingleAsync(x => x.Id == readerId);
        reader.Fines += fineValue;
        await _ctx.SaveChangesAsync();
    }

    public async Task SubtractFineAsync(Guid readerId, int fineValue)
    {
        var reader = await _ctx.Readers.SingleAsync(x => x.Id == readerId);
        reader.Fines -= fineValue;
        await _ctx.SaveChangesAsync();
    }

    public async Task<Reader[]?> Readers()
    {
        return await _ctx.Readers
            .Include(_ => _.Rents)
                .ThenInclude(_ => _.Book)
                    .ThenInclude(_ => _.Author)
            .ToArrayAsync();
    }
}