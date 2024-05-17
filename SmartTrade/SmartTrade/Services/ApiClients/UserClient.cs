using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartTradeDTOs;
using SmartTrade.Entities;

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

    public async Task AddPaypalAsync(PayPalInfo paypalinfo)
    {
        if(Logged == null) return;
        await PerformApiInstructionAsync($"AddPaypal", ApiInstruction.Post, paypalinfo);
    }

    public async Task AddCreditCardAsync(CreditCardInfo creditCard)
    {
        if (Logged == null) return;
        await PerformApiInstructionAsync($"AddCreditCard", ApiInstruction.Post, creditCard);
    }

    public async Task AddBizumAsync(BizumInfo bizum)
    {
        if (Logged == null) return;
        await PerformApiInstructionAsync($"AddBizum", ApiInstruction.Post, bizum);
    }

    public async Task AddPurchaseAsync(PurchaseDTO purchase)
    {
        if (Logged == null) return;
        await PerformApiInstructionAsync("AddPurchase", ApiInstruction.Post, purchase);
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

    public async Task AddGiftListAsync(SimpleGiftListDTO giftList)
    {
        if (Logged == null) return;
        await PerformApiInstructionAsync($"AddGiftList", ApiInstruction.Post, giftList);
    }

    public async Task RemoveGiftListAsync(string listName)
    {
        if (Logged == null) return;
        await PerformApiInstructionAsync($"RemoveGiftList?listName={listName}", ApiInstruction.Delete);
    }

    public async Task<List<GiftListDTO>?> GetGiftListsAsync()
    {
        if (Logged == null) return null;
        return JsonConvert.DeserializeObject<List<GiftListDTO>>(await PerformApiInstructionAsync($"GetGiftLists", ApiInstruction.Get));
    }

    public async Task AddGiftAsync(SimpleGiftDTO giftList)
    {
        if (Logged == null) return;
        await PerformApiInstructionAsync($"AddGift", ApiInstruction.Put, giftList);
    }

    public async Task RemoveGiftAsync(SimpleGiftDTO giftList)
    {
        if (Logged == null) return;
        await PerformApiInstructionAsync($"RemoveGift", ApiInstruction.Put, giftList);
    }

    public async Task<int> AddAddressAsync(Address address)
    {
        if (Logged == null) return -1;
        return int.Parse(await PerformApiInstructionAsync($"AddAddress", ApiInstruction.Put, address));
    }
}