﻿namespace Shop.Data;
using Microsoft.EntityFrameworkCore;
using Shop.Models;

public class OrderDbConenction : DbContext
{
    public DbSet<InformationAbautNewProdact> InformationAbautNewProdact { get; set; }
    public DbSet<PerchaseModel> AllPerchaseItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<AdminModel> Admins { get; set; }
    public DbSet<UserModel> Users { get; set; } 
    public OrderDbConenction(DbContextOptions<OrderDbConenction> options) : base(options)
    {

    }
}
