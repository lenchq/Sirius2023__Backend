using Sirius.LibraryGraphQL.Model;

namespace Sirius.LibraryGraphQL.Types;

public class UpdateReaderInputType : InputObjectType<Reader>
{
    protected override void Configure(IInputObjectTypeDescriptor<Reader> descriptor)
    {
        descriptor.Name("UpdateReaderInput");
        
        descriptor.Ignore(static _ => _.Rents);
        
        descriptor.Field(static _ => _.Id)
            .Type<NonNullType<UuidType>>();
        descriptor.Field(static _ => _.Email)
            .Type<StringType>();
        descriptor.Field(static _ => _.Name)
            .Type<StringType>();
        descriptor.Field(static _ => _.Fines)
            .Type<IntType>();
    }
}