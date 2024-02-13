using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace astra_otoparts;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public OrderController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> AddOrder(AddOrderRequest request)
    {
        var station = await _context.Stations.Where(s => s.Id == request.StationId).SingleOrDefaultAsync();
        if (station is null)
        {
            return NotFound();
        }
        var pricePerKwh = 1000;
        var order = new Order
        {
            TotalKwh = request.Kwh,
            Price = request.Kwh * pricePerKwh
        };
        station.Status = "used";
        _context.Stations.Update(station);
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProgress(int id, UpdateProgressRequest request)
    {
        var order = await _context.Orders.Where(o => o.Id == id).SingleOrDefaultAsync();
        if (order is null)
        {
            return NotFound();
        }
        if (order.EndTime is not null)
        {
            return BadRequest();
        }
        order.CurrentKwh = request.Kwh;
        if (order.CurrentKwh == order.TotalKwh)
        {
            order.EndTime = DateTime.Now;
        }
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
