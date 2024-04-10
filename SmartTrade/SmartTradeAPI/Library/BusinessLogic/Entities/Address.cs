namespace SmartTrade.Entities;

public partial class Address
{
    public Address() { }
    public Address(string province, string street, string city, string postalCode, string number, string door)
    {
        Province = province;
        Street = street;
        City = city;
        PostalCode = postalCode;
        Number = number;
        Door = door;
    }
}