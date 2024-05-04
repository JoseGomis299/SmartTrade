using SmartTrade.Entities;

namespace SmartTradeDTOs;

public class SimplePostDTO
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public Category Category { get; set; }
    public int MinimumAge { get; set; }
    public string? EcologicPrint { get; set; }
    public bool Validated { get; set; }
    public string? SellerID { get; set; }
    public float Price { get; set; }
    public float ShippingCost { get; set; }
    public byte[]? Image { get; set; }
    public string ProductName { get; set; }

    public SimplePostDTO()
    {

    }

    public SimplePostDTO(PostDTO post)
    {
        Id = post.Id;
        Title = post.Title;
        Category = post.Category;
        MinimumAge = post.MinimumAge;
        EcologicPrint = post.EcologicPrint;
        Validated = post.Validated;
        SellerID = post.SellerID;
        Price = post.Offers[0].Price;
        Image = post.Offers[0].Product.Images[0];
        ProductName = post.ProductName;
        ShippingCost = post.Offers[0].ShippingCost;
    }
}