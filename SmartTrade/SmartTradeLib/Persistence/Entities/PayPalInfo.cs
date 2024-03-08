namespace SmartTradeLib.Entities;

public partial class PayPalInfo : IPayMethod
{
    public string Email { get; set; }
    public string Password { get; set; }
}