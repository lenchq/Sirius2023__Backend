using Sirius.LibraryGraphQL.Model;

namespace Sirius.LibraryGraphQL.Types;

public class RentType : ObjectType<Rent>
{
    protected override void Configure(IObjectTypeDescriptor<Rent> descriptor)
    {
        
        descriptor.Field(static _ => _.Id)
            .Type<NonNullType<UuidType>>();
        descriptor.Field(static _ => _.DayFine)
            .Type<NonNullType<IntType>>();
        descriptor.Field(static _ => _.RentDurationDays)
            .Type<NonNullType<IntType>>();
        descriptor.Field(static _ => _.RentDate)
            .Type<NonNullType<DateType>>();
        descriptor.Field(static _ => _.Reader)
            .Type<NonNullType<ReaderType>>();
        descriptor.Field(static _ => _.Book)
            .Type<NonNullType<BookType>>();

        descriptor.Ignore(static _ => _.ReaderId);
        descriptor.Ignore(static _ => _.BookId);
    }
}