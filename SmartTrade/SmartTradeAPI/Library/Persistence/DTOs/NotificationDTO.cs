namespace SmartTradeDTOs;

public class NotificationDTO
{
    public int Id { get; set; }
    public string ConsumerId { get; set; }
    public bool Visited { get; set; }
    public SimplePostDTO Post { get; set; }
}