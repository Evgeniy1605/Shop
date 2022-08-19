namespace Shop.Data;
using Microsoft.EntityFrameworkCore;
using Shop.Models;

public class OrderDbConenction : DbContext
{
    public DbSet<InformationAboutNewProduct> InformationAbautNewProdact { get; set; }
    public DbSet<PurchaseModel> AllPerchaseItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<AdminModel> Admins { get; set; }
    public DbSet<UserModel> Users { get; set; } 
    public OrderDbConenction(DbContextOptions<OrderDbConenction> options) : base(options)
    {

    }
}
