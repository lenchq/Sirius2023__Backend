using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using Sirius.LibraryGraphQL.Database;
using Sirius.LibraryGraphQL.Interfaces;
using Sirius.LibraryGraphQL.Model;
using Sirius.LibraryGraphQL.Types;

namespace Sirius.LibraryGraphQL.Repository;

public sealed class BookRepository : IBookRepository
{
    private readonly AppDbContext _ctx;
    
    public BookRepository(AppDbContext ctx)
    {
        _ctx = ctx;
    }
    
    
    public async Task<Book?> GetBookByIdAsync(Guid id)
    {
        return await _ctx.Books.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _ctx.Books.Include(_ => _.Author).ToArrayAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _ctx.Books
            .AsNoTracking().
            AnyAsync(x => x.Id == id);
    }

    public async Task<Book> AddBookAsync(Book book)
    {
        var addedBookId = (await _ctx.Books
            .AddAsync(book)
            ).Entity.Id;
        await _ctx.SaveChangesAsync();
        var addedBook = await _ctx.Books
            .Include(x => x.Author)
            .SingleAsync(x => x.Id == addedBookId);
        return addedBook;
    }

    public async Task<Book> UpdateBookAsync(Book book)
    {
        var bk = await _ctx.Books
            .Include(x => x.Author)
            .SingleAsync(x => x.Id == book.Id);
        bk.AuthorId = book.AuthorId ?? bk.AuthorId;
        bk.Name = book.Name ?? bk.Name;
        bk.CoverUrl = book.CoverUrl ?? bk.CoverUrl;
        bk.MaxRentDurationDays = book.MaxRentDurationDays ?? bk.MaxRentDurationDays;
        await _ctx.SaveChangesAsync();
        return bk;
    }

    public async Task<Book> RemoveBookAsync(Guid id)
    {
        var book = await _ctx.Books
            .Include(_ => _.Author)
            //.ThenInclude(_ => _.AvailableBooksCount)
            .SingleAsync(x => x.Id == id);
        _ctx.Books.Remove(book);
        await _ctx.SaveChangesAsync();
        return book;
    }
}