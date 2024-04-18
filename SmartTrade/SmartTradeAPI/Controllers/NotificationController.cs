using Microsoft.AspNetCore.Mvc;
using SmartTrade.BusinessLogic;
using SmartTradeDTOs;

namespace SmartTradeAPI.Controllers;

[ApiController]
[Route("SmartTradeAPI/Notification")]
public class NotificationController : ControllerBase
{
    [HttpGet("GetNotifications")]
    public List<NotificationDTO> GetNotifications()
    {
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
        ISmartTradeService service = new SmartTradeService();
        return service.GetNotifications(loggedId);
    }

    [HttpDelete("Delete")]
    public void Delete(int id)
    {
        ISmartTradeService service = new SmartTradeService();
        service.DeleteNotification(id);
    }

    [HttpPut("SetVisited")]
    public void SetVisited(int id)
    {
        ISmartTradeService service = new SmartTradeService();
        service.SetVisited(id);
    }
}