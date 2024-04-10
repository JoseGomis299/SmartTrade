using Microsoft.AspNetCore.Mvc;
using SmartTrade.BusinessLogic;
using SmartTradeDTOs;

namespace SmartTradeAPI.Controllers;

[ApiController]
[Route("SmartTradeAPI/User")]
public class UserController : ControllerBase
{
    [HttpPost("Login")]
    public UserDTO Login([FromBody] LoginData login)
    {
        ISmartTradeService service = new SmartTradeService();
        return service.LogIn(login.Email, login.Password);
    }

    [HttpPost("RegisterSeller")]
    public void Post([FromBody] SellerRegisterData seller)
    {
        ISmartTradeService service = new SmartTradeService();
        service.RegisterSeller(seller);
    }

    [HttpPost("RegisterConsumer")]
    public void Post([FromBody] ConsumerRegisterData consumer)
    {
        ISmartTradeService service = new SmartTradeService();
        service.RegisterConsumer(consumer);
    }
}