using System.ComponentModel.DataAnnotations;

namespace SmartTradeLib.Entities;

public partial class User
{
    [Key]
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string LastNames { get; set; }
}