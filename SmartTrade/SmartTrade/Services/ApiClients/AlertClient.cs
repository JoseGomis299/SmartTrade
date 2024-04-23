using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartTradeDTOs;

namespace SmartTrade.Services.ApiClients;

public class AlertRepository : ApiRepository
{
    public AlertRepository(SmartTradeBroker broker) : base(broker, "Alert") { }

    public async Task<int> CreateAlertAsync(int productId)
    {
        return int.Parse(await PerformApiInstructionAsync($"CreateAlert?id={productId}", ApiInstruction.Post));
    }

    public async Task DeleteAlertAsync(int alertId)
    {
        await PerformApiInstructionAsync($"Delete?id={alertId}", ApiInstruction.Delete);
    }

    public async Task<AlertDTO?> GetAlertsAsync(string productName)
    {
        return JsonConvert.DeserializeObject<AlertDTO>(await PerformApiInstructionAsync($"GetAlert?productName={productName}", ApiInstruction.Get));
    }
}