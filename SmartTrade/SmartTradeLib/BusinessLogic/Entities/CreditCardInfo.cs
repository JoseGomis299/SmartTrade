namespace SmartTradeLib.Entities;

public partial class CreditCardInfo : IPayMethod
{
    public CreditCardInfo(string cardNumber, string expirationDate, string cvv, string cardHolder)
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