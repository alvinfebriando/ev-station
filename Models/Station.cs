namespace astra_otoparts;

public class Station
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public double TotalKwh { get; set; }
    public string Status { get; set; }
    public List<Order> Orders { get; } = [];
}
