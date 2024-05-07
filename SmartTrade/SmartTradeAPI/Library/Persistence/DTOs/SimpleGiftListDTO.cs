using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartTradeDTOs;
using SmartTrade.Entities;

namespace SmartTradeDTOs
{
    public class SimpleGiftListDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public string? ConsumerEmail { get; set; }

        public SimpleGiftListDTO()
        {
        }

        public SimpleGiftListDTO(string name, DateTime? date, string consumerId)
        {
            Name = name;
            Date = date;
            ConsumerEmail = consumerId;
        }
    }
}
