using SmartTrade.Entities;

namespace SmartTradeDTOs;

public class UserDTO
{
    public bool IsSeller { get; set; }
    public bool IsConsumer { get; set; }
    public bool IsAdmin { get; set; }

    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string LastNames { get; set; }

    public UserType GetUserType()
    {
        if (IsSeller)
        {
            return UserType.Seller;
        }
        else if (IsConsumer)
        {
            return UserType.Consumer;
        }
        else if (IsAdmin)
        {
            return UserType.Admin;
        }
        else
        {
            return UserType.None;
        }
    }
}

public enum UserType
{
    Consumer,
    Seller,
    Admin,
    None
}
