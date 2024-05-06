using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartTradeDTOs;

namespace SmartTrade.Services.ApiClients;

public class AlertClient : ApiClient
{
    public AlertClient(SmartTradeBroker broker) : base(broker, "Alert") { }

    public async Task<int> CreateAlertAsync(string productName)
    {
        return int.Parse(await PerformApiInstructionAsync($"CreateAlert?productName={productName}", ApiInstruction.Post, new()));
    }

    public async Task DeleteAlertAsync(int alertId)
    {
        await PerformApiInstructionAsync($"DeleteAlert?id={alertId}", ApiInstruction.Delete);
    }

    public async Task DeleteAlertAsync(string productName)
    {
        await PerformApiInstructionAsync($"DeleteAlertByProductName?productName={productName}", ApiInstruction.Delete);
    }

    public async Task<AlertDTO?> GetAlertAsync(string productName)
    {
        return JsonConvert.DeserializeObject<AlertDTO>(await PerformApiInstructionAsync($"GetAlertByProductName?productName={productName}", ApiInstruction.Get));
    }

    public async Task<List<AlertDTO>?> GetAlertsAsync()
    {
        return JsonConvert.DeserializeObject<List<AlertDTO>>(await PerformApiInstructionAsync($"GetUserAlerts", ApiInstruction.Get));
    }
}