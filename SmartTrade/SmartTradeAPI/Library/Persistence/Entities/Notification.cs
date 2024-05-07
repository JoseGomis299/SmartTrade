using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTrade.Entities;

public partial class Notification
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Message { get; set; }
    public bool Visited { get; set; }
    public virtual Consumer TargetUser { get; set; }
    public virtual Post TargetPost { get; set; }
}