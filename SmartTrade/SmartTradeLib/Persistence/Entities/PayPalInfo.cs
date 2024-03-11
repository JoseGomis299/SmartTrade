using System.ComponentModel.DataAnnotations;

namespace SmartTradeLib.Entities;

public partial class PayPalInfo : IPayMethod
{
    [Key]
    public string Email { get; set; }
    public string Password { get; set; }
}