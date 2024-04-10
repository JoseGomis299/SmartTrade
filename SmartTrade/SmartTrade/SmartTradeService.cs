using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartTrade.Entities;
using SmartTradeDTOs;

namespace SmartTrade;

public class SmartTradeService
{
    private static SmartTradeService? _instance;
    public static SmartTradeService Instance => _instance ??= new SmartTradeService();

    public UserDTO? Logged { get; private set; }

    public async Task LogInAsync(string email, string password)
    {
        string json = JsonConvert.SerializeObject(new { Email = email, Password = password });
        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        Logged = JsonConvert.DeserializeObject<UserDTO>(await PerformApiInstructionAsync("User/Login", ApiInstruction.Post, content));
    }

    public async Task<string> RegisterConsumerAsync(string email, string password, string name, string lastnames, string dni, DateTime dateBirth, Address billingAddress, Address consumerAddress)
    {
        string json = JsonConvert.SerializeObject(new ConsumerRegisterData()
        { 
            Email = email, Password = password, 
            Address = consumerAddress, BillingAddress = billingAddress, 
            BirthDate = dateBirth, DNI = dni, LastNames = lastnames, Name = name

        });
        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        return await PerformApiInstructionAsync("User/RegisterConsumer", ApiInstruction.Post, content);
    }

    public async Task<string> RegisterSellerAsync(string email, string password, string name, string lastnames,
        string dni, string companyName, string iban)
    {
        string json = JsonConvert.SerializeObject(new SellerRegisterData()
        {
            Email = email, Password = password, 
            CompanyName = companyName, IBAN = iban,
             DNI = dni, LastNames = lastnames, Name = name

        });
        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        return await PerformApiInstructionAsync("User/RegisterSeller", ApiInstruction.Post, content);
    }

    public async Task AddPostAsync(PostDTO post)
    {
        string json = JsonConvert.SerializeObject(post);
        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        await PerformApiInstructionAsync("User/PublishPost", ApiInstruction.Post, content);
    }

    public async Task<string> GetPostsAsync()
    {
        return await PerformApiInstructionAsync("Post/GetAll", ApiInstruction.Get);
    }

    public async Task<string> GetPostsNamesAsync()
    {
        return await PerformApiInstructionAsync("Post/GetAllNames", ApiInstruction.Get);
    }

    public async Task<string> GetPostsFuzzyContainAsync(string? searchText)
    {
        return await PerformApiInstructionAsync("Post/GetContaining?content=" + Uri.EscapeDataString(searchText), ApiInstruction.Get);
    }

    public async Task EditPostAsync(int postId, string postInfoJson)
    {
        using var content = new StringContent(postInfoJson, Encoding.UTF8, "application/json");

        await PerformApiInstructionAsync($"User/EditPost/{postId}", ApiInstruction.Put, content);
    }

    public async Task DeletePostAsync(int postId)
    {
        await PerformApiInstructionAsync($"User/RemovePost/{postId}", ApiInstruction.Delete);
    }

    private async Task<string> PerformApiInstructionAsync(string function, ApiInstruction instruction, HttpContent content = null)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://smarttradeapi00.azurewebsites.net/");
        client.DefaultRequestHeaders.Add("Logged", Logged?.Email);

        try
        {
            HttpResponseMessage response = null;

            switch (instruction)
            {
                case ApiInstruction.Get:
                    response = await client.GetAsync("SmartTradeAPI/" + function);
                    break;
                case ApiInstruction.Post:
                    if (content == null) throw new ArgumentNullException(nameof(content));
                    response = await client.PostAsync("SmartTradeAPI/" + function, content);
                    break;
                case ApiInstruction.Put:
                    if (content == null) throw new ArgumentNullException(nameof(content));
                    response = await client.PutAsync("SmartTradeAPI/" + function, content);
                    break;
                case ApiInstruction.Delete:
                    response = await client.DeleteAsync("SmartTradeAPI/" + function);
                    break;
            }

            if (response == null || !response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al conectar a la API. Código de estado: {response?.StatusCode}");
            }

            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al conectar a la API: {e.Message}");
            return string.Empty;
        }
    }
}

public enum ApiInstruction
{
    Get,
    Post,
    Put,
    Delete
}
