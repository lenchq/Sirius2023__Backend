using Sirius.CaesarCipher.Model;

namespace Sirius.CaesarCipher.Interfaces;

public interface IShiftRepository
{
    public Task AddShiftAsync(int value);
    public Task<ShiftData[]> GetShiftDataAsync();
}