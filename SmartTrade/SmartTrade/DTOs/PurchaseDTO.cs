using SmartTrade.Entities;
using System;

namespace SmartTradeAPI.Library.Persistence.DTOs
{
    public class PurchaseDTO
    {
        public float Price { get; set; }
        public float ShippingPrice { get; set; }
        public int? ProductId { get; set; }
        public string? EmailSeller { get; set; }
        public int? PostId { get; set; }
        public int? OfferId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpectedDate { get; set; }

        public PurchaseDTO(){}
        
        public PurchaseDTO(float price, float shippingPrice, int productId, string emailSeller, int postId, int offerId, DateTime purchaseDate, DateTime expectedDate)
        {
            Price = price;
            ShippingPrice = shippingPrice;
            ProductId = productId;
            EmailSeller = emailSeller;
            PostId = postId;
            OfferId = offerId;
            PurchaseDate = purchaseDate;
            ExpectedDate = expectedDate;
        }
    }
}
