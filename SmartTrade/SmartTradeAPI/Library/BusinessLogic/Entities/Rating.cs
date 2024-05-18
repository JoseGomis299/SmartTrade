namespace SmartTrade.Entities;

public partial class Rating
{
    public Rating() { }

    public Rating(int points, string description, Consumer user, Post post)
    {
        Points = points;
        Description = description;
        User = user;
        Post = post;
    }
}