using SmartTrade.Entities;
using System.Collections.ObjectModel;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels;

public class SellerCatalogModel : CatalogModel
{
    public ObservableCollection<ProductModel> MyProducts { get; set; }

    public SellerCatalogModel()
    {
        MyProducts = new ObservableCollection<ProductModel>();
    }

    public override async Task LoadProductsAsync()
    {
        List<SimplePostDTO>? posts = await SmartTradeService.Instance.GetPostsAsync();

        posts.ForEach(post =>
        {
             MyProducts.Add(new ProductModel(post));
        });
    }

}