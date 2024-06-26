﻿using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartTrade.Services;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels;

public class ValidatePostModel : PostModificationModel
{
    public PostDTO Post { get; }
    public UserDTO Logged => Service.Logged;

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

    public async Task EditPostAsync()
    {
        PostDTO postDto = CreatePostInfo(Post);
        postDto.Validated = Logged.IsAdmin;

        await Service.EditPostAsync((int) Post.Id, postDto);
        SmartTradeNavigationManager.Instance.MainView.ShowCatalogReinitializingAsync();
    }

    public async Task RejectPostAsync()
    {
       await Service.DeletePostAsync((int) Post.Id);
       SmartTradeNavigationManager.Instance.MainView.ShowCatalogReinitializingAsync();
    }
}