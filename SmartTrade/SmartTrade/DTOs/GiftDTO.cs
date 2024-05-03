using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartTradeDTOs;

namespace SmartTrade.Entities
{
    public class GiftDTO
    {
        public string ListName { get; set; }
        public DateOnly? Date { get; set; }
        public int? Quantity { get; set; }
        public virtual string? ConsumerEmail { get; set; }
        public virtual PostDTO? PostId { get; set; }
        public virtual OfferDTO? OfferId { get; set; }

        public GiftDTO()
        {
        }

        public GiftDTO(string ListName, DateOnly? Date, int? Quantity, string? ConsumerEmail, PostDTO? PostId, OfferDTO? OfferId) 
        {
            ListName = ListName;
            Date = Date;
            Quantity = Quantity;
            ConsumerEmail = ConsumerEmail;
            PostId = PostId;
            OfferId = OfferId;
        }
    }
}
