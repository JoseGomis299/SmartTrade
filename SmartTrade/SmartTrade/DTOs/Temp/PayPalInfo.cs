﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTrade.Entities;

public class PayPalInfo 
{
    public string Email { get; set; }
    public string Password { get; set; }
}