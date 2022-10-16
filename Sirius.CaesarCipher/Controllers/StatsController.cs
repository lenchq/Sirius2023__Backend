using Microsoft.AspNetCore.Mvc;
using Sirius.CaesarCipher.Interfaces;

namespace Sirius.CaesarCipher.Controllers;

[ApiController]
[Route("")]
public sealed class StatsController : ControllerBase
{
    private readonly IShiftRepository _repo;
    private readonly ILogger<StatsController> _logger;
    private readonly IRotStatisticsProvider _rotStats;

    public StatsController(ILogger<StatsController> logger,
        IShiftRepository repo,
        IRotStatisticsProvider rotStatistics)
    {
        _logger = logger;
        _repo = repo;
        _rotStats = rotStatistics;
    }

    [HttpGet("stats")]
    public async Task<IActionResult> GetStats()
    {
        var usageStats = await _rotStats.GetStatisticsAsync();

        return new JsonResult(usageStats);
    }
}