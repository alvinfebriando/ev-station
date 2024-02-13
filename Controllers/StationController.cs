using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace astra_otoparts;

[ApiController]
[Route("[controller]")]
public class StationController : ControllerBase
{
    private ApplicationDbContext _context;
    public StationController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStations()
    {
        return Ok(await _context.Stations.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStationById(int id)
    {

        var fetchedStation = await _context.Stations.Where(s => s.Id == id).SingleOrDefaultAsync();
        if (fetchedStation is null)
        {
            return NotFound();
        }
        return Ok(fetchedStation);
    }

    [HttpPost]
    public async Task<IActionResult> AddStation(AddStationRequest request)
    {
        var station = new Station
        {
            Name = request.Name,
            Address = request.Address,
            Status = "unused"
        };
        try
        {
            _context.Stations.Add(station);
            await _context.SaveChangesAsync();
            return Created();
        }
        catch (DbException)
        {
            return StatusCode(500);
        }
    }
}
