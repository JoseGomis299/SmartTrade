using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTrade.Entities;

public partial class Gift
{
   public Gift() 
    { 
    }

    public Gift(string listName, DateOnly? date, int? quantity, Post? post, Offer? offer):this()
    {
        ListName = listName;
        Date = date;
        Quantity = quantity;
        Post = post;
        Offer = offer;
    }
}