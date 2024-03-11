using System.ComponentModel.DataAnnotations;

namespace SmartTradeLib.Entities;

public partial class BizumInfo : IPayMethod
{
    [Key]
    public string TelephonNumber { get; set; }
}