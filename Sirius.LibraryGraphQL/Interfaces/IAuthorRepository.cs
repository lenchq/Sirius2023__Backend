using Sirius.LibraryGraphQL.Model;

namespace Sirius.LibraryGraphQL.Interfaces;

public interface IAuthorRepository
{
    public Task<Author> AddAuthorAsync(Author author);
    public Task<Author> RemoveAuthorAsync(Guid id);
    public Task<Author> UpdateAuthorAsync(Author author);
    public Task<Author?> GetAuthorByIdAsync(Guid id);
    public Task<IEnumerable<Author>> GetAllAuthors();

    public Task<int> GetBookCount(Guid id);
    
}