namespace SmartTrade.Entities;

public partial class Seller : User
{
    public string DNI { get; set; }
    public string CompanyName { get; set; }
    public string IBAN { get; set; }
    public virtual ICollection<Post> Posts { get; set; }
}