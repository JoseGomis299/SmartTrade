using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTrade.Entities;

public partial class Gift
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int Quantity { get; set; }
    public bool Notified { get; set; }

    public virtual Post? Post { get; set; }
    public virtual Offer? Offer { get; set; }
}