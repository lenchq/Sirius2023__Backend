using Sirius.LibraryGraphQL.Model;

namespace Sirius.LibraryGraphQL.Types;

public class UpdateAuthorInputType : InputObjectType<Author>
{
    protected override void Configure(IInputObjectTypeDescriptor<Author> descriptor)
    {
        descriptor.Name("UpdateAuthorInput");
        
        descriptor.Field(static _ => _.Id)
            .Type<NonNullType<UuidType>>();
        descriptor.Field(static _ => _.Name)
            .Type<StringType>();
        descriptor.Field(static _ => _.BirthDate)
            .Type<DateType>();
        descriptor.Field(static _ => _.DeathDate)
            .Type<DateType>();
        descriptor.Field(static _ => _.PhotoUrl)
            .Type<UrlType>();

        descriptor.Ignore(static _ => _.Books);
        descriptor.Ignore(static _ => _.AvailableBooksCount);
    }
}