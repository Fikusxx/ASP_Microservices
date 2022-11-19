using Microsoft.EntityFrameworkCore;

namespace Shop.Services.OrderAPI;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    public DbSet<OrderHeader> OrderHeaders { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }
}
