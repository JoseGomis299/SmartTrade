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
    public int CreateAlert(string productName)
    {
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
        ISmartTradeService service = new SmartTradeService();
        return service.CreateAlert(loggedId, productName);
    }

    [HttpDelete("DeleteAlert")]
    public void DeleteAlert(int id)
    {
        SmartTradeService service = new();
        service.DeleteAlert(id);
    }

    [HttpDelete("DeleteAlertByProductName")]
    public void DeleteAlert(string productName)
    {
        ISmartTradeService service = new SmartTradeService();
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;

        service.DeleteAlert(productName, loggedId);
    }

    [HttpGet("GetAlertByProductName")]
    public AlertDTO GetAlertByProductName(string productName)
    {
        ISmartTradeService service = new SmartTradeService();
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;

        return service.GetAlert(productName, loggedId);
    }

    [HttpGet("GetUserAlerts")]
    public List<AlertDTO> GetUserAlerts()
    {
        ISmartTradeService service = new SmartTradeService();
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
        return service.GetAlerts(loggedId);
    }
}