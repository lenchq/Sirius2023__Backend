using Sirius.LibraryGraphQL.Interfaces;
using Sirius.LibraryGraphQL.Model;

namespace Sirius.LibraryGraphQL.Query;

public partial class Query
{
    public async Task<Book?> BookById([Argument("id")] Guid id, 
        [Service] IBookRepository repo)
    {
        return await repo.GetBookByIdAsync(id);
    }

    public async Task<IEnumerable<Book>> Books([Service] IBookRepository repo)
    {
        return await repo.GetAllBooksAsync();
    }
}