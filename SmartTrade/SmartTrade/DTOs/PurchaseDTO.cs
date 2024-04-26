using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTrade.DTOs
{
    public class PurchaseDTO
    {
        public float Price { get; set; }
        public float ShippingPrice { get; set; }
        public int? ProductId { get; set; }
        public string? EmailSeller { get; set; }
        public int? PostId { get; set; }
        public int? OfferId { get; set; }
    }
}
