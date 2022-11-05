using Sirius.LibraryGraphQL.Model;

namespace Sirius.LibraryGraphQL.Types;

public class BookType : ObjectType<Book>
{
    protected override void Configure(IObjectTypeDescriptor<Book> descriptor)
    {
        descriptor.Name("Book");

        descriptor.Field(static x => x.Id)
            .Type<NonNullType<UuidType>>();

        descriptor.Field(static x => x.CoverUrl)
            .Type<NonNullType<UrlType>>()
            .Name("cover");

        descriptor.Field(static x => x.MaxRentDurationDays)
            .Type<NonNullType<IntType>>()
            .Name("maxRentDuration");

        descriptor.Field(static _ => _.AuthorId)
            .Ignore();

        descriptor.Field(static x => x.Author)
            .Type<AuthorType>()
            .Name("author");
    }
}