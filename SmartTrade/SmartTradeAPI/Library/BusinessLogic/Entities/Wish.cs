namespace SmartTrade.Entities;

public partial class Wish
{
    public Wish() { }
    public Wish(Consumer user, Post post)
    {
        User = user;
        Post = post;
    }
}