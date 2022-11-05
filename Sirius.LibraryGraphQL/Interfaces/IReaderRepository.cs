using Sirius.LibraryGraphQL.Model;

namespace Sirius.LibraryGraphQL.Interfaces;

public interface IReaderRepository
{
    public Task<Reader> CreateReaderAsync(Reader reader);
    public Task<Reader> UpdateReaderAsync(Reader input);
    public Task<Reader> DeleteReaderAsync(Guid readerId);
    public Task<Reader?> GetReaderById(Guid readerId);
    public Task<bool> ExistsAsync(Guid readerId);
    
    /// <summary>
    /// Set readers fine to fineValue value
    /// </summary>
    /// <param name="readerId">reader id</param>
    /// <param name="fineValue">value to set</param>
    /// <returns></returns>
    public Task AddFineAsync(Guid readerId, int fineValue);
    public Task SubtractFineAsync(Guid readerId, int fineValue);
    public Task<Reader[]?> Readers();

}