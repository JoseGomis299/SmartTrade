using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTradeDTOs
{
    public class PurchaseDTO
    {
        public float Price { get; set; }
        public float ShippingPrice { get; set; }
        public int? ProductId { get; set; }
        public string? EmailSeller { get; set; }
        public int? PostId { get; set; }
        public int? OfferId { get; set; }

        public PurchaseDTO()
        {
        }

        public PurchaseDTO(float price, float shippingPrice, int productId, string emailSeller, int postId, int offerId)
        {
            Price = price;
            ShippingPrice = shippingPrice;
            ProductId = productId;
            EmailSeller = emailSeller;
            PostId = postId;
            OfferId = offerId;
        }
    }
}
