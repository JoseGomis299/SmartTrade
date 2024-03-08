namespace SmartTradeLib.Entities;

public partial class CreditCardInfo : IPayMethod
{
    public string CardNumber { get; set; }
    public string ExpirationDate { get; set; }
    public string CVV { get; set; }
    public string CardHolder { get; set; }
}