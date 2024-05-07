namespace SmartTradeDTOs;

public class AlertDTO
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string ProductName { get; set; }

    public AlertDTO(string productName, string loggedEmail, int id)
    {
        ProductName = productName;
        UserId = loggedEmail;
        Id = id;
    }
}