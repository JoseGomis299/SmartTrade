using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTrade.Entities;

public partial class Gift
{
   public Gift() 
    { 
    }

    public Gift(int quantity, Post post, Offer offer)
    {
        Quantity = quantity;
        Post = post;
        Offer = offer;
    }
}