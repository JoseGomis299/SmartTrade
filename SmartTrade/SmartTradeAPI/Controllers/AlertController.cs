using Microsoft.AspNetCore.Mvc;
using SmartTrade.BusinessLogic;
using SmartTrade.Entities;
using SmartTradeDTOs;

namespace SmartTradeAPI.Controllers;

[ApiController]
[Route("SmartTradeAPI/Alert")]
public class AlertController : ControllerBase
{
    [HttpPost("CreateAlert")]
    public int CreateAlert(int id)
    {
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
        ISmartTradeService service = new SmartTradeService();
        return service.CreateAlert(loggedId, id);
    }

    [HttpDelete("DeletAlert")]
    public void DeleteAlert(int id)
    {
        SmartTradeService service = new();
        service.DeleteAlert(id);
    }
}