﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTradeLib.Entities;

public partial class Offer
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public virtual Post Post { get; set; }
    [Required]
    public virtual Product Product { get; set; }
    [Required]
    public float Price { get; set; }
    [Required]
    public float ShippingCost { get; set; }
    [Required]
    public int Stock { get; set; }

}

public partial class OfferDTO
{
    public float Price { get; set; }
    public float ShippingCost { get; set; }
    public int Stock { get; set; }
}