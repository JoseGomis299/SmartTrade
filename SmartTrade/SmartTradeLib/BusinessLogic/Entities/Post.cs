namespace SmartTrade.Entities;

public partial class Post
{
    public Post()
    {
        Offers = new List<Offer>();
    }
    public Post(string title, string description, bool validated, Seller? seller) : this()
    {
        Title = title;
        Description = description;
        Validated = validated;
        Seller = seller;
    }
}