using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTrade.Entities;

public partial class GiftList
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateOnly? Date { get; set; }
    public virtual string ConsumerEmail { get; set; }
    public virtual ICollection<Gift> Gifts { get; set; }

}