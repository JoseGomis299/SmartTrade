using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SmartTradeLib.Entities;

namespace SmartTradeLib.Persistence;

public class SmartTradeContext : BaseDbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Costumer> Costumers { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Clothing> Clothing { get; set; }
    public DbSet<Toy> Toys { get; set; }
    public DbSet<Nutrition> Foods { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<CreditCardInfo> CreditCards { get; set; }
    public DbSet<PayPalInfo> PayPals { get; set; }
    public DbSet<BizumInfo> Bizums { get; set; }
    public DbSet<Alert> Alerts { get; set; }
    public DbSet<Address> Adresses { get; set; }

    public SmartTradeContext()
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("Server=tcp:furgoneta.database.windows.net,1433;Initial Catalog=SmartTradeDB;Persist Security Info=False;User ID=CloudSAf9df27ed;Password=123456789Aa;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");


    /// <summary>
    /// Delete every object stored in the DbSets
    /// Every class in the Namespace "...Entities" must have a DbSet in the DbContext
    /// </summary>
    public virtual void RemoveAllData()
    {
        if(Products.Any()) Products.RemoveRange(Products);
        if(Admins.Any()) Admins.RemoveRange(Admins); 
        if(Books.Any()) Books.RemoveRange(Books);
        if(Costumers.Any()) Costumers.RemoveRange(Costumers);
        if(Alerts.Any()) Alerts.RemoveRange(Alerts);
        if(CreditCards.Any()) CreditCards.RemoveRange(CreditCards);
        if(PayPals.Any()) PayPals.RemoveRange(PayPals);
        if(Bizums.Any()) Bizums.RemoveRange(Bizums);
        if(Adresses.Any()) Adresses.RemoveRange(Adresses);
        if(Sellers.Any()) Sellers.RemoveRange(Sellers);
        if(Posts.Any()) Posts.RemoveRange(Posts);
        if(Offers.Any()) Offers.RemoveRange(Offers);
        if(Clothing.Any()) Clothing.RemoveRange(Clothing);
        if(Toys.Any()) Toys.RemoveRange(Toys);
        if(Foods.Any()) Foods.RemoveRange(Foods);

        SaveChanges();
    }
}