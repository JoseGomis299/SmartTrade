using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartTradeDTOs;

namespace SmartTrade.Entities
{
    public class CartItemDTO
    {
        public PostDTO Post { get; set; }
        public OfferDTO Offer { get; set; }
        public int EstimatedShippingDays { get; set; }
        public int Quantity { get; set; }

        public CartItemDTO(PostDTO post, OfferDTO offer, int quantity = 1)
        {
            Post = post;
            Offer = offer;
            Quantity = quantity;

            Random random = new Random(offer.Id);
            EstimatedShippingDays = random.Next(3, 10);
        }
    }
}
