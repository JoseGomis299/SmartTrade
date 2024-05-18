using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTrade.Entities;

public partial class Rating
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public int Points { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public virtual Consumer User { get; set; }
    [Required]
    public virtual Post Post { get; set; }
}