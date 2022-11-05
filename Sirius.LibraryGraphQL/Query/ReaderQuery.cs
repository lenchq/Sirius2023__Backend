using Sirius.LibraryGraphQL.Interfaces;
using Sirius.LibraryGraphQL.Model;

namespace Sirius.LibraryGraphQL.Query;

public partial class Query
{
    public async Task<Reader?> ReaderById(Guid id,
        [Service] IReaderRepository repo)
    {
        return await repo.GetReaderById(id);
    }

    public async Task<Reader?[]> Readers([Service] IReaderRepository repo)
    {
        return await repo.Readers();
    }
}