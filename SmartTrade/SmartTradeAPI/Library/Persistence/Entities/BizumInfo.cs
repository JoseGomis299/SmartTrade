using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTradeLib.Entities;

public partial class BizumInfo : IPayMethod
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string TelephonNumber { get; set; }
}