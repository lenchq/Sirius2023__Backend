using Sirius.LibraryGraphQL.Model;

namespace Sirius.LibraryGraphQL.Types;

public class AddReaderInputType : InputObjectType<Reader>
{
    protected override void Configure(IInputObjectTypeDescriptor<Reader> descriptor)
    {
        descriptor.Name("AddReaderInput");

        descriptor.Ignore(static _ => _.Id);
        descriptor.Ignore(static _ => _.Fines);
        descriptor.Ignore(static _ => _.Rents);
        
        descriptor.Field(static _ => _.Name)
            .Type<NonNullType<StringType>>();
        descriptor.Field(static _ => _.Email)
            .Type<NonNullType<StringType>>();
    }
}