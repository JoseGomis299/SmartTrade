using System.ComponentModel.DataAnnotations;

namespace SmartTradeLib.Entities;

public class Adress
{
    [Key]
    public int Id { get; set; }
    public string Province { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Door { get; set; }
}