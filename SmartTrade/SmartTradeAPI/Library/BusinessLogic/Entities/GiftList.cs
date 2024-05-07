using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTrade.Entities;

public partial class GiftList
{
   public GiftList() 
    {
        Gifts = new List<Gift>();
    }

    public GiftList(string Name, DateOnly? Date, string ConsumerEmail):this()
    {
        Name = Name;
        Date = Date;
        ConsumerEmail = ConsumerEmail;
    }
}