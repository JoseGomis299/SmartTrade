using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SmartTradeLib.Entities;

namespace SmartTrade.ViewModels;

public class ValidatePostModel : PostModificationModel
{
    private Post _post { get; }

    public ValidatePostModel()
    {
    }

    public ValidatePostModel(Post post)
    {
        _post = post;
        Title = post.Title;
        Description = post.Description;

        Product product = post.Offers.First().Product;

        ProductName =product.Name;
        Category = product.GetCategories().First();
        MinimumAge = product.MinimumAge.ToString();
        Certifications = product.Certification;
        EcologicPrint = product.EcologicPrint;

        foreach (var offer in post.Offers)
        {
            Stock stock = new(offer, this);
            Stocks.Add(stock);
        }
    }

    public void ValidatePost()
    {
        List<int> stocks = new();
        List<float> prices = new();
        List<float> shippingCosts = new();

        List<List<byte[]>> images = new();
        List<List<string>> attributes = new();

        foreach (var stock in Stocks)
        {
            images.Add(stock.Images.Select(image => image.Bytes).ToList());

            stocks.Add(int.Parse(stock.StockQuantity));
            prices.Add(float.Parse(stock.Price));
            shippingCosts.Add(float.Parse(stock.ShippingCost));

            attributes.Add(stock.CategoryAttributes.Select(attribute => attribute.Value).ToList());
        }

        MainViewModel.SmartTradeService.ValidatePost(Title, Description, ProductName, Category, int.Parse(MinimumAge), Use,Certifications, EcologicPrint, ReducePrint, stocks, prices, shippingCosts, images, attributes, _post);
    }

    public void RejectPost()
    {
        MainViewModel.SmartTradeService.RejectPost(_post);
    }
}