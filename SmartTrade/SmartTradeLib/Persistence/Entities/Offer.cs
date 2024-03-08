using System.ComponentModel.DataAnnotations;

namespace SmartTradeLib.Entities;

public partial class Offer
{
    [Key]
    public virtual Post Post { get; set; }
    public float Price { get; set; }
    public float ShippingCost { get; set; }
    public int Stock { get; set; }

}