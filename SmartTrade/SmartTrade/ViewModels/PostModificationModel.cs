using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SmartTradeDTOs;
using SmartTrade.Entities;

namespace SmartTrade.ViewModels;

public class PostModificationModel : ViewModelBase
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Use { get; set; }
    public string? ProductName { get; set; }

    public Category Category
    {
        get => _category;
        set
        {
            if (Stocks.Count > 0)
            {
                Stocks[0].ChangeCategory(value);
                for (int i = 1; i < Stocks.Count; i++)
                {
                    Stocks.RemoveAt(i);
                }
            }
            _category = value;
        }
    }
    public string? MinimumAge { get; set; }
    public string? Certifications { get; set; }
    public string? EcologicPrint { get; set; }
    public string? ReducePrint { get; set; }
    public ObservableCollection<Stock> Stocks { get; } = new ObservableCollection<Stock>();
    public UserDTO Logged => Service.Logged;

    private Category _category;

    public void CheckErrors()
    {
        if (Stocks.Count == 0)
            throw new Exception("Post must have at least one stock");

        for (var i = 0; i < Stocks.Count; i++)
        {
            var stock = Stocks[i];
            if (string.IsNullOrEmpty(stock.StockQuantity) || string.IsNullOrEmpty(stock.Price) ||
                string.IsNullOrEmpty(stock.ShippingCost))
                throw new Exception("Stock must have all fields filled");

            if(int.Parse(stock.StockQuantity) <= 0)
                throw new Exception("Stock quantity must be greater than 0");

            if (float.Parse(stock.Price) <= 0)
                throw new Exception("Price must be greater than 0");


            foreach (var attribute in stock.CategoryAttributes)
            {
                if (string.IsNullOrEmpty(attribute.Value))
                    throw new Exception($"Stock {i} must have all fields filled");

                if (attribute.OnlyInt && !int.TryParse(attribute.Value, out _))
                    throw new Exception($"Field \"{attribute.Name}\" in stock {i} must be a number");

                if (attribute.OnlyFloat && !float.TryParse(attribute.Value, out _))
                    throw new Exception($"Field \"{attribute.Name}\" in stock {i}  must be a number");
            }

            if (stock.Images.Count == 0)
                throw new Exception($"Stock {i} must have at least one image");

            for (var j = 0; j < Stocks.Count; j++)
            {
                var otherStock = Stocks[j];
                if (stock != otherStock && stock.CategoryAttributes.SequenceEqual(otherStock.CategoryAttributes))
                    throw new Exception($"Stocks {i} and {j} must have different attributes");
            }
        }
    }

    protected PostDTO CreatePostInfo(PostDTO? post)
    {
        List<int> stocks = new();
        List<float> prices = new();
        List<float> shippingCosts = new();

        List<List<byte[]>> images = new();
        List<Dictionary<string, string>> attributes = new();

        foreach (var stock in Stocks)
        {
            images.Add(stock.Images.Select(image => image.Bytes).ToList());

            stocks.Add(int.Parse(stock.StockQuantity));
            prices.Add(float.Parse(stock.Price));
            shippingCosts.Add(float.Parse(stock.ShippingCost));

            var attributesDictionary = new Dictionary<string, string>();
            foreach (var attribute in stock.CategoryAttributes)
            {
                attributesDictionary.Add(attribute.Name, attribute.Value);
            }
            attributes.Add(attributesDictionary);
        }

        PostDTO postDto = new()
        {
            Title = Title,
            Description = Description,
            ProductName = ProductName,
            Category = Category,
            MinimumAge = int.Parse(MinimumAge),
            HowToUse = Use,
            Certifications = Certifications,
            EcologicPrint = EcologicPrint,
            HowToReducePrint = ReducePrint,
            Validated = false,
            SellerID = post != null ? post.SellerID : "",
            Offers = new List<OfferDTO>(),
            SellerCompanyName = post != null? post.SellerCompanyName : "",
            Id = post?.Id,
            Ratings = post?.Ratings ?? new List<RatingDTO>(),
            NumRatings = post?.NumRatings ?? 0
        };

        for (int i = 0; i < stocks.Count; i++)
        {
            OfferDTO offerDto = new()
            {
                Stock = stocks[i],
                Price = prices[i],
                ShippingCost = shippingCosts[i],
                Product = new ProductDTO
                {
                    Images = images[i],
                    Attributes = attributes[i],
                    Info = "",
                    Id = 0,
                    Differentiators = "",
                }
            };

            if (post?.Offers.Count > i)
                offerDto.Id = post.Offers[i].Id;
            else offerDto.Id = -1;
            postDto.Offers.Add(offerDto);
        }

        return postDto;
    }
}