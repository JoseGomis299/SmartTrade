namespace SmartTrade.Entities;

public partial class CreditCardInfo : IPayMethod
{
    public CreditCardInfo() { }
    public CreditCardInfo(string cardNumber, DateTime expirationDate, string cvv, string cardHolder)
    {
        CardNumber = cardNumber;
        ExpirationDate = expirationDate;
        CVV = cvv;
        CardHolder = cardHolder;
    }

    public void Pay(float amount)
    {

    }
}