using System.Collections.Generic;
using SmartTrade.Entities;

namespace SmartTradeDTOs;

public class ProductDTO
{
    public int Id { get; set; }
    public List<byte[]> Images { get; set; }
    public Dictionary<string, string> Attributes { get; set; }
    public string? Differentiators { get; set; }
    public string? Info { get; set; }
}