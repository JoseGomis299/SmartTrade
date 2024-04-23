using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartTradeDTOs;

namespace SmartTrade.Services.ApiClients;

public class PostClient : ApiClient
{
    public PostClient(SmartTradeBroker broker) : base(broker, "Post") { }

    public async Task AddPostAsync(PostDTO post)
    {
        await PerformApiInstructionAsync("PublishPost", ApiInstruction.Post, post);
    }

    public async Task<List<SimplePostDTO>?> GetPostsAsync()
    {
        return JsonConvert.DeserializeObject<List<SimplePostDTO>>(await PerformApiInstructionAsync("GetAll", ApiInstruction.Get)); ;
    }

    public async Task<PostDTO?> GetPostAsync(int postId)
    {
        return JsonConvert.DeserializeObject<PostDTO>(await PerformApiInstructionAsync($"GetById?id={postId}", ApiInstruction.Get));
    }

    public async Task<List<string>?> GetPostsNamesAsync()
    {
        return JsonConvert.DeserializeObject<List<string>>(await PerformApiInstructionAsync("GetAllNames", ApiInstruction.Get));
    }

    public async Task EditPostAsync(int postId, PostDTO postInfo)
    {
        await PerformApiInstructionAsync($"EditPost?id={postId}", ApiInstruction.Put, postInfo);
    }

    public async Task DeletePostAsync(int postId)
    {
        await PerformApiInstructionAsync($"RemovePost?id={postId}", ApiInstruction.Delete);
    }
}