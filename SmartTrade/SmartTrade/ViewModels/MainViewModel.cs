using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using Newtonsoft.Json;
using SmartTrade.Views;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string? SearchText { get; set; }
    public ObservableCollection<string> SearchAutoComplete { get; set; }

    public MainViewModel()
    {
        SearchAutoComplete = new ObservableCollection<string>();
    }

    public async Task InitializeAsync(MainView mainView)
    {
        
        await mainView.ShowCatalogAsync();

        foreach (var name in (await SmartTradeService.Instance.GetPostsNamesAsync())!)
        {
            SearchAutoComplete.Add(name);
        }
    }

    public async Task<List<PostDTO>?> LoadProductsAsync()
    {
        return await SmartTradeService.Instance.GetPostsFuzzyContainAsync(SearchText);
    }


    //public async Task<UserControl> GetCatalogAsync()
    //{
    //    if (SmartTradeService.Instance.Logged == null)
    //    {
    //        ProductCatalog productCatalog = new ProductCatalog();
    //        await ((ProductCatalogModel)productCatalog.DataContext).LoadProductsAsync();

    //        return productCatalog;
    //    }

    //    if (SmartTradeService.Instance.Logged.IsSeller)
    //    {
    //        SellerCatalog sellerCatalog = new SellerCatalog();
    //        await ((SellerCatalogModel)sellerCatalog.DataContext).LoadProductsAsync();

    //        return sellerCatalog;
    //    }

    //    if (SmartTradeService.Instance.Logged.IsAdmin)
    //    {
    //        AdminCatalog adminCatalog = new AdminCatalog();
    //        await ((AdminCatalogModel)adminCatalog.DataContext).LoadProductsAsync();

    //        return adminCatalog;
    //    }

    //    ProductCatalog productCatalogg = new ProductCatalog();
    //    await ((ProductCatalogModel)productCatalogg.DataContext).LoadProductsAsync();

    //    return productCatalogg;
    //}

}
