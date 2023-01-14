using Microsoft.EntityFrameworkCore;
using Orders.DAL.Entities;

namespace Orders.DAL;

public class OrderDbContext : DbContext
{
    public DbSet<Buyer> Buyers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    public OrderDbContext(DbContextOptions options) : base(options) { }
}