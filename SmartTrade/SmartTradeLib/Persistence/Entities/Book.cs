namespace SmartTrade.Entities;

public partial class Book : Product
{
    public string Author { get; set; }
    public string Publisher { get; set; }
    public string Pages { get; set; }
    public string Language { get; set; }
    public string ISBN { get; set; }
}