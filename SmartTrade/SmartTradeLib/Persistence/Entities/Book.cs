namespace SmartTradeLib.Entities;

public partial class Book : Product
{
    public string Author { get; set; }
    public string Publisher { get; set; }
    public int Pages { get; set; }
    public string Language { get; set; }
    public string ISBN { get; set; }
}