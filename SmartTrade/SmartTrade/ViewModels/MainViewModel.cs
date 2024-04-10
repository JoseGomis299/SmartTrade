using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Newtonsoft.Json;
using SmartTrade.Views;
using SmartTradeDTOs;
using SmartTradeLib.BusinessLogic;
using SmartTradeLib.Entities;

namespace SmartTrade.ViewModels;

public class MainViewModel : ViewModelBase
{
    public static ISmartTradeService SmartTradeService { get; } = new SmartTradeService();
    public string? SearchText { get; set; }
    public ObservableCollection<string> SearchAutoComplete { get; set; }

    public MainViewModel()
    {
        SearchAutoComplete = new ObservableCollection<string>();

        foreach (var name in SmartTradeService.GetPostsNamesStartWith("", Int32.MaxValue))
        {
            SearchAutoComplete.Add(name);
        }

    }

    public List<PostDTO> LoadProducts()
    {
        return JsonConvert.DeserializeObject<List<PostDTO>>(SmartTradeService.GetPostsFuzzyContain(SearchText));
    }

    public UserControl GetCatalog()
    {
        if (SmartTradeService.Logged is Seller)
        {
            return new SellerCatalog();
        }

        if (SmartTradeService.Logged is Admin)
        {
            return new AdminCatalog();
        }

        return new ProductCatalog();
    }

}
