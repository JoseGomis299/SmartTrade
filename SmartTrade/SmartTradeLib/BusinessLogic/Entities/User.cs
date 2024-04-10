namespace SmartTrade.Entities;

public abstract partial class User
{
    protected User(){}
    protected User(string email, string password, string name, string lastNames)
    {
        Email = email;
        Password = password;
        Name = name;
        LastNames = lastNames;
    }
}