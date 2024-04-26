using SmartTrade.Entities;

namespace SmartTradeDTOs;

public class ProductDTO
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string? Certification { get; set; }
    public string? EcologicPrint { get; set; }
    public string? HowToReducePrint { get; set; }
    public int MinimumAge { get; set; }
    public string HowToUse { get; set; }
    public List<byte[]> Images { get; set; }
    public Dictionary<string, string> Attributes { get; set; }
    public string? Differentiators { get; set; }
    public string? Info { get; set; }
    public List<string> UsersWithAlertsInThisProduct { get; set; }

    public ProductDTO(){}
    public ProductDTO(Product product)
    {
        Id = product.Id;
        Images = product.Images.Select(i => i.ImageSource).ToList();
        Attributes = product.GetAttributes();
        Differentiators = product.GetDifferentiations();
        Info = product.GetInfo();
        Name = product.Name;
        Certification = product.Certification;
        EcologicPrint = product.EcologicPrint;
        HowToReducePrint = product.HowToReducePrint;
        MinimumAge = product.MinimumAge;
        HowToUse = product.HowToUse;
        UsersWithAlertsInThisProduct = product.Alerts.Select(a => a.User.Email).ToList();
    }
}