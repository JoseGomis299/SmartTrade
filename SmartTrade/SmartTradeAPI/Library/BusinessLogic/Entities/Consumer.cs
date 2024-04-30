namespace SmartTrade.Entities;

public partial class Consumer : User
{
    public Consumer()
    {
        Addresses = new List<Address>();
        PayPalAccounts = new List<PayPalInfo>();
        BizumAccounts = new List<BizumInfo>();
        CreditCards = new List<CreditCardInfo>();
        Alerts = new List<Alert>();
        WishList = new List<Wish>();
        Purchases = new List<Purchase>();
        ShoppingCart = new List<CartItem>();
    }

    public Consumer(string email, string password, string name, string lastNames, string dni, DateTime birthDate, Address billingAddress, Address address) : base(email, password, name, lastNames)
    {
        DNI = dni;
        BirthDate = birthDate;
        BillingAddress = billingAddress;

        Addresses = new List<Address>();
        PayPalAccounts = new List<PayPalInfo>();
        BizumAccounts = new List<BizumInfo>();
        CreditCards = new List<CreditCardInfo>();
        Alerts = new List<Alert>();
        WishList = new List<Wish>();
        Purchases = new List<Purchase>();   

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

    public void AddToCart(CartItem cartItem)
    {
        var index = ((List<CartItem>)ShoppingCart).FindIndex(x => x.Offer.Id == cartItem.Offer.Id);

        if(index == -1)
            ShoppingCart.Add(cartItem);
        else ((List<CartItem>)ShoppingCart)[index].Quantity = cartItem.Quantity;
    }

    public void AddShippingAddress(Address address)
    {
        Addresses.Add(address);
    }

    public void AddAlert(Alert alert)
    {
        Alerts.Add(alert);
    }

    public void AddWish(Wish wish)
    {
        WishList.Add(wish);
    }

    public void AddPurchases(Purchase purchase)
    {
        Purchases.Add(purchase);
    }

    public void RemoveFromCart(int OfferId)
    {
        var index = ((List<CartItem>)ShoppingCart).FindIndex(x => x.Offer.Id == OfferId);
        if (index != -1) ((List<CartItem>)ShoppingCart).RemoveAt(index);
    }
}