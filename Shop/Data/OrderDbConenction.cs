namespace Shop.Data;
using Microsoft.EntityFrameworkCore;
using Shop.Models;

public class OrderDbConenction : DbContext
{
    public DbSet<PerchaseModel> AllPerchaseItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public OrderDbConenction(DbContextOptions<OrderDbConenction> options) : base(options)
    {

    }
}
