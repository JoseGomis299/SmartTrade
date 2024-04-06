﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTradeLib.Entities;

public partial class PayPalInfo : IPayMethod
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}