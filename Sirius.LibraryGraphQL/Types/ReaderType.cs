using Sirius.LibraryGraphQL.Model;

namespace Sirius.LibraryGraphQL.Types;

public class ReaderType : ObjectType<Reader>
{
    protected override void Configure(IObjectTypeDescriptor<Reader> descriptor)
    {
        descriptor.Field(static _ => _.Id)
            .Type<NonNullType<UuidType>>();
        descriptor.Field(static _ => _.Name)
            .Type<NonNullType<StringType>>();
        descriptor.Field(static _ => _.Rents)
            .Type<ListType<RentType>>();
        descriptor.Field(static _ => _.Fines)
            .Type<NonNullType<IntType>>();
    }
}