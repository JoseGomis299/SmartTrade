using SmartTradeLib.Entities;
using System.Collections.ObjectModel;
using System;

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
        MainViewModel.SmartTradeService.GetPosts().ForEach(post =>
        {
             MyProducts.Add(new ProductModel(post));
        });
    }

}