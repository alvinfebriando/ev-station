using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace astra_otoparts;

[ApiController]
[Route("[controller]")]
public class ReportController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public ReportController(ApplicationDbContext context)
    {
        _context = context;
    }
    [HttpGet("kwh_total")]
    public async Task<IActionResult> GetKwhTotal(GetReportRequest request)
    {
        var totalKwh = await _context.Orders
            .Where(o => o.StartTime <= request.End && o.StartTime >= request.Start)
            .SumAsync(o => o.TotalKwh);
        return Ok(totalKwh);
    }

    [HttpGet("station_status")]
    public async Task<IActionResult> GetStationStatus()
    {
        var stations = await _context.Stations.GroupBy(s => s.Status).CountAsync();
        return Ok(stations);
    }

    [HttpGet("available_station")]
    public async Task<IActionResult> GetAvailableStation()
    {
        var stations = await _context.Stations.Where(s => s.Status == "unused").ToListAsync();
        return Ok(new
        {
            Stations = stations,
            TotalKwh = stations.Sum(s => s.TotalKwh)
        });
    }
}
