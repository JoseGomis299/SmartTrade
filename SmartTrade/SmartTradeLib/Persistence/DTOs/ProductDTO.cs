using SmartTradeLib.Entities;

namespace SmartTradeDTOs;

public class ProductDTO
{
    public List<byte[]> Images { get; set; }
    public List<string> Attributes { get; set; }

    public ProductDTO(Product product)
    {
        Images = product.Images.Select(i => i.ImageSource).ToList();
        Attributes = product.GetAttributes().ToList();
    }
}