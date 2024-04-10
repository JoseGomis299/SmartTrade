namespace SmartTrade.Entities;

public partial class Seller : User
{
    public Seller()
    {
        Posts = new List<Post>();
    }
    public Seller(string email, string password, string name, string lastNames, string dni, string companyName, string iban) : base(email, password, name, lastNames)
    {
        DNI = dni;
        CompanyName = companyName;
        IBAN = iban;

        Posts = new List<Post>();
    }

    public void AddPost(Post post)
    {
        Posts.Add(post);
    }
}