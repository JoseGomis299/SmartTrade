using SmartTradeLib.Entities;

namespace SmartTradeAPI.Library.Persistence.NewFolder;

public class PostInfo
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? ProductName { get; set; }
    public Category Category { get; set; }
    public int MinimumAge { get; set; }
    public string HowToUse { get; set; }
    public string? Certifications { get; set; }
    public string? EcologicPrint { get; set; }
    public string? HowToReducePrint { get; set; }
    public bool Validated { get; set; }
    public string? SellerID { get; set; }
    public List<OfferInfo> Offers { get; set; }

    public class OfferInfo
    {
        public int stock { get; set; }
        public float price { get; set; }
        public float shippingCost { get; set; }

        public List<ProductInfo> products { get; set; }

        public class ProductInfo
        {
            public List<byte[]> images { get; set; }
            public List<string> attributes { get; set; }
        }
    }
}