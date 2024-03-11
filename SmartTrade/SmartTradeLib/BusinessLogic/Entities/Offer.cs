namespace SmartTradeLib.Entities;

public partial class Offer
{
    public Offer(Post post, Product product, float price, float shippingCost, int stock)
    {
        Post = post;
        Product = product;
        Price = price;
        ShippingCost = shippingCost;
        Stock = stock;
    }
}