using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTrade.Entities;

public abstract partial class Product
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Certification { get; set; }
    public string? EcologicPrint { get; set; }
    public string? HowToReducePrint { get; set; }
    public int MinimumAge { get; set; }
    public string HowToUse { get; set; }
    public virtual ICollection<Image> Images { get; set; }
    public virtual ICollection<Post> Posts { get; set; }
    public virtual ICollection<Alert> Alerts { get; set; }
}