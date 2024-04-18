using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTrade.Entities;

public abstract partial class User
{
    protected User()
    {
        Alerts = new List<Alert>();
    }
    protected User(string email, string password, string name, string lastNames)
    {
        Email = email;
        Password = password;
        Name = name;
        LastNames = lastNames;
    }
}
