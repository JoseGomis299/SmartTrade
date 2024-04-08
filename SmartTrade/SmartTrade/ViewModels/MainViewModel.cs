using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

    public List<Post> LoadProducts()
    {
        return SmartTradeService.GetPostsFuzzyContain(SearchText);
    }

    public List<string> GetNamesProducts()
    {
        return SmartTradeService.GetPostsNamesStartWith(SearchText, 8);
    }
}
