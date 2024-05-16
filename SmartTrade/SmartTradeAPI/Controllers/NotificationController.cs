using Microsoft.AspNetCore.Mvc;
using SmartTrade.BusinessLogic;
using SmartTradeDTOs;

namespace SmartTradeAPI.Controllers;

[ApiController]
[Route("SmartTradeAPI/Notification")]
public class NotificationController : ControllerBase
{
    private readonly ISmartTradeService _service = new SmartTradeService();

    [HttpGet("GetNotifications")]
    public List<NotificationDTO> GetNotifications()
    {
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
        return _service.GetNotifications(loggedId);
    }

    [HttpDelete("Delete")]
    public void Delete(int id)
    {
        _service.DeleteNotification(id);
    }

    [HttpPut("SetVisited")]
    public void SetVisited(int id)
    {
        _service.SetVisited(id);
    }
}