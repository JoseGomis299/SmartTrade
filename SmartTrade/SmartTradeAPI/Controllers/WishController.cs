using Microsoft.AspNetCore.Mvc;
using SmartTrade.BusinessLogic;
using SmartTrade.Entities;

namespace SmartTradeAPI.Controllers;

[ApiController]
[Route("SmartTradeAPI/Wish")]
public class WishController : ControllerBase
{
    [HttpPost("CreateWish")]
    public int CreateWish(int id)
    {
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
        ISmartTradeService service = new SmartTradeService();
        return service.CreateWish(loggedId, id);
    }

    [HttpDelete("DeletWish")]
    public void DeleteWish(int id)
    {
        SmartTradeService service = new();
        service.DeleteWish(id);
    }

    [HttpGet("GetWishList")]
    public List<WishDTO> GetWishList()
    {
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
        ISmartTradeService service = new SmartTradeService();
        return service.GetWishList(loggedId);
    }
}