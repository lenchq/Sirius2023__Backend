using Sirius.LibraryGraphQL.Model;

namespace Sirius.LibraryGraphQL.Interfaces;

public interface IBookRepository
{
    public Task<Book?> GetBookByIdAsync(Guid id);
    public Task<Book> AddBookAsync(Book book);
    public Task<Book> UpdateBookAsync(Book book);
    public Task<Book> RemoveBookAsync(Guid id);
    public Task<IEnumerable<Book>> GetAllBooksAsync();

    public Task<bool> ExistsAsync(Guid id);
}