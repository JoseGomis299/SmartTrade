using System;

namespace SmartTrade.Entities;

public class CreditCardInfo
{
    public string CreditCardNumber { get; set; }
    public DateTime CreditCardExpiryDate { get; set; }
    public string CreditCardCvv { get; set; }
    public string CreditCardName { get; set; }

    public CreditCardInfo(string creditCardNumber, DateTime creditCardExpiryDate, string creditCardCVV, string creditCardName)
    {
        this.CreditCardNumber = creditCardNumber;
        this.CreditCardExpiryDate = creditCardExpiryDate;
        this.CreditCardCvv = creditCardCVV;
        this.CreditCardName = creditCardName;
    }

    
}