namespace SmartTradeLib.Entities;

public partial class Consumer : User
{
    public string DNI { get; set; }
    public DateTime BirthDate { get; set; }

    public virtual Address BillingAddress { get; set; }
    public virtual ICollection<Address> Addresses { get; set; }
    public virtual ICollection<PayPalInfo> PayPalAccounts { get; set; }
    public virtual ICollection<BizumInfo> BizumAccounts { get; set; }
    public virtual ICollection<CreditCardInfo> CreditCards { get; set; }
    public virtual ICollection<Alert> Alerts { get; set; }
}