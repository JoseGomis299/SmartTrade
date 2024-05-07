using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTrade.Entities;

public partial class GiftList
{
   public GiftList() 
    {
        Gifts = new List<Gift>();
    }

    public GiftList(string name, DateOnly? date, string consumerEmail, int id) : this()
    {
        Name = name;
        Date = date;
        ConsumerEmail = consumerEmail;
        Id = id;
    }
}