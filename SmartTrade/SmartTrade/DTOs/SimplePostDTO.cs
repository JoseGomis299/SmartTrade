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
    public byte[]? Image { get; set; }
    public string ProductName { get; set; }
}