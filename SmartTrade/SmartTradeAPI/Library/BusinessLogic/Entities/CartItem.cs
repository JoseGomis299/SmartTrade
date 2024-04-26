using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartTradeDTOs;

namespace SmartTrade.Entities
{
    public partial class CartItem
    {
        public CartItem() { }

        public CartItem(Post post, Offer offer, int quantity)
        {
            Post = post;
            Offer = offer;
            Quantity = quantity;
        }
    }
}
