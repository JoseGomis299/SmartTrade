using Avalonia.Controls;
using System;
using Avalonia.Input;
using Avalonia.Interactivity;
using SmartTrade.ViewModels;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Newtonsoft.Json;
using System.Collections.Generic;
using SmartTradeDTOs;

namespace SmartTrade.Views;

public partial class MainView : UserControl
{
    private MainViewModel _model;

    private Bitmap? _homeImage;
    private Bitmap _userImage;
    private Bitmap _cartImage;
    private Bitmap? _cartImageSelected;
    private Bitmap? _userImageSelected;
    private Bitmap? _homeImageSelected;

    int _selectedButton = 0;

    public MainView()
    {
        DataContext = _model = new MainViewModel();
        SmartTradeNavigationManager.Instance.OnNavigate += HandleNavigation;
        SmartTradeNavigationManager.Instance.OnChangeNavigationStack += SelectButton;
        InitializeComponent();

        SmartTradeNavigationManager.Instance.Initialize(ViewContent, new ProductCatalog());

        AutoCompleteBox.TextChanged += AutoCompleteBox_TextChanged;
        AutoCompleteBox.KeyDown += AutoCompleteBox_KeyDown;

        AutoCompleteBox.ItemsSource = _model.SearchAutoComplete;

        ProfileButton.Click += OnProfileButtonOnClick;
        HomeButton.Click += OnHomeButtonOnClick;
        ShoppingCartButton.Click += OnShoppingCartButtonOnClick;

        _homeImage = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/Home.png")));
        _userImage = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/User.png")));
        _cartImage = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/Cart.png")));
        _cartImageSelected = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/CartSelected.png")));
        _userImageSelected = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/UserSelected.png")));
        _homeImageSelected = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/HomeSelected.png")));

        HomeImage.Source = _homeImageSelected;
        UserImage.Source = _userImage;
        CartImage.Source = _cartImage;
    }

    private void OnShoppingCartButtonOnClick(object? sender, RoutedEventArgs e)
    {
        SmartTradeNavigationManager.Instance.NavigateWithButton(new ShoppingCartView(), _selectedButton, 1);
    }

    private async void OnHomeButtonOnClick(object? sender, RoutedEventArgs e)
    {
        if (SmartTradeService.Instance.Logged == null)
        {
            ProductCatalog productCatalog = new ProductCatalog();

            if(SmartTradeNavigationManager.Instance.NavigateWithButton(productCatalog, _selectedButton, 0))
                await ((ProductCatalogModel)productCatalog.DataContext).LoadProductsAsync();
            return;
        }

        if (SmartTradeService.Instance.Logged.IsSeller)
        {
            SellerCatalog sellerCatalog = new SellerCatalog();

            if(SmartTradeNavigationManager.Instance.NavigateWithButton(sellerCatalog, _selectedButton, 0))
              await ((SellerCatalogModel)sellerCatalog.DataContext).LoadProductsAsync();
            return;
        }

        if (SmartTradeService.Instance.Logged.IsAdmin)
        {
            AdminCatalog adminCatalog = new AdminCatalog();

            if(SmartTradeNavigationManager.Instance.NavigateWithButton(adminCatalog, _selectedButton, 0))
                await ((AdminCatalogModel)adminCatalog.DataContext).LoadProductsAsync();
            return;
        }

        ProductCatalog productCatalogg = new ProductCatalog();

        if(SmartTradeNavigationManager.Instance.NavigateWithButton(productCatalogg, _selectedButton, 0))
         await ((ProductCatalogModel)productCatalogg.DataContext).LoadProductsAsync();
    }

    private void OnProfileButtonOnClick(object? sender, RoutedEventArgs e)
    {
        if(SmartTradeService.Instance.Logged == null) 
            SmartTradeNavigationManager.Instance.NavigateWithButton(new Login(), _selectedButton, 2);
        else SmartTradeNavigationManager.Instance.NavigateWithButton(new Profile(), _selectedButton, 2);
    }

    private void SelectButton(int i)
    {
        HomeImage.Source = _homeImage;
        UserImage.Source = _userImage;
        CartImage.Source = _cartImage;

        _selectedButton = i;

        if (i == 0) HomeImage.Source = _homeImageSelected;
        else if (i == 1) CartImage.Source = _cartImageSelected;
        else if (i == 2) UserImage.Source = _userImageSelected;
    }

    private void HandleNavigation(Type type)
    {
        if (type == typeof(RegisterPost))
        {
            SearchBar.IsVisible = false;
            return;
        }

        ResetVisibility();
    }
    private void ResetVisibility()
    {
        SearchBar.IsVisible = true;
        BottomBar.IsVisible = true;
    }

    private async void AutoCompleteBox_KeyDown(object? sender, KeyEventArgs e)
    {

        if (e.Key.Equals(Key.Enter))
        {
            SmartTradeNavigationManager.Instance.NavigateToOverriding(new SearchResult(await _model.LoadProductsAsync()));
        }
    }

    private void AutoCompleteBox_TextChanged(object? sender, TextChangedEventArgs e)
    {
        _model.SearchText = AutoCompleteBox.Text;
    }
}
