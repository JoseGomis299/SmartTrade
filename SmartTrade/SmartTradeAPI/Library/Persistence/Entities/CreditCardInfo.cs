using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmartTradeLib.Entities;

public partial class CreditCardInfo : IPayMethod
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string CardNumber { get; set; }
    public string ExpirationDate { get; set; }
    public string CVV { get; set; }
    public string CardHolder { get; set; }
}