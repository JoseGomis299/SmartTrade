using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartTrade.Entities;
using SmartTrade.Services.ApiClients;
using SmartTradeDTOs;

namespace SmartTrade.Services;

public class SmartTradeBroker
{
    private UserApiClient _userApiClient;
    private PostApiClient _postApiClient;
    private NotificationApiClient _notificationApiClient;
    private AlertApiClient _alertApiClient;

    public UserDTO? Logged { get; set; }

    public SmartTradeBroker()
    {
        _userApiClient = new UserApiClient(this);
        _postApiClient = new PostApiClient(this);
        _notificationApiClient = new NotificationApiClient(this);
        _alertApiClient = new AlertApiClient(this);
    }

    public void LogOut()
    {
        Logged = null;
    }

    public async Task SetLoggedAsync(string json)
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

    #region User Operations

    public async Task LogInAsync(string email, string password)
    {
       await _userApiClient.LogInAsync(email, password);
    }

    public async Task RegisterConsumerAsync(string email, string password, string name, string lastnames, string dni, DateTime dateBirth, Address billingAddress, Address consumerAddress)
    {
       await _userApiClient.RegisterConsumerAsync(email, password, name, lastnames, dni, dateBirth, billingAddress, consumerAddress);
    }

    public async Task RegisterSellerAsync(string email, string password, string name, string lastnames, string dni, string companyName, string iban)
    {
       await _userApiClient.RegisterSellerAsync(email, password, name, lastnames, dni, companyName, iban);
    }

    public async Task AddPaypalAsync(PayPalInfo paypalinfo, string loggedID)
    {
        await _userApiClient.AddPaypalAsync(paypalinfo, loggedID);
    }

    public async Task AddCreditCardAsync(CreditCardInfo creditCard)
    {
        await _userApiClient.AddCreditCardAsync(creditCard);
    }

    public async Task AddBizumAsync(BizumInfo bizum)
    {
        await _userApiClient.AddBizumAsync(bizum);
    }

    #endregion

    #region Post Operations

    public async Task AddPostAsync(PostDTO post)
    {
        await _postApiClient.AddPostAsync(post);
    }

    public async Task<List<SimplePostDTO>?> GetPostsAsync()
    {
        return await _postApiClient.GetPostsAsync();
    }

    public async Task<PostDTO?> GetPostAsync(int postId)
    {
        return await _postApiClient.GetPostAsync(postId);
    }

    public async Task EditPostAsync(int postId, PostDTO postInfo)
    {
        await _postApiClient.EditPostAsync(postId, postInfo);
    }

    public async Task DeletePostAsync(int postId)
    {
        await _postApiClient.DeletePostAsync(postId);
    }

    #endregion

    #region Notification Operations

    public async Task<List<NotificationDTO>?> GetNotificationsAsync()
    {
        return await _notificationApiClient.GetNotificationsAsync();
    }

    public async Task DeleteNotificationAsync(int notificationId)
    {
        await _notificationApiClient.DeleteNotificationAsync(notificationId);
    }

    public async Task SetNotificationAsVisitedAsync(int notificationId)
    {
        await _notificationApiClient.SetNotificationAsVisitedAsync(notificationId);
    }

    #endregion

    #region Alert Operations

    public async Task<int> CreateAlertAsync(int productId)
    {
        return await _alertApiClient.CreateAlertAsync(productId);
    }

    public async Task DeleteAlertAsync(int alertId)
    {
        await _alertApiClient.DeleteAlertAsync(alertId);
    }

    public async Task<AlertDTO?> GetAlertsAsync(string productName)
    {
        return await _alertApiClient.GetAlertsAsync(productName);
    }

    #endregion

    /// <summary>
    /// Realiza una petición a la API
    /// 
    /// <para>
    /// <paramref name="function"/>
    /// Ruta de la función a la que se quiere acceder en la API.
    ///<para>
    ///<example>
    /// Ejemplos: "User/GetUser", "Post/GetAll", "Post/GetById?id=1" (Si la función requiere un argumento llamado id)
    ///</example>
    /// </para>
    /// </para>
    /// 
    /// <para>
    /// <paramref name="instruction"/>
    /// Tipo de instrucción a realizar en la API.
    /// <para>
    /// Puede ser Get (Recuperar), Post (Añadir), Put (Modificar) o Delete (Borrar).
    /// </para>
    /// </para>
    /// 
    /// <para>
    /// <paramref name="content"/>
    /// Contenido de la petición. Solo necesario en caso de que la instrucción sea Post o Put.
    /// <para>
    ///  El contenido son los argumentos de la API [FromBody].
    /// </para>
    /// <para>
    /// Este contenido debe ser un objeto serializado en formato JSON.
    /// </para>
    /// </para>
    /// </summary>
    /// 
    /// <returns>
    /// Devuelve el resultado de la petición a la API en formato JSON.
    /// </returns>

    public async Task<string> PerformApiInstructionAsync(string function, ApiInstruction instruction, object? content = null)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://smarttradeapi2.azurewebsites.net/");
        client.DefaultRequestHeaders.Add("Logged", Logged?.Email);

        HttpContent? httpContent = null;
        if (content != null)
        {
            httpContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
        }

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
                    response = await client.PostAsync("SmartTradeAPI/" + function, httpContent);
                    break;
                case ApiInstruction.Put:
                    if (content == null) throw new ArgumentNullException(nameof(content));
                    response = await client.PutAsync("SmartTradeAPI/" + function, httpContent);
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