using System.ComponentModel.DataAnnotations;

namespace SmartTradeLib.Entities;

public partial class Post
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public bool Validated { get; set; }
    public virtual Seller Seller { get; set; }
    public virtual ICollection<Offer> Offers { get; set; }
}