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
    public string? CartItems { get; set; }
    public string? SearchText { get; set; }
    public ObservableCollection<string> SearchAutoComplete { get; set; }

    private bool _cartVisible;
    private bool _buttonsVisible;

    public UserType LoggedType => Service.LoggedType;

    public MainViewModel()
    {
        SearchAutoComplete = new ObservableCollection<string>();

        Service.OnCartChanged += OnServiceOnOnCartChanged;
        OnServiceOnOnCartChanged();
    }

    private void OnServiceOnOnCartChanged()
    {
        CartItems = Service.CartItemsCount.ToString();
        this.RaisePropertyChanged(nameof(CartItems));
    }

    public async Task InitializeAsync(MainView mainView)
    {
        await mainView.ShowCatalogAsync();
        await Service.InitializeCacheAsync();

        foreach (var name in Service.Posts.Select(x => x.Title))
        {
            SearchAutoComplete.Add(name);
        }

        Service.OnPostsChanged += () =>
        {
            SearchAutoComplete.Clear();
            foreach (var name in Service.Posts.Select(x => x.Title))
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

    public int NotificationsCount
    {
        get
        {
            if (Service.Notifications == null) return 0;
            return Service.Notifications.Count(x => !x.Visited);
        }
    }
}
