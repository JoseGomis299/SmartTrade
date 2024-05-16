using SmartTrade.Entities;
using SmartTradeDTOs;
using System;

namespace SmartTradeDTOs
{
    public class PurchaseDTO
    {
        public float Price { get; set; }
        public float ShippingPrice { get; set; }
        public int Quantity { get; set; }
        public int? ProductId { get; set; }
        public string? EmailSeller { get; set; }
        public PostDTO Post { get; set; }
        public OfferDTO Offer { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpectedDate { get; set; }

        public PurchaseDTO(){}
        
        public PurchaseDTO(float price, float shippingPrice, int quantity, int productId, string emailSeller, PostDTO postId, OfferDTO offerId, DateTime purchaseDate, DateTime expectedDate)
        {
            Price = price;
            ShippingPrice = shippingPrice;
            Quantity = quantity;
            ProductId = productId;
            EmailSeller = emailSeller;
            Post = postId;
            Offer = offerId;
            PurchaseDate = purchaseDate;
            ExpectedDate = expectedDate;
        }
    }
}
