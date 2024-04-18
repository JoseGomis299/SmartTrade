using Microsoft.AspNetCore.Mvc;
using SmartTrade.BusinessLogic;
using SmartTrade.Entities;
using SmartTradeDTOs;

namespace SmartTradeAPI.Controllers;

[ApiController]
[Route("SmartTradeAPI/User")]
public class UserController : ControllerBase
{
    [HttpPost("Login")]
    public dynamic Login([FromBody] LoginData login)
    {
        ISmartTradeService service = new SmartTradeService();
        return service.LogIn(login.Email, login.Password);
    }

    [HttpPost("RegisterSeller")]
    public SellerDTO RegisterSeller([FromBody] SellerRegisterData seller)
    {
        ISmartTradeService service = new SmartTradeService();
        return service.RegisterSeller(seller);
    }

    [HttpPost("RegisterConsumer")]
    public ConsumerDTO RegisterConsumer([FromBody] ConsumerRegisterData consumer)
    {
        ISmartTradeService service = new SmartTradeService();
        return service.RegisterConsumer(consumer);
    }


    [HttpPost("AddPaypal")]
    public void AddPaypal(string id, [FromBody] PayPalInfo info)
    {
        ISmartTradeService service = new SmartTradeService();
        service.AddPaypal(info,id);
    }

    [HttpPost("AddCreditCard")]
    public void AddCreditCard(string id, [FromBody] CreditCardInfo info)
    {
        ISmartTradeService service = new SmartTradeService();
        service.AddCreditCard(info, id);
    }

    [HttpPost("AddBizum")]
    public void Add(string id, [FromBody] BizumInfo info)
    {
        ISmartTradeService service = new SmartTradeService();
        service.AddBizum(info, id);
    }

    [HttpGet("GetNotifications")]
    public List<NotificationDTO> GetNotifications()
    {
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
        ISmartTradeService service = new SmartTradeService();
        return service.GetNotifications(loggedId);
    }
}
