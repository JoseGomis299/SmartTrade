using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartTradeDTOs;

namespace SmartTrade.Services.ApiClients;

public class WishClient : ApiClient
{
    public WishClient(SmartTradeBroker broker) : base(broker, "Wish") { }

    public async Task<int> CreateWishAsync(int postId)
    {
        return int.Parse(await PerformApiInstructionAsync($"CreateWish?id={postId}", ApiInstruction.Post, new()));
    }

    public async Task DeleteWishAsync(int WishId)
    {
        await PerformApiInstructionAsync($"DeletWish?id={WishId}", ApiInstruction.Delete);
    }

    public async Task<List<WishDTO>?> GetWishAsync()
    {
        return JsonConvert.DeserializeObject<List<WishDTO>>(await PerformApiInstructionAsync($"GetWishList", ApiInstruction.Get));
    }
}