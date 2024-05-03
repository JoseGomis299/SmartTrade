namespace SmartTrade.Entities;

public partial class Alert
{
    public Alert(){}
    public Alert(Consumer user, Product product)
    {
        User = user;
        Product = product;
    }
}