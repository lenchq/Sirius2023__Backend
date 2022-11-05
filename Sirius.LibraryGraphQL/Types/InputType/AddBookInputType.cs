using Sirius.LibraryGraphQL.Model;

namespace Sirius.LibraryGraphQL.Types;

public class AddBookInputType : InputObjectType<Book>
{
    protected override void Configure(IInputObjectTypeDescriptor<Book> descriptor)
    {
        descriptor.Name("AddBookInput");
        
        descriptor.Ignore(_ => _.Id);
        descriptor.Ignore(_ => _.Author);

        descriptor.Field(static _ => _.Name)
            .Type<NonNullType<StringType>>();
        descriptor.Field(static _ => _.AuthorId)
            .Type<NonNullType<UuidType>>();
        descriptor.Field(static _ => _.CoverUrl)
            .Type<UrlType>();
        descriptor.Field(static _ => _.MaxRentDurationDays)
            .Type<NonNullType<IntType>>();
    }
}