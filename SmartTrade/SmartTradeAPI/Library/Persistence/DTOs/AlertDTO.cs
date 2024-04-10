using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTrade.Entities;

public class AlertDTO
{
    public int Id { get; set; }
    public virtual User User { get; set; }
    public virtual Product Product { get; set; }
}