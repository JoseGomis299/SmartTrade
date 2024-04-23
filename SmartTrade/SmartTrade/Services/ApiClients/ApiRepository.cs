using System.Net.Http;
using System.Threading.Tasks;

namespace SmartTrade.Services.ApiClients;

public abstract class ApiRepository
{
    protected SmartTradeBroker Broker { get; }
    protected string BasePath { get; }

    protected ApiRepository(SmartTradeBroker broker, string basePath)
    {
        Broker = broker;
        BasePath = basePath;
    }

    protected async Task<string> PerformApiInstructionAsync(string function, ApiInstruction instruction, object? content = null)
    {
        return await Broker.PerformApiInstructionAsync($"{BasePath}/{function}", instruction, content);
    }
}