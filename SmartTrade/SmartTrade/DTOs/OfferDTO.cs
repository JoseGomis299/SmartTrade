using SmartTrade.Entities;

namespace SmartTradeDTOs;

public class OfferDTO
{
    public int Id { get; set; }
    public int Stock { get; set; }
    public float Price { get; set; }
    public float ShippingCost { get; set; }
    public ProductDTO Product { get; set; }

}