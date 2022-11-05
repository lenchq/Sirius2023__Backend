using Sirius.LibraryGraphQL.Interfaces;
using Sirius.LibraryGraphQL.Model;

namespace Sirius.LibraryGraphQL.Mutation;

public partial class Mutation
{
    public async Task<Reader> AddReaderAsync(Reader input,
        [Service] IReaderRepository repo)
    {
        Reader r = new Reader
        {
            Id = Guid.NewGuid(),
            Email = input.Email,
            Name = input.Name,
            Fines = 0,
        };

        await repo.CreateReaderAsync(r);

        return r;
    }
    
    public async Task<Reader> DeleteReaderAsync(Guid input,
        [Service] IReaderRepository repo)
    {
        return await repo.DeleteReaderAsync(input);
    } 
    
    public async Task<Reader> UpdateReaderAsync(Reader input,
        [Service] IReaderRepository repo)
    {
        return await repo.UpdateReaderAsync(input);
    }
}