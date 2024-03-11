using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTradeLib.Entities;

public abstract partial class Product
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Certification { get; set; }
    public string EcologicPrint { get; set; }
    public int MinimumAge { get; set; }
    public bool Validated { get; set; }
    public virtual ICollection<byte[]> Images { get; set; }
    public virtual ICollection<Product> Variants { get; set; }
    public virtual ICollection<Alert> Alerts { get; set; }
    public virtual ICollection<Post> Posts { get; set; }
}