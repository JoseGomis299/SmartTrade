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
    public UserRepository UserRepository;
    public PostRepository PostRepository;
    public NotificationRepository NotificationRepository;
    public AlertRepository AlertRepository;

    public UserDTO? Logged { get; set; }

    public SmartTradeBroker()
    {
        UserRepository = new UserRepository(this);
        PostRepository = new PostRepository(this);
        NotificationRepository = new NotificationRepository(this);
        AlertRepository = new AlertRepository(this);
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

        await NotificationRepository.GetNotificationsAsync();
    }


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