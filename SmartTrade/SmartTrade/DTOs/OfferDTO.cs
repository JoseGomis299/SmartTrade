namespace SmartTradeDTOs;

public class OfferDTO
{
    public int Stock { get; set; }
    public float Price { get; set; }
    public float ShippingCost { get; set; }
    public ProductDTO Product { get; set; }

}