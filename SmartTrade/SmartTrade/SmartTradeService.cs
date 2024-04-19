using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
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

    private List<SimplePostDTO>? _posts;
    public IEnumerable<SimplePostDTO>? Posts => _posts;
    public UserDTO? Logged { get; private set; }
    public List<NotificationDTO>? Notifications { get; private set; }

    private async Task SetLogged(string json)
    {
        Logged = JsonConvert.DeserializeObject<UserDTO>(json);

        if (Logged == null) return;

        if (Logged.IsConsumer)
        {
            Logged = JsonConvert.DeserializeObject<ConsumerDTO>(json);
        }
        else if (Logged.IsSeller)
        {
            Logged = JsonConvert.DeserializeObject<SellerDTO>(json);
        }

        await GetNotificationsAsync();
    }
    public void LogOut()
    {
        Logged = null;
    }

    public async Task LogInAsync(string email, string password)
    {
        string json = JsonConvert.SerializeObject(new { Email = email, Password = password });
        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        await SetLogged(await PerformApiInstructionAsync("User/LogIn", ApiInstruction.Post, content));
    }

    public async Task RegisterConsumerAsync(string email, string password, string name, string lastnames, string dni, DateTime dateBirth, Address billingAddress, Address consumerAddress)
    {
        string json = JsonConvert.SerializeObject(new ConsumerRegisterData()
        { 
            Email = email, Password = password, 
            Address = consumerAddress, BillingAddress = billingAddress, 
            BirthDate = dateBirth, DNI = dni, LastNames = lastnames, Name = name

        });
        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        await SetLogged(await PerformApiInstructionAsync("User/RegisterConsumer", ApiInstruction.Post, content));
    }

    public async Task RegisterSellerAsync(string email, string password, string name, string lastnames, string dni, string companyName, string iban)
    {
        string json = JsonConvert.SerializeObject(new SellerRegisterData()
        {
            Email = email, Password = password, 
            CompanyName = companyName, IBAN = iban,
             DNI = dni, LastNames = lastnames, Name = name

        });
        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        await SetLogged(await PerformApiInstructionAsync("User/RegisterSeller", ApiInstruction.Post, content));
    }

    public async Task AddPostAsync(PostDTO post)
    {
        string json = JsonConvert.SerializeObject(post);
        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        await PerformApiInstructionAsync("Post/PublishPost", ApiInstruction.Post, content);
    }

    public async Task<List<SimplePostDTO>?> GetPostsAsync()
    {
        _posts = JsonConvert.DeserializeObject<List<SimplePostDTO>>(await PerformApiInstructionAsync("Post/GetAll", ApiInstruction.Get));
        return _posts;
    }

    public async Task<PostDTO?> GetPostAsync(int postId)
    {
       return JsonConvert.DeserializeObject<PostDTO>(await PerformApiInstructionAsync($"Post/GetById?id={postId}", ApiInstruction.Get));
    }

    public async Task<List<string>?> GetPostsNamesAsync()
    {
        return JsonConvert.DeserializeObject<List<string>>(await PerformApiInstructionAsync("Post/GetAllNames", ApiInstruction.Get));
    }

    public async Task<List<SimplePostDTO>?> GetPostsFuzzyContainAsync(string? searchText)
    {
        return JsonConvert.DeserializeObject<List<SimplePostDTO>>(await PerformApiInstructionAsync("Post/GetContaining?content=" + Uri.EscapeDataString(searchText), ApiInstruction.Get));
    }

    public async Task EditPostAsync(int postId, PostDTO postInfo)
    {
        string postInfoJson = JsonConvert.SerializeObject(postInfo);
        using var content = new StringContent(postInfoJson, Encoding.UTF8, "application/json");

        await PerformApiInstructionAsync($"Post/EditPost?id={postId}", ApiInstruction.Put, content);
    }

    public async Task AddPaypalAsync(PayPalInfo paypalinfo, String loggedID)
    {
        string json = JsonConvert.SerializeObject(paypalinfo);
        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        await PerformApiInstructionAsync($"User/AddPaypal?id={loggedID}", ApiInstruction.Post, content);

    }

    public async Task DeletePostAsync(int postId)
    {
        await PerformApiInstructionAsync($"Post/RemovePost?id={postId}", ApiInstruction.Delete);
    }

    public async Task AddCreditCardAsync(CreditCardInfo creditCard)
    {
        string json = JsonConvert.SerializeObject(creditCard);
        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        await PerformApiInstructionAsync($"User/AddCreditCard?id={Logged.Email}", ApiInstruction.Post, content);
    }

    public async Task AddBizumAsync(BizumInfo bizum)
    {
        string json = JsonConvert.SerializeObject(bizum);
        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        await PerformApiInstructionAsync($"User/AddBizum?id={Logged.Email}", ApiInstruction.Post, content);
    }

    public async Task<List<NotificationDTO>?> GetNotificationsAsync()
    {
        Notifications = JsonConvert.DeserializeObject<List<NotificationDTO>>(await PerformApiInstructionAsync($"Notification/GetNotifications", ApiInstruction.Get));
        return Notifications;
    }

    public async Task<int> CreateAlertAsync(int productId)
    {
        string json = JsonConvert.SerializeObject(productId);
        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        return int.Parse(await PerformApiInstructionAsync($"Alert/CreateAlert?id={productId}", ApiInstruction.Post, content));
    }

    public async Task DeleteAlertAsync(int alertId)
    {
        await PerformApiInstructionAsync($"Alert/Delete?id={alertId}", ApiInstruction.Delete);
    }

    public async Task<AlertDTO> GetAlertsAsync(string productName)
    {
        return JsonConvert.DeserializeObject<AlertDTO>(await PerformApiInstructionAsync($"Alert/GetAlert?productName={productName}", ApiInstruction.Get));
    }

    public async Task DeleteNotificationAsync(int notificationId)
    {
        Notifications.Remove(Notifications.First(n => n.Id == notificationId));
        await PerformApiInstructionAsync($"Notification/DeleteNotification?id={notificationId}", ApiInstruction.Delete);
    }

    public async Task SetNotificationAsVisited(int notificationId)
    {
        Notifications.First(n => n.Id == notificationId).Visited = true;
        await PerformApiInstructionAsync($"Notification/SetAsVisited?id={notificationId}", ApiInstruction.Put);
    }

    private async Task<string> PerformApiInstructionAsync(string function, ApiInstruction instruction, HttpContent content = null)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://smarttradeapi2.azurewebsites.net/");
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
