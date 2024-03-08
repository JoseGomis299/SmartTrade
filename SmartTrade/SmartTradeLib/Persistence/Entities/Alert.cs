using System.ComponentModel.DataAnnotations;

namespace SmartTradeLib.Entities;

public partial class Alert
{
    [Key]
    public virtual User User { get; set; }
    [Key]
    public virtual Product Product { get; set; }
}