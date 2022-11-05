using Sirius.LibraryGraphQL.Interfaces;
using Sirius.LibraryGraphQL.Model;

namespace Sirius.LibraryGraphQL.Query;

public partial class Query
{
    public async Task<Author?> AuthorById(Guid id,
        [Service] IAuthorRepository repo)
    {
        return await repo.GetAuthorByIdAsync(id);
    }

    public async Task<IEnumerable<Author>> Authors([Service] IAuthorRepository repo)
    {
        return await repo.GetAllAuthors();
    }
}