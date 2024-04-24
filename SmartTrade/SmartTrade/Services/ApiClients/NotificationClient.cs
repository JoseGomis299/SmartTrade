using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartTradeDTOs;

namespace SmartTrade.Services.ApiClients;

public class NotificationClient : ApiClient
{
    public NotificationClient(SmartTradeBroker broker) : base(broker, "Notification") { }

    public async Task<List<NotificationDTO>?> GetNotificationsAsync()
    {
        return JsonConvert.DeserializeObject<List<NotificationDTO>>(await PerformApiInstructionAsync("GetNotifications", ApiInstruction.Get));
    }

    public async Task DeleteNotificationAsync(int notificationId)
    {
        await PerformApiInstructionAsync($"DeleteElement?id={notificationId}", ApiInstruction.Delete);
    }

    public async Task SetNotificationAsVisitedAsync(int notificationId)
    {
        await PerformApiInstructionAsync($"SetAsVisited?id={notificationId}", ApiInstruction.Put);
    }
}