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
        public string ListName { get; set; }
        public DateOnly? Date { get; set; }
        public int? Quantity { get; set; }
        public virtual string? ConsumerEmail { get; set; }
        public virtual int? PostId { get; set; }
        public virtual int? OfferId { get; set; }

        public SimpleGiftDTO()
        {
        }

        public SimpleGiftDTO(string ListName, DateOnly? Date, int? Quantity, string? ConsumerEmail, int? PostId, int? OfferId) 
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
