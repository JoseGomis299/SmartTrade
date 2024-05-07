using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartTradeDTOs;
using SmartTrade.Entities;

namespace SmartTradeDTOs
{
    public class GiftListDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateOnly? Date { get; set; }
        public string? ConsumerEmail { get; set; }
        public List<GiftDTO> Gifts { get; set; }

        public GiftListDTO()
        {
            Gifts = new List<GiftDTO>();
        }

        public GiftListDTO(string name, DateOnly? date, string consumerId, List<GiftDTO> gifts) 
        {
            Name = name;
            Date = date;
            ConsumerEmail = consumerId;
            Gifts = gifts;
        }
    }
}
