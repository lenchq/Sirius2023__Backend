using Sirius.LibraryGraphQL.Model;

namespace Sirius.LibraryGraphQL.Types;

public class RentBookInputType : InputObjectType<Rent>
{
    protected override void Configure(IInputObjectTypeDescriptor<Rent> descriptor)
    {
        descriptor.Name("RentBookInputType");

        descriptor.Ignore(static _ => _.Id);
        descriptor.Ignore(static _ => _.Reader);
        descriptor.Ignore(static _ => _.Book);
        descriptor.Ignore(static _ => _.RentDate);
        descriptor.Ignore(static _ => _.RentDurationDays);
        descriptor.Ignore(static _ => _.DayFine);
        
        descriptor.Field(static _ => _.BookId)
            .Type<NonNullType<UuidType>>();
        descriptor.Field(static _ => _.ReaderId)
            .Type<NonNullType<UuidType>>();
    }
}