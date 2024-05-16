using Microsoft.AspNetCore.Mvc;
using SmartTrade.BusinessLogic;
using SmartTrade.Entities;
using SmartTradeAPI.Library.Persistence.DTOs;
using SmartTradeDTOs;

namespace SmartTradeAPI.Controllers;

[ApiController]
[Route("SmartTradeAPI/User")]
public class UserController : ControllerBase
{
    private readonly ISmartTradeService _service = new SmartTradeService();

    [HttpPost("Login")]
    public dynamic Login([FromBody] LoginData login)
    {
        return _service.LogIn(login.Email, login.Password);
    }

    [HttpPost("RegisterSeller")]
    public SellerDTO RegisterSeller([FromBody] SellerRegisterData seller)
    {
        return _service.RegisterSeller(seller);
    }

    [HttpPost("RegisterConsumer")]
    public ConsumerDTO RegisterConsumer([FromBody] ConsumerRegisterData consumer)
    {
        return _service.RegisterConsumer(consumer);
    }


    [HttpPost("AddPaypal")]
    public void AddPaypal([FromBody] PayPalInfo info)
    {
        string id = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
        _service.AddPaypal(info, id);
    }

    [HttpPost("AddCreditCard")]
    public void AddCreditCard([FromBody] CreditCardInfo info)
    {
        string id = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
        _service.AddCreditCard(info, id);
    }

    [HttpPost("AddBizum")]
    public void AddBizum([FromBody] BizumInfo info)
    {
        string id = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
        _service.AddBizum(info, id);
    }

    [HttpPost("AddPurchase")]
    public void AddPurchase([FromBody] PurchaseDTO purchase)
    {
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
        _service.AddPurchase(loggedId, purchase);
    }

    [HttpPut("AddAddress")]
    public int AddAddress([FromBody] Address address)
    {
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
       return _service.AddAddress(loggedId, address);
    }
    
    [HttpGet("GetPurchases")]
    public List<PurchaseDTO> GetPurchases()
    {
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
        return _service.GetPurchases(loggedId);
    }

    [HttpGet("GetShoppingCart")]
    public List<CartItemDTO> GetShoppingCart()
    {
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
        return _service.GetShoppingCart(loggedId);
    }

    [HttpPost("AddToShoppingCart")]
    public void AddToShoppingCart([FromBody] SimpleCartItemDTO item)
    {
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
        _service.AddToCart(loggedId, item);
    }

    [HttpDelete("RemoveFromShoppingCart")]
    public void RemoveFromShoppingCart(int id)
    {
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
        _service.RemoveFromCart(loggedId, id);
    }

    [HttpPost("AddGiftList")]
    public void AddGiftList([FromBody] SimpleGiftListDTO giftList)
    {
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
        _service.AddGiftList(loggedId, giftList);
    }

    [HttpDelete("RemoveGiftList")]
    public void RemoveGiftList(string listName)
    {
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
        _service.RemoveGiftList(loggedId, listName);
    }

    [HttpGet("GetGiftLists")]
    public List<GiftListDTO>? GetGiftLists()
    {
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
        return _service.GetGiftLists(loggedId);
    }

    [HttpPut("AddGift")]
    public void AddGift([FromBody] SimpleGiftDTO gift)
    {
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
        _service.AddGift(loggedId, gift);
    }

    [HttpPut("RemoveGift")]
    public void RemoveGift([FromBody] SimpleGiftDTO gift)
    {
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;
        _service.RemoveGift(loggedId, gift);
    }
}
