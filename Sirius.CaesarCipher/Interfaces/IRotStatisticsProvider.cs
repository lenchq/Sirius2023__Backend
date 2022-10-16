using Sirius.CaesarCipher.Model;

namespace Sirius.CaesarCipher.Interfaces;

public interface IRotStatisticsProvider
{
    public Task<ShiftStatistics[]> GetStatisticsAsync();
}