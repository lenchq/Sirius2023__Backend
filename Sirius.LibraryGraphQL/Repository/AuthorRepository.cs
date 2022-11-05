using Microsoft.EntityFrameworkCore;
using Sirius.LibraryGraphQL.Database;
using Sirius.LibraryGraphQL.Interfaces;
using Sirius.LibraryGraphQL.Model;

namespace Sirius.LibraryGraphQL.Repository;

public sealed class AuthorRepository : IAuthorRepository
{
    private readonly DbSet<Author> _authors;
    private readonly AppDbContext _ctx;

    public AuthorRepository(AppDbContext ctx)
    {
        _authors = ctx.Authors!;
        _ctx = ctx;
    }
    
    public async Task<Author> AddAuthorAsync(Author author)
    {
        var auth = await _authors.AddAsync(author);
        await _ctx.SaveChangesAsync();
        return auth.Entity;
    }

    public async Task<Author> RemoveAuthorAsync(Guid id)
    {
        var author = await _ctx.Authors.SingleAsync(x => x!.Id == id);
        _ctx.Authors.Remove(author);
        await _ctx.SaveChangesAsync();
        return author;
    }

    public async Task<Author> UpdateAuthorAsync(Author author)
    {
        var a = await _authors
            .Include(x => x.Books)
            .SingleAsync(x => x.Id == author.Id);
        a.Name = author.Name ?? a.Name;
        a.BirthDate = author.BirthDate ?? a.BirthDate;
        a.DeathDate = author.DeathDate ?? a.DeathDate;
        a.PhotoUrl = author.PhotoUrl ?? a.PhotoUrl;

        await _ctx.SaveChangesAsync();

        return a;
    }

    public async Task<Author?> GetAuthorByIdAsync(Guid id)
    {
        return await _authors
            .AsNoTracking()
            .Include(_ => _.Books)
            .FirstOrDefaultAsync(_ => _.Id == id);
    }

    public async Task<IEnumerable<Author>> GetAllAuthors()
    {
        return await _authors
            .AsNoTracking()
            .Include(_ => _.Books)
            .ToArrayAsync();
    }

    public async Task<int> GetBookCount(Guid id)
    {
        return await _ctx.Books.CountAsync(_ => _.AuthorId == id);
        // return (
        //     _authors
        //         .Include(static _ => _.Books)
        //         .First(_ => _.Id == id).Books 
        //     ?? throw new InvalidOperationException()
        //     ).Count;
    }
}