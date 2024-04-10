using SmartTradeLib.Entities;
using System.Collections.ObjectModel;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels;

public class SellerCatalogModel : ViewModelBase
{
    public ObservableCollection<ProductModel> MyProducts { get; set; }

    public SellerCatalogModel()
    {
        MyProducts = new ObservableCollection<ProductModel>();
        LoadProducts();
    }

    private void LoadProducts()
    {
        List<PostDTO> posts = JsonConvert.DeserializeObject<List<PostDTO>>(MainViewModel.SmartTradeService.GetPosts());

        posts.ForEach(post =>
        {
             MyProducts.Add(new ProductModel(post));
        });
    }

}