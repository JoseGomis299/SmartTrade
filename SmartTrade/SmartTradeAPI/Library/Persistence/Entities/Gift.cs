using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTrade.Entities;

public partial class Gift
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string ListName { get; set; }

    [Required]
    public virtual Consumer User { get; set; }
    [Required]
    public virtual Post Post { get; set; }
    [Required]
    public virtual Offer Offer { get; set; }

}