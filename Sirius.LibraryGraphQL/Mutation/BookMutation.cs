using Sirius.LibraryGraphQL.Interfaces;
using Sirius.LibraryGraphQL.Model;

namespace Sirius.LibraryGraphQL.Mutation;

public partial class Mutation
{
    public async Task<Book> AddBookAsync(
        Book input,
        [Service] IBookRepository bookRepo,
        [Service] IAuthorRepository authorRepo)
    {
        var id = input.AuthorId!.Value;
        if (await authorRepo.GetAuthorByIdAsync(id) is null)
        {
            throw new GraphQLException(new Error("No author with this id", "404"));
        }
        var book = new Book
        {
            Id = Guid.NewGuid(),
            AuthorId = input.AuthorId,
                
            Name = input.Name,
            CoverUrl = input.CoverUrl,
            MaxRentDurationDays = input.MaxRentDurationDays,
        };

        await bookRepo.AddBookAsync(book);

        return book;
    }
    
    public async Task<Book> DeleteBookAsync(
        Guid input,
        [Service] IBookRepository bookRepo)
    {
        if (await bookRepo.ExistsAsync(input))
            return await bookRepo.RemoveBookAsync(input);
        throw new GraphQLException(
            new Error("No book with this id", "400"));
    }
    
    public async Task<Book> UpdateBookAsync(
        Book input,
        [Service] IBookRepository bookRepo)
    {
        if (await bookRepo.ExistsAsync(input.Id))
            return await bookRepo.UpdateBookAsync(input);
        throw new GraphQLException(new Error("No book with this id", "400"));
    }
}