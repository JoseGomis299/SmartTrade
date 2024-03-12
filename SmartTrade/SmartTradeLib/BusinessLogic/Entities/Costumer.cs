namespace SmartTradeLib.Entities;

public partial class Costumer : User
{
    public Costumer()
    {
        Addresses = new List<Address>();
        PayPalAccounts = new List<PayPalInfo>();
        BizumAccounts = new List<BizumInfo>();
        CreditCards = new List<CreditCardInfo>();
        Alerts = new List<Alert>();
    }

    public Costumer(string email, string password, string name, string lastNames, string dni, DateTime birthDate, Address billingAddress, Address address) : base(email, password, name, lastNames)
    {
        DNI = dni;
        BirthDate = birthDate;
        BillingAddress = billingAddress;

        Addresses = new List<Address>();
        PayPalAccounts = new List<PayPalInfo>();
        BizumAccounts = new List<BizumInfo>();
        CreditCards = new List<CreditCardInfo>();
        Alerts = new List<Alert>();

        Addresses.Add(address);
    }

    public void AddPaymentMethod(IPayMethod payMethod)
    {
        if (payMethod is BizumInfo)
        {
            BizumAccounts.Add((BizumInfo) payMethod);
        }else if (payMethod is CreditCardInfo)
        {
            CreditCards.Add((CreditCardInfo) payMethod);
        }else if (payMethod is PayPalInfo)
        {
            PayPalAccounts.Add((PayPalInfo) payMethod);
        }
    }

    public void AddShippingAddress(Address address)
    {
        Addresses.Add(address);
    }

    public void AddAlert(Alert alert)
    {
        Alerts.Add(alert);
    }
}