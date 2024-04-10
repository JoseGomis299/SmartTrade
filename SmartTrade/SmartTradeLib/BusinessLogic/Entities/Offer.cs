using System.Diagnostics;

namespace SmartTrade.Entities;

public partial class Offer
{
    public Offer() { }
    public Offer(Product product, float price, float shippingCost, int stock)
    {
        Product = product;
        Price = price;
        ShippingCost = shippingCost;
        Stock = stock;
    }
}

