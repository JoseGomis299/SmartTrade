namespace SmartTrade.Entities;

public class Address
{
    public int Id { get; set; }
    public string Province { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Number { get; set; }
    public string Door { get; set; }

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