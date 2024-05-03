using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTrade.Entities;

public partial class Gift
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string ListName { get; set; }
    public DateOnly Date { get; set; }
    public int Quantity { get; set; }
    public virtual Consumer User { get; set; }
    public virtual Post Post { get; set; }
    public virtual Offer Offer { get; set; }

}