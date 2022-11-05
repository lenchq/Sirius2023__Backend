using Sirius.LibraryGraphQL.Interfaces;
using Sirius.LibraryGraphQL.Model;

namespace Sirius.LibraryGraphQL.Query;

public partial class Query
{
    public async Task<Rent[]> Rents(Guid readerId,
        [Service] IRentRepository rentRepository,
        [Service] IReaderRepository readerRepository)
    {
        if (!await readerRepository.ExistsAsync(readerId))
        {
            throw new GraphQLException(new Error("No reader with this id", "400"));
        }
        
        return await rentRepository.GetRentsAsync(readerId);
    }
}