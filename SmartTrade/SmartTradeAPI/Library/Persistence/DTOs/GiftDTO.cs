using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartTrade.Entities;

namespace SmartTradeDTOs
{
    public class GiftDTO
    {
        public int Quantity { get; set; }
        public PostDTO Post { get; set; }
        public OfferDTO Offer { get; set; }
        public string GiftListName { get; set; }

        public GiftDTO()
        {
        }

        public GiftDTO(int quantity, PostDTO post, OfferDTO offer, string giftListName) 
        {
            Quantity = quantity;
            Post = post;
            Offer = offer;
            GiftListName = giftListName;
        }
    }
}
