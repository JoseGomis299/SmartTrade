using SmartTrade.Entities;
using System.Collections.ObjectModel;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels;

public class SellerCatalogModel : ViewModelBase
{
    public ObservableCollection<ProductModel> MyProducts { get; set; }

    public SellerCatalogModel()
    {
        MyProducts = new ObservableCollection<ProductModel>();
    }

    public async Task LoadProductsAsync()
    {
        List<PostDTO> posts = JsonConvert.DeserializeObject<List<PostDTO>>( await SmartTradeService.Instance.GetPostsAsync());

        posts.ForEach(post =>
        {
             MyProducts.Add(new ProductModel(post));
        });
    }

}