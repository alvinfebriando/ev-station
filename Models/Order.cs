namespace astra_otoparts;

public class Order
{
    public int Id { get; set; }
    public double TotalKwh { get; set; }
    public double CurrentKwh { get; set; }
    public decimal Price { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int StationId { get; set; }
    public Station Station { get; set; }
}
