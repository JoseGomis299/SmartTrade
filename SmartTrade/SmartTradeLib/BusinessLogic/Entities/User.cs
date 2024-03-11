namespace SmartTradeLib.Entities;

public partial class User
{
    public User(string email, string password, string name, string lastNames)
    {
        Email = email;
        Password = password;
        Name = name;
        LastNames = lastNames;
    }
}