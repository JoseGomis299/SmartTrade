using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Newtonsoft.Json;
using SmartTrade.Views;
using SmartTradeDTOs;
using ReactiveUI;
using SmartTrade.Services;

namespace SmartTrade.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string? SearchText { get; set; }
    public ObservableCollection<string> SearchAutoComplete { get; set; }

    private bool _cartVisible;
    private bool _buttonsVisible;

    public UserType LoggedType => Service.GetUserType();

    public MainViewModel()
    {
        SearchAutoComplete = new ObservableCollection<string>();
    }

    public async Task InitializeAsync(MainView mainView)
    {
        
        await mainView.ShowCatalogAsync();

        foreach (var name in Proxy.Posts.Select(x => x.Title))
        {
            SearchAutoComplete.Add(name);
        }

        Proxy.OnPostsChanged += () =>
        {
            SearchAutoComplete.Clear();
            foreach (var name in Proxy.Posts.Select(x => x.Title))
            {
                SearchAutoComplete.Add(name);
            }
        };
    }

    public List<SimplePostDTO>? FindProducts()
    {
        return  Service.GetPostsFuzzyContain(SearchText); 
    }

    public bool CartVisible
    {
        get => _cartVisible;
        set => this.RaiseAndSetIfChanged(ref _cartVisible, value);
    }

    public bool ButtonVisible
    {
        get => _buttonsVisible;
        set => this.RaiseAndSetIfChanged(ref _buttonsVisible, value);
    }
}
