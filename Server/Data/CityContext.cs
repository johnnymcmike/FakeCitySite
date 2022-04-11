using FakeCitySite.Shared;
using Microsoft.EntityFrameworkCore;

namespace FakeCitySite.Server.Data;

public class CityContext : DbContext
{
    public CityContext(DbContextOptions<CityContext> options) : base(options)
    {
        
    }
    public CityContext(string connectionString) : base(new DbContextOptionsBuilder<CityContext>().UseSqlite(connectionString).Options)
    {
        
    }
    public DbSet<City> Cities { get; set; }
}