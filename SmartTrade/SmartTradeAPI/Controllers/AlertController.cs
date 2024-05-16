using Microsoft.AspNetCore.Mvc;
using SmartTrade.BusinessLogic;
using SmartTrade.Entities;
using SmartTradeDTOs;

namespace SmartTradeAPI.Controllers;

[ApiController]
[Route("SmartTradeAPI/Alert")]
public class AlertController : ControllerBase
{
    private readonly ISmartTradeService _service = new SmartTradeService();

    [HttpPost("CreateAlert")]
    public int CreateAlert(string productName)
    {
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
        return _service.CreateAlert(loggedId, productName);
    }

    [HttpDelete("DeleteAlert")]
    public void DeleteAlert(int id)
    {
        _service.DeleteAlert(id);
    }

    [HttpDelete("DeleteAlertByProductName")]
    public void DeleteAlert(string productName)
    {
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
        _service.DeleteAlert(productName, loggedId);
    }

    [HttpGet("GetAlertByProductName")]
    public AlertDTO GetAlertByProductName(string productName)
    {
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
        return _service.GetAlert(productName, loggedId);
    }

    [HttpGet("GetUserAlerts")]
    public List<AlertDTO> GetUserAlerts()
    {
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
        return _service.GetAlerts(loggedId);
    }
}