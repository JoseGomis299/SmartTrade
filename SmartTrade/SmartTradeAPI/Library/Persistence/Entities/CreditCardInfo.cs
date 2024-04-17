using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmartTrade.Entities;

public partial class CreditCardInfo : IPayMethod
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string CardNumber { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string CVV { get; set; }
    public string CardHolder { get; set; }
}