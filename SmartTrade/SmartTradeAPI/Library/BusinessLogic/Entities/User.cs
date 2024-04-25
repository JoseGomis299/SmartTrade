using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTrade.Entities;

public abstract partial class User
{
    protected User()
    {
        Alerts = new List<Alert>();
        WishList = new List<Wish>();
    }
    protected User(string email, string password, string name, string lastNames)
    {
        Email = email;
        Password = password;
        Name = name;
        LastNames = lastNames;
    }

    public void AddAlert(Alert alert)
    {
        Alerts.Add(alert);
    }

    public void AddWish(Wish wish)
    {
        WishList.Add(wish);
    }
}
