using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTradeLib.Entities;

public partial class Post
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    public string Description { get; set; }
    public bool Validated { get; set; }
    [Required]
    public virtual Seller? Seller { get; set; }
    public virtual ICollection<Offer> Offers { get; set; }
}