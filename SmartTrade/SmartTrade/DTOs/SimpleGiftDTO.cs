using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTradeDTOs
{
    public class SimpleGiftDTO
    {
        public int Quantity { get; set; }
        public int PostId { get; set; }
        public int OfferId { get; set; }
        public string GiftListName { get; set; }

        public SimpleGiftDTO()
        {
        }

        public SimpleGiftDTO(int quantity, int postId, int offerId, string giftListName) 
        {
            Quantity = quantity;
            PostId = postId;
            OfferId = offerId;
            GiftListName = giftListName;
        }
    }
}
