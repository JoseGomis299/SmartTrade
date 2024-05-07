namespace SmartTrade.Entities;

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
    public virtual ICollection<Wish> WishList { get; set; }
    public virtual ICollection<Purchase> Purchases { get; set;}
    public virtual ICollection<CartItem> ShoppingCart { get; set;}
    public virtual ICollection<Notification> Notifications { get; set; }
    public virtual ICollection<GiftList> GiftLists { get; set; }
}