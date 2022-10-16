using Sirius.CaesarCipher.Interfaces;
using Sirius.CaesarCipher.Model;

namespace Sirius.CaesarCipher.Providers;

public sealed class RotStatisticsProvider : IRotStatisticsProvider
{
    private readonly IShiftRepository _repo;

    public RotStatisticsProvider(IShiftRepository repo)
    {
        _repo = repo;
    }
    
    public async Task<ShiftStatistics[]> GetStatisticsAsync()
    {
        var stats = await _repo.GetShiftDataAsync();
        var todayStats = stats.Where(static x => x.Date.Date == DateTime.Today.Date);

        var usageStats = todayStats
            .DistinctBy(x => x.Shift)
            .Select(x =>
            {
                var value = x.Shift;
                var count = stats.Count(y => y.Shift == value);

                return new ShiftStatistics
                {
                    Rot = value,
                    Usages = count,
                };
            });
        return usageStats.ToArray();
    }
}