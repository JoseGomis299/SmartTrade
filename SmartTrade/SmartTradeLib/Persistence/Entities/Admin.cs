namespace SmartTrade.Entities;

public partial class Admin : User
{
    public virtual ICollection<Post> ValidatedPosts { get; set; }
    public virtual ICollection<Product> ValidatedProducts { get; set; }

}