using System.Diagnostics;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartTrade.Entities;

namespace SmartTrade.Persistence;

public class SmartTradeContext : BaseDbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Consumer> Consumers { get; set; }
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
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Image> Image { get; set; }
    public DbSet<Notification> Notification { get; set; }

    public SmartTradeContext()
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.LogTo(message => Debug.WriteLine(message));
        options.UseLazyLoadingProxies();
        options.UseSqlServer("Server=tcp:furgoneta.database.windows.net,1433;Initial Catalog=SmartTradeDB;Persist Security Info=False;User ID=CloudSAf9df27ed;Password=123456789Aa;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }

    /// <summary>
    /// Delete every object stored in the DbSets
    /// Every class in the Namespace "...Entities" must have a DbSet in the DbContext
    /// </summary>
    public virtual void RemoveAllData()
    {
        if (!Image.IsNullOrEmpty()) Image.RemoveRange(Image);
        if (!Products.IsNullOrEmpty()) Products.RemoveRange(Products);
        if(!Admins.IsNullOrEmpty()) Admins.RemoveRange(Admins);
        if(!Consumers.IsNullOrEmpty()) Consumers.RemoveRange(Consumers);
        if(!Sellers.IsNullOrEmpty()) Sellers.RemoveRange(Sellers);
        if(!Posts.IsNullOrEmpty()) Posts.RemoveRange(Posts);
        if(!Offers.IsNullOrEmpty()) Offers.RemoveRange(Offers);
        if(!CreditCards.IsNullOrEmpty()) CreditCards.RemoveRange(CreditCards);
        if(!PayPals.IsNullOrEmpty()) PayPals.RemoveRange(PayPals);
        if(!Bizums.IsNullOrEmpty()) Bizums.RemoveRange(Bizums);
        if(!Alerts.IsNullOrEmpty()) Alerts.RemoveRange(Alerts);
        if(!Addresses.IsNullOrEmpty()) Addresses.RemoveRange(Addresses);
        if(!Notification.IsNullOrEmpty()) Notification.RemoveRange(Notification);

        SaveChanges();
    }
}