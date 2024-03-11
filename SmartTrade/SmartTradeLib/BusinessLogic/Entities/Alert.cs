namespace SmartTradeLib.Entities;

public partial class Alert
{
    public Alert(User user, Product product)
    {
        User = user;
        Product = product;
    }
}