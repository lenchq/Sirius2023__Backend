using Sirius.LibraryGraphQL.Model;

namespace Sirius.LibraryGraphQL.Interfaces;

public interface IRentRepository
{
    public Task<Rent[]> GetRentsAsync(Guid renterId);
    public Task<Rent> GetRentAsync(Guid rentId);
    public Task<Rent?> GetRentAsync(Guid bookId, Guid readerId);
    public Task<Rent> CreateRentAsync(Guid bookId, Guid readerId);
    public Task DeleteRentAsync(Guid rentId);
    public Task DeleteRentAsync(Guid bookId, Guid readerId);
}