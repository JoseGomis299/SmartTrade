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

    public void ValidatePost()
    {
        PostDTO postDto = CreatePostInfo(Post.SellerID);
        postDto.Validated = true;

        string postInfoJson = JsonConvert.SerializeObject(postDto);

        MainViewModel.SmartTradeService.EditPost((int) Post.Id, postInfoJson);
    }

    public void RejectPost()
    {
        MainViewModel.SmartTradeService.DeletePost((int) Post.Id);
    }
}