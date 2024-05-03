using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartTradeDTOs;
using SmartTrade.Entities;
using HarfBuzzSharp;

namespace SmartTrade.Services.ApiClients;

public class UserClient : ApiClient
{
    public UserDTO? Logged => Broker.Logged;

    public UserClient(SmartTradeBroker broker) : base(broker, "User") { }


    public async Task LogInAsync(string email, string password)
    {
        await Broker.SetLoggedAsync(await PerformApiInstructionAsync("Login", ApiInstruction.Post, new { Email = email, Password = password }));
    }

    public async Task RegisterConsumerAsync(string email, string password, string name, string lastnames, string dni, DateTime dateBirth, Address billingAddress, Address consumerAddress)
    {
        var registerData = new ConsumerRegisterData()
        {
            Email = email,
            Password = password,
            Address = consumerAddress,
            BillingAddress = billingAddress,
            BirthDate = dateBirth,
            DNI = dni,
            LastNames = lastnames,
            Name = name

        };

        await Broker.SetLoggedAsync(await PerformApiInstructionAsync("RegisterConsumer", ApiInstruction.Post, registerData));
    }

    public async Task RegisterSellerAsync(string email, string password, string name, string lastnames, string dni, string companyName, string iban)
    {
        var registerData = new SellerRegisterData()
        {
            Email = email,
            Password = password,
            CompanyName = companyName,
            IBAN = iban,
            DNI = dni,
            LastNames = lastnames,
            Name = name

        };

        await Broker.SetLoggedAsync(await PerformApiInstructionAsync("RegisterSeller", ApiInstruction.Post, registerData));
    }

    public async Task AddPaypalAsync(PayPalInfo paypalinfo, string loggedID)
    {
        if(Logged == null) return;
        await PerformApiInstructionAsync($"AddPaypal?id={loggedID}", ApiInstruction.Post, paypalinfo);
    }

    public async Task AddCreditCardAsync(CreditCardInfo creditCard)
    {
        if (Logged == null) return;
        await PerformApiInstructionAsync($"AddCreditCard?id={Logged.Email}", ApiInstruction.Post, creditCard);
    }

    public async Task AddBizumAsync(BizumInfo bizum)
    {
        if (Logged == null) return;
        await PerformApiInstructionAsync($"AddBizum?id={Logged.Email}", ApiInstruction.Post, bizum);
    }

    public async Task AddPurchaseAsync(int idProduct, int idPost, string emailSeller, int precio, int precioEnvio, int idoffer)
    {
        if (Logged == null) return;

        var registerData = new PurchaseDTO()
        {
            ProductId = idProduct,
            PostId = idPost,
            EmailSeller = emailSeller,
            Price = precio,
            ShippingPrice = precioEnvio,
            OfferId = idoffer

        };

        await PerformApiInstructionAsync("AddPurchase", ApiInstruction.Post, registerData);
    }

    public async Task<List<PurchaseDTO>?> GetPurchaseAsync()
    {
        if (Logged == null) return null;
        return JsonConvert.DeserializeObject<List<PurchaseDTO>>(await PerformApiInstructionAsync($"GetPurchases?id={Logged.Email}", ApiInstruction.Get));
    }

    public async Task<List<CartItemDTO>?> GetShoppingCartAsync()
    {
        if (Logged == null) return null;
        return JsonConvert.DeserializeObject<List<CartItemDTO>>(await PerformApiInstructionAsync($"GetShoppingCart", ApiInstruction.Get));
    }

    public async Task AddToCartAsync(SimpleCartItemDTO cartItem)
    {
        if (Logged == null) return;
        await PerformApiInstructionAsync($"AddToShoppingCart", ApiInstruction.Post, cartItem);
    }

    public async Task RemoveFromCartAsync(int offerId)
    {
        if (Logged == null) return;
        await PerformApiInstructionAsync($"RemoveFromShoppingCart?id={offerId}", ApiInstruction.Delete);
    }

    public async Task AddGiftAsync(string ListName, DateOnly? Date, int? Quantity, string? ConsumerEmail, int? PostId, int? OfferId)
    {
        if (Logged == null) return;

        var registerData = new SimpleGiftDTO()
        {
            ListName = ListName,
            Date = Date,
            Quantity = Quantity,
            ConsumerEmail = ConsumerEmail,
            PostId = PostId,
            OfferId = OfferId
        };

        await PerformApiInstructionAsync("AddGift", ApiInstruction.Post, registerData);
    }
}