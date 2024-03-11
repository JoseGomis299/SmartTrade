namespace SmartTradeLib.Entities;

public partial class Post
{
    public Post(string title, string description, bool validated, Seller seller, Offer offer)
    {
        Title = title;
        Description = description;
        Validated = validated;
        Seller = seller;

        Offers = new List<Offer>();
        Offers.Add(offer);
    }
}