namespace SmartTrade.Entities;

public partial class Post
{
    public Post()
    {
        Offers = new List<Offer>();
        Ratings = new List<Rating>();
    }
    public Post(string title, string description, bool validated, Seller? seller) : this()
    {
        Title = title;
        Description = description;
        Validated = validated;
        Seller = seller;
    }

    public void AddRating(Rating rating)
    {
        Ratings.Add(rating);
    }
}