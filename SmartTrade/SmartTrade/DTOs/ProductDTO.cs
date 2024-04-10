using System.Collections.Generic;

namespace SmartTradeDTOs;

public class ProductDTO
{
    public List<byte[]> Images { get; set; }
    public List<string> Attributes { get; set; }
}