using Sirius.LibraryGraphQL.Interfaces;
using Sirius.LibraryGraphQL.Model;

namespace Sirius.LibraryGraphQL.Mutation;

public partial class Mutation
{
    public async Task<Rent> RentBookAsync(Rent input,
        [Service] IRentRepository rentRepo,
        [Service] IBookRepository bookRepo,
        [Service] IReaderRepository readerRepo)
    {
        if (!await bookRepo.ExistsAsync(input.BookId))
        {
            throw new GraphQLException(new Error("No such book exists", "400"));
        }

        if (!await readerRepo.ExistsAsync(input.ReaderId))
        {
            throw new GraphQLException(new Error("No such reader exists", "400"));
        }
        return await rentRepo.CreateRentAsync(input.BookId, input.ReaderId);
    } 
    public async Task<Rent> ReturnBookAsync(Rent input,
        [Service] IRentRepository rentRepo,
        [Service] IBookRepository bookRepo,
        [Service] IReaderRepository readerRepo)
    {
        var bookId = input.BookId;
        if (!await bookRepo.ExistsAsync(bookId))
        {
            throw new GraphQLException(new Error("No such book exists", "400"));
        }

        var readerId = input.ReaderId;
        if (!await readerRepo.ExistsAsync(readerId))
        {
            throw new GraphQLException(new Error("No such reader exists", "400"));
        }

        
        //TODO implement SRP?
        // e.g. fineValue = fineCalculator.Calculate(...)
        var rent = await rentRepo.GetRentAsync(bookId, readerId);
        if (rent is null)
        {
            throw new GraphQLException(new Error("No rent found", "400"));
        }
        var fine = 0;
        var rentOverdueDate = rent!.RentDate.ToDateTime(TimeOnly.MinValue);
        rentOverdueDate = rentOverdueDate.AddDays(rent.RentDurationDays);
        var today = DateTime.Today;
        if (rentOverdueDate < today)
        {
            var diff = (int)(rentOverdueDate - today).TotalDays;
            fine = Math.Abs(diff * rent.DayFine);
        }

        if (fine > 0)
        {
            await readerRepo.AddFineAsync(readerId, fine);
        }

        await rentRepo.DeleteRentAsync(rent.Id);

        return rent;
    } 

}