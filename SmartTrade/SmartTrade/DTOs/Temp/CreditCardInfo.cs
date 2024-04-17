namespace SmartTrade.Entities;

public class CreditCardInfo
{
    private string creditCardNumber;
    private string creditCardExpiryDate;
    private string creditCardCVV;
    private string creditCardName;

    public CreditCardInfo(string creditCardNumber, string creditCardExpiryDate, string creditCardCVV, string creditCardName)
    {
        this.creditCardNumber = creditCardNumber;
        this.creditCardExpiryDate = creditCardExpiryDate;
        this.creditCardCVV = creditCardCVV;
        this.creditCardName = creditCardName;
    }

    public string CardNumber { get; set; }
    public string ExpirationDate { get; set; }
    public string CVV { get; set; }
    public string CardHolder { get; set; }
}