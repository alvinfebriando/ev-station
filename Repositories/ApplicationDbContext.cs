using Microsoft.EntityFrameworkCore;

namespace astra_otoparts;

public class ApplicationDbContext : DbContext
{
    public DbSet<Station> Stations { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<User> Users { get; set; }
    public string DbPath { get; }
    public ApplicationDbContext()
    {
        DbPath = Path.Join("stations.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
           => options.UseSqlite($"Data Source={DbPath}");
}
