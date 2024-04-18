using SmartTrade.Entities;

namespace SmartTradeDTOs;

public class OfferDTO
{
    public int Stock { get; set; }
    public float Price { get; set; }
    public float ShippingCost { get; set; }
    public ProductDTO Product { get; set; }

    public OfferDTO() { }
    public OfferDTO(Offer offer)
    {
        Stock = offer.Stock;
        Price = offer.Price;
        ShippingCost = offer.ShippingCost;
        Product = new ProductDTO(offer.Product);
    }
}