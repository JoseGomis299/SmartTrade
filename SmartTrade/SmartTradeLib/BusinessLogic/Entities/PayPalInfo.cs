namespace SmartTradeLib.Entities;

public partial class PayPalInfo : IPayMethod
{
    public PayPalInfo() { }
    public PayPalInfo(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public void Pay(float amount)
    {
            
    }
}