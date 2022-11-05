using Sirius.LibraryGraphQL.Model;

namespace Sirius.LibraryGraphQL.Types;

public class UpdateBookInputType : InputObjectType<Book>
{
    protected override void Configure(IInputObjectTypeDescriptor<Book> descriptor)
    {
        descriptor.Name("UpdateBookInput");
        
        descriptor.Ignore(static _ => _.Author);
        
        descriptor.Field(static _ => _.Id) 
            .Type<NonNullType<UuidType>>();
        descriptor.Field(_ => _.AuthorId)
            .Type<UuidType>();
        descriptor.Field(static _ => _.MaxRentDurationDays)
            .Type<IntType>();
        descriptor.Field(static _ => _.CoverUrl)
            .Type<UrlType>();
    }
}