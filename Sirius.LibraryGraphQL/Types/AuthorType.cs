using Sirius.LibraryGraphQL.Interfaces;
using Sirius.LibraryGraphQL.Model;

namespace Sirius.LibraryGraphQL.Types;

public class AuthorType : ObjectType<Author>
{
    protected override void Configure(IObjectTypeDescriptor<Author> descriptor)
    {
        descriptor.Name("Author");

        descriptor.Field(x => x.Books)
            .Type<ListType<BookType>>()
            .Name("books");

        descriptor.Field(x => x.BirthDate)
            .Type<NonNullType<DateType>>()
            .Name("birthDate");
        descriptor.Field(x => x.DeathDate)
            .Type<DateType>()
            .Name("deathDate");
        descriptor.Field(x => x.PhotoUrl)
            .Type<NonNullType<UrlType>>()
            .Name("photoUrl");
        
        descriptor.Field(x => x.AvailableBooksCount)
            .Type<NonNullType<IntType>>()
            .Name("availableBookCount")
            .ResolveWith<Resolvers>(_ => _.GetBookCount(default!,default!));
    }

    private class Resolvers
    {
        public async Task<int> GetBookCount([Parent] Author author, [Service] IAuthorRepository repo)
        {
            return await repo.GetBookCount(author.Id);
        }
    }
}