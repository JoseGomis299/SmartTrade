using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels;

public class ValidatePostModel : PostModificationModel
{
    public PostDTO Post { get; }

    public ValidatePostModel()
    {
    }

    public ValidatePostModel(PostDTO post)
    {
        Post = post;
        Title = post.Title;
        Description = post.Description;

        ProductName = post.ProductName;
        Category = post.Category;
        MinimumAge = post.MinimumAge.ToString();
        Certifications = post.Certifications;
        EcologicPrint = post.EcologicPrint;

        foreach (var offer in post.Offers)
        {
            Stock stock = new(offer, this);
            Stocks.Add(stock);
        }
    }

    public async Task ValidatePostAsync()
    {
        PostDTO postDto = CreatePostInfo(Post);
        postDto.Validated = true;

        await SmartTradeService.Instance.EditPostAsync((int) Post.Id, postDto);
        SmartTradeNavigationManager.Instance.MainView.ShowCatalogReinitializingAsync();
    }

    public async Task RejectPostAsync()
    {
       await SmartTradeService.Instance.DeletePostAsync((int) Post.Id);
       SmartTradeNavigationManager.Instance.MainView.ShowCatalogReinitializingAsync();
    }
}