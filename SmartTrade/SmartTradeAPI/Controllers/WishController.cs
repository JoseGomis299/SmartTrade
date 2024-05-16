using Microsoft.AspNetCore.Mvc;
using SmartTrade.BusinessLogic;
using SmartTrade.Entities;

namespace SmartTradeAPI.Controllers;

[ApiController]
[Route("SmartTradeAPI/Wish")]
public class WishController : ControllerBase
{
    private readonly ISmartTradeService _service = new SmartTradeService();

    [HttpPost("CreateWish")]
    public int CreateWish(int id)
    {
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
        return _service.CreateWish(loggedId, id);
    }

    [HttpDelete("DeleteWish")]
    public void DeleteWish(int id)
    {
        _service.DeleteWish(id);
    }

    [HttpGet("GetWishList")]
    public List<WishDTO> GetWishList()
    {
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
        return _service.GetWishList(loggedId);
    }
}