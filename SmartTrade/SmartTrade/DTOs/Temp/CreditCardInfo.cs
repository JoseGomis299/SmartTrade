namespace SmartTrade.Entities;

public class CreditCardInfo
{
    private string creditCardNumber { get; set; }
    private string creditCardExpiryDate { get; set; }
    private string creditCardCVV { get; set; }
    private string creditCardName { get; set; }

    public CreditCardInfo(string creditCardNumber, string creditCardExpiryDate, string creditCardCVV, string creditCardName)
    {
        this.creditCardNumber = creditCardNumber;
        this.creditCardExpiryDate = creditCardExpiryDate;
        this.creditCardCVV = creditCardCVV;
        this.creditCardName = creditCardName;
    }

    
}