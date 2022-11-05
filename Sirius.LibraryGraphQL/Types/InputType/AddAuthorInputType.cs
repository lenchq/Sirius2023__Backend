using Sirius.LibraryGraphQL.Model;

namespace Sirius.LibraryGraphQL.Types;

public class AddAuthorInputType : InputObjectType<Author>
{
    protected override void Configure(IInputObjectTypeDescriptor<Author> descriptor)
    {
        descriptor.Name("AddAuthorInput");
        
        descriptor.Ignore(_ => _.Id);
        descriptor.Ignore(_ => _.Books);
        descriptor.Ignore(_ => _.AvailableBooksCount);

        descriptor.Field(static _ => _.Name)
            .Type<NonNullType<StringType>>();
        descriptor.Field(static _ => _.BirthDate)
            .Type<NonNullType<DateType>>();
        descriptor.Field(static _ => _.DeathDate)
            .Type<DateType>();
        descriptor.Field(static _ => _.PhotoUrl)
            .Type<UrlType>();
    }
}