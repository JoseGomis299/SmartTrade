using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartTrade.Entities;
using SmartTradeDTOs;

namespace SmartTrade.Services.ApiClients;

public class UserApiClient : ApiClient
{
    public UserDTO? Logged => Broker.Logged;

    public UserApiClient(SmartTradeBroker broker) : base(broker, "User") { }


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
        await PerformApiInstructionAsync($"AddPaypal?id={loggedID}", ApiInstruction.Post, paypalinfo);

    }

    public async Task AddCreditCardAsync(CreditCardInfo creditCard)
    {
        await PerformApiInstructionAsync($"AddCreditCard?id={Logged.Email}", ApiInstruction.Post, creditCard);
    }

    public async Task AddBizumAsync(BizumInfo bizum)
    {
        await PerformApiInstructionAsync($"AddBizum?id={Logged.Email}", ApiInstruction.Post, bizum);
    }

}