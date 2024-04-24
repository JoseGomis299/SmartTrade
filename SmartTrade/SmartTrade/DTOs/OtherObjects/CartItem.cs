using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartTradeDTOs;

namespace SmartTrade.Entities
{
    public class CartItem
    {
        public PostDTO Post { get; set; }
        public int OfferIndex { get; set; }
        public int Quantity { get; set; }

        public CartItem(PostDTO post, int offerIndex, int quantity = 1)
        {
            Post = post;
            OfferIndex = offerIndex;
            Quantity = quantity;
        }
    }
}
