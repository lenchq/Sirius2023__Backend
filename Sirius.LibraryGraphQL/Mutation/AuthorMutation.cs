using Sirius.LibraryGraphQL.Interfaces;
using Sirius.LibraryGraphQL.Model;

namespace Sirius.LibraryGraphQL.Mutation;

public partial class Mutation
{
    public async Task<Author> AddAuthorAsync(
        Author input,
        [Service] IAuthorRepository authorRepo)
    {
        var author = new Author
        {
            Id = Guid.NewGuid(),
            AvailableBooksCount = 0,
            Name = input.Name,
            BirthDate = input.BirthDate,
            DeathDate = input.DeathDate,
            PhotoUrl = input.PhotoUrl,
        };

        await authorRepo.AddAuthorAsync(author);

        return author;
    }
    public async Task<Author> DeleteAuthorAsync(
        Guid input,
        [Service] IAuthorRepository authorRepo)
    {
        if (await authorRepo.GetAuthorByIdAsync(input) is null)
        {
            throw new GraphQLException(
                new Error("No author with this id", "404"));
        }
        return await authorRepo.RemoveAuthorAsync(input);
    }
    
    public async Task<Author> UpdateAuthorAsync(
        Author input,
        [Service] IAuthorRepository authorRepo)
    {
        if (await authorRepo.GetAuthorByIdAsync(input.Id) is null)
        {
            throw new GraphQLException(new Error("No author with this id", "404"));
        }
        
        var auth = await authorRepo.UpdateAuthorAsync(input);
        return auth;
    }
}