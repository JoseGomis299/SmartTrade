using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTrade.Entities;

public abstract partial class User
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string LastNames { get; set; }
    public virtual ICollection<Alert> Alerts { get; set; }
    public virtual ICollection<Notification> Notifications { get; set; }
    public virtual ICollection<Wish> WishList { get; set; }
}