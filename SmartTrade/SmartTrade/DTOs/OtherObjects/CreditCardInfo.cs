using System;

namespace SmartTrade.Entities;

public class CreditCardInfo
{
    public string CardNumber { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string CVV { get; set; }
    public string CardHolder { get; set; }

    public CreditCardInfo(string creditCardNumber, DateTime creditCardExpiryDate, string creditCardCVV, string creditCardName)
    {
        this.CardNumber = creditCardNumber;
        this.ExpirationDate = creditCardExpiryDate;
        this.CVV = creditCardCVV;
        this.CardHolder = creditCardName;
    }

    
}