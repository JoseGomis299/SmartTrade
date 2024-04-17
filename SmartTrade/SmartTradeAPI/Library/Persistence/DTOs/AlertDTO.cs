using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTrade.Entities;

public class AlertDTO
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string ProductName { get; set; }

    public AlertDTO(Alert alert)
    {
        Id = alert.Id;
        UserId = alert.User.Email;
        ProductName = alert.Product.Name;
    }
}