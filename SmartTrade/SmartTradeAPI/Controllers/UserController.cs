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
        service.AddPaypal(info, id);
    }

    [HttpPost("AddCreditCard")]
    public void AddCreditCard(string id, [FromBody] CreditCardInfo info)
    {
        ISmartTradeService service = new SmartTradeService();
        service.AddCreditCard(info, id);
    }

    [HttpPost("AddBizum")]
    public void AddBizum(string id, [FromBody] BizumInfo info)
    {
        ISmartTradeService service = new SmartTradeService();
        service.AddBizum(info, id);
    }

    [HttpPost("AddPurchase")]
    public void AddPurchase([FromBody] PurchaseDTO purchase)
    {
        ISmartTradeService service = new SmartTradeService();
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;

        service.AddPurchase(loggedId, purchase);
    }

    [HttpPut("AddAddress")]
    public int AddAddress([FromBody] Address address)
    {
        ISmartTradeService service = new SmartTradeService();
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;

       return service.AddAddress(loggedId, address);
    }
    
    [HttpGet("GetPurchases")]
    public List<PurchaseDTO> GetPurchases()
    {
        ISmartTradeService service = new SmartTradeService();
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;

        return service.GetPurchases(loggedId);
    }

    [HttpGet("GetShoppingCart")]
    public List<CartItemDTO> GetShoppingCart()
    {
        ISmartTradeService service = new SmartTradeService();
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;

        return service.GetShoppingCart(loggedId);
    }

    [HttpPost("AddToShoppingCart")]
    public void AddToShoppingCart([FromBody] SimpleCartItemDTO item)
    {
        ISmartTradeService service = new SmartTradeService();
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;

        service.AddToCart(loggedId, item);
    }

    [HttpDelete("RemoveFromShoppingCart")]
    public void RemoveFromShoppingCart(int id)
    {
        ISmartTradeService service = new SmartTradeService();
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;

        service.RemoveFromCart(loggedId, id);
    }

    [HttpPost("AddGiftList")]
    public void AddGiftList([FromBody] SimpleGiftListDTO giftList)
    {
            ISmartTradeService service = new SmartTradeService();
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;

        service.AddGiftList(loggedId, giftList);
    }

    [HttpDelete("RemoveGiftList")]
    public void RemoveGiftList(string listName)
    {
        ISmartTradeService service = new SmartTradeService();
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;

        service.RemoveGiftList(loggedId, listName);
    }

    [HttpGet("GetGiftLists")]
    public List<GiftListDTO>? GetGiftLists()
    {
        ISmartTradeService service = new SmartTradeService();
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;

        return service.GetGiftLists(loggedId);
    }

    [HttpPut("AddGift")]
    public void AddGift([FromBody] SimpleGiftDTO gift)
    {
        ISmartTradeService service = new SmartTradeService();
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;

        service.AddGift(loggedId, gift);
    }

    [HttpPut("RemoveGift")]
    public void RemoveGift([FromBody] SimpleGiftDTO gift)
    {
        ISmartTradeService service = new SmartTradeService();
        string? loggedId = Request.Headers.FirstOrDefault(x => x.Key == "Logged").Value;

        service.RemoveGift(loggedId, gift);
    }
}
