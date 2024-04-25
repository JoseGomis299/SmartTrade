namespace SmartTrade.Entities;

public partial class Wish
{
    public Wish() { }
    public Wish(User user, Post post)
    {
        User = user;
        Post = post;
    }
}