namespace SmartTrade.Entities;

public partial class Alert
{
    public Alert(){}
    public Alert(Consumer user, string productName)
    {
        User = user;
        ProductName = productName;
    }
}