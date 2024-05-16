using Avalonia.Controls;
using System;
using Avalonia.Input;
using Avalonia.Interactivity;
using SmartTrade.ViewModels;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System.Threading.Tasks;
using SmartTrade.Helpers;
using SmartTrade.Services;
using SmartTradeDTOs;

namespace SmartTrade.Views;

public partial class MainView : UserControl
{
    private MainViewModel _model;
    private CatalogModel? _catalogModel;
    private SearchResultModel? _searchModel;

    private Bitmap? _homeImage;
    private Bitmap _userImage;
    private Bitmap _cartImage;
    private Bitmap? _cartImageSelected;
    private Bitmap? _userImageSelected;
    private Bitmap? _homeImageSelected;
    private Bitmap? _alertImage;
    private Bitmap? _alertSelectedImage;

    public int SelectedButton = 0;

    private int? _currentCategory;
    private readonly LoadingScreenManager _loadingScreenManager;

    public bool ShowingPopUp { get; set; }
    public bool ReinitializeHomeNextTime { get; set; }
    
    public MainView()
    {
        DataContext = _model = new MainViewModel();
        SmartTradeNavigationManager.Instance.OnNavigate += HandleNavigation;
        SmartTradeNavigationManager.Instance.OnChangeNavigationStack += SelectButton;
        InitializeComponent();

        Initialize();
        _loadingScreenManager = LoadingScreenManager.Instance;
    }

    #region Initialization

    private void Initialize()
    {
        SmartTradeNavigationManager.Instance.Initialize(ViewContent, new ProductCatalog());
        SmartTradeNavigationManager.Instance.MainView = this;

        InitializeSearchBar();
        InitializeButtons();
        InitializeImages();
        SetButtonVisibility();
    }

    private void InitializeImages()
    {
        _homeImage = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/Home.png")));
        _userImage = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/User.png")));
        _cartImage = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/Cart.png")));
        _cartImageSelected = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/CartSelected.png")));
        _userImageSelected = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/UserSelected.png")));
        _homeImageSelected = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/HomeSelected.png")));
        _alertImage = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/Alert.png")));
        _alertSelectedImage = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/AlertSelected.png")));

        HomeImage.Source = _homeImageSelected;
        UserImage.Source = _userImage;
        CartImage.Source = _cartImage;
        AlertImage.Source = _alertImage;
    }

    private void InitializeButtons()
    {
        SideBarButton.Click += SideBarButton_Click;
        SideBar.PaneClosing += SideBar_PaneClosing;
        ListBoxDepartments.SelectionChanged += ListBoxDepartments_SelectionChanged;
        WishListButton.Click += OpenWishList;
        GiftListButton.Click += OpenGiftList;
        AlertButton.Click += OnAlertButtonOnClick;
        ProfileButton.Click += OnProfileButtonOnClick;
        HomeButton.Click += OnHomeButtonOnClick;
        ShoppingCartButton.Click += OnShoppingCartButtonOnClick;
        AddPostButton.Click += OnAddPostButtonOnClick;
    }

    private void InitializeSearchBar()
    {
        AutoCompleteBox.TextChanged += AutoCompleteBox_TextChanged;
        AutoCompleteBox.KeyDown += AutoCompleteBox_KeyDown;
        AutoCompleteBox.ItemsSource = _model.SearchAutoComplete;
    }

    private void SetButtonVisibility()
    {
        if (_model.LoggedType == UserType.Admin || _model.LoggedType == UserType.Seller)
        {
            ShoppingCartButton.IsVisible = false;
            AlertButton.IsVisible = false;
            CartItems.IsVisible = false;
            Menus.IsVisible = false;

            if (_model.LoggedType == UserType.Seller)
            {
                AddPostButton.IsVisible = true;
            }
        }
        else if (_model.LoggedType == UserType.Consumer)
        {
            ShoppingCartButton.IsVisible = true;
            AlertButton.IsVisible = true;
            CartItems.IsVisible = true;
            Menus.IsVisible = true;
            AddPostButton.IsVisible = false;
        }
        else
        {
            ShoppingCartButton.IsVisible = true;
            AlertButton.IsVisible = false;
            CartItems.IsVisible = true;
            Menus.IsVisible = false;
            AddPostButton.IsVisible = false;
        }
    }

    private void SetNotificationsImage()
    {
        if (_model.NotificationsCount > 0)
        {
            AlertImage.Source = _alertSelectedImage;
        }
        else
        {
            AlertImage.Source = _alertImage;
        }
    }

    #endregion


    #region SideBar
    private void SideBarButton_Click(object? sender, RoutedEventArgs e)
    {
        SideBar.IsPaneOpen = true;
        DarkenBorder.Background = new Avalonia.Media.SolidColorBrush(Avalonia.Media.Color.FromRgb(183,183,183));
    }
    private void SideBar_PaneClosing(object? sender, CancelRoutedEventArgs e)
    {
        DarkenBorder.Background = new Avalonia.Media.SolidColorBrush(Avalonia.Media.Color.FromArgb(0,0,0,0));
    }

    private void ListBoxDepartments_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        _currentCategory = ListBoxDepartments.SelectedIndex;
        SideBar.IsPaneOpen = false;

        if (ListBoxDepartments.SelectedIndex == -1)
        {
            _currentCategory = null;
        }

        _catalogModel?.SortByCategory(_currentCategory);
        _searchModel?.SortByCategory(_currentCategory);
    }

    private void OpenWishList(object? sender, RoutedEventArgs e)
    {
        var view = new WishListView();
        SmartTradeNavigationManager.Instance.NavigateTo(view);
    }

    private void OpenGiftList(object? sender, RoutedEventArgs e)
    {
        var view = new GiftsView();
        SmartTradeNavigationManager.Instance.NavigateTo(view);
    }

    #endregion

    #region Buttons

    private void OnAddPostButtonOnClick(object? sender, RoutedEventArgs e)
    {
        SmartTradeNavigationManager.Instance.NavigateTo(new RegisterPost());
    }

    private void OnShoppingCartButtonOnClick(object? sender, RoutedEventArgs e)
    {
        if (_loadingScreenManager.IsLoadingCart)
        {
            SmartTradeNavigationManager.Instance.NavigateWithButton(null, SelectedButton, 1, out _);
            _loadingScreenManager.ShowLoadingScreen();
            return;
        }

        _loadingScreenManager.HideLoadingScreen();
        SmartTradeNavigationManager.Instance.NavigateWithButton(typeof(ShoppingCartView), SelectedButton, 1, out _, true);
    }

    private async void OnHomeButtonOnClick(object? sender, RoutedEventArgs e)
    {
        if (ReinitializeHomeNextTime)
        {
            await ShowCatalogReinitializingAsync();
            ReinitializeHomeNextTime = false;
        }
        else await ShowCatalogAsync();
    }

    private void OnProfileButtonOnClick(object? sender, RoutedEventArgs e)
    {
        if (_loadingScreenManager.IsLoadingUser)
        {
            SmartTradeNavigationManager.Instance.NavigateWithButton(null, SelectedButton, 2, out _);
            _loadingScreenManager.ShowLoadingScreen();
            return;
        }

        _loadingScreenManager.HideLoadingScreen();
        if (_model.LoggedType == UserType.None)
            SmartTradeNavigationManager.Instance.NavigateWithButton(typeof(Login), SelectedButton, 2, out _);
        else SmartTradeNavigationManager.Instance.NavigateWithButton(typeof(Profile), SelectedButton, 2, out _);
    }

    public async Task ShowCatalogAsync()
    {
        if (_loadingScreenManager.IsLoadingHome)
        {
            SmartTradeNavigationManager.Instance.NavigateWithButton(null, SelectedButton, 0, out _);
            _loadingScreenManager.ShowLoadingScreen();
            return;
        }

        _loadingScreenManager.HideLoadingScreen();
        if (_model.LoggedType == UserType.None)
        {
            await NavigateTo(typeof(ProductCatalog));
            return;
        }

        if (_model.LoggedType == UserType.Seller)
        {
            await NavigateTo(typeof(SellerCatalog));
            return;
        }

        if (_model.LoggedType == UserType.Admin)
        {
            await NavigateTo(typeof(AdminCatalog));
            return;
        }

        await NavigateTo(typeof(ProductCatalog));

        async Task NavigateTo(Type catalogType)
        {
            if (SelectedButton != 0 || (SelectedButton == 0 && SmartTradeNavigationManager.Instance.Navigator.CurrentView.GetType() != catalogType))
            {
                SmartTradeNavigationManager.Instance.NavigateWithButton(catalogType, SelectedButton, 0, out _);
                return;
            }

            if (SmartTradeNavigationManager.Instance.NavigateWithButton(catalogType, SelectedButton, 0, out var catalog, true))
            {
                int loadingScreen = _loadingScreenManager.StartLoading();
                var model = (CatalogModel)catalog.DataContext;
                await model.LoadProductsAsync();
                _catalogModel = model;

                if (_currentCategory != null)
                    _catalogModel.SortByCategory(_currentCategory); 

                if (SelectedButton == 0) SmartTradeNavigationManager.Instance.NavigateToWithoutSaving(catalog);
                _loadingScreenManager.StopLoading(loadingScreen);
            }
        }
    }

    private void SelectButton(int i)
    {
        HomeImage.Source = _homeImage;
        UserImage.Source = _userImage;
        CartImage.Source = _cartImage;

        SelectedButton = i;

        if (i == 0) HomeImage.Source = _homeImageSelected;
        else if (i == 1) CartImage.Source = _cartImageSelected;
        else if (i == 2) UserImage.Source = _userImageSelected;
    }

    public async void OnAlertButtonOnClick(object? sender, RoutedEventArgs e)
    {
        AlertView view = new AlertView();
        AlertViewModel model = (AlertViewModel)view.DataContext;
        await model.LoadNotificationsAsync();

        SmartTradeNavigationManager.Instance.NavigateTo(view);
    }

    #endregion

    #region Navigation

    private void HandleNavigation(Type type)
    {
        if (type == typeof(RegisterPost))
        {
            SearchBar.IsVisible = false;
            return;
        }

        if (SelectedButton == 2)
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

    public async Task ShowCatalogReinitializingAsync()
    {
        SetButtonVisibility();
        _loadingScreenManager.HideLoadingScreen();

        if (_model.LoggedType == UserType.Seller)
        {
            await NavigateTo(new SellerCatalog());
            return;
        }

        if (_model.LoggedType == UserType.Admin)
        {
            await NavigateTo(new AdminCatalog());
            return;
        }

        await NavigateTo(new ProductCatalog());

        async Task NavigateTo(UserControl catalog)
        {
            SmartTradeNavigationManager.Instance.ReInitializeNavigation(catalog);
            int loadingScreen = _loadingScreenManager.StartLoading();
            var model = (CatalogModel)catalog.DataContext;
            await model.LoadProductsAsync();
            _catalogModel = model;

            if(_currentCategory != null)
                _catalogModel.SortByCategory(_currentCategory);

            if (SelectedButton == 0) SmartTradeNavigationManager.Instance.NavigateToWithoutSaving(catalog);
            _loadingScreenManager.StopLoading(loadingScreen);
        }
    }

    public void ShowPopUp(ContentControl popUp)
    {
        Overlay.IsVisible = true;

        PopUp.Content = popUp;
        ShowingPopUp = true;
    }

    public void HidePopUp()
    {
        Overlay.IsVisible = false;
        ShowingPopUp = false;
    }
    #endregion

    #region SearchBar

    private void AutoCompleteBox_KeyDown(object? sender, KeyEventArgs e)
    {

        if (e.Key.Equals(Key.Enter))
        {
            int loadingScreen = _loadingScreenManager.StartLoading();
            SearchResult searchResult = new SearchResult(_model.FindProducts());
            if(SelectedButton ==  loadingScreen) SmartTradeNavigationManager.Instance.NavigateToOverriding(searchResult);
            else SmartTradeNavigationManager.Instance.AddToStack(searchResult, loadingScreen);

            _searchModel = (SearchResultModel)searchResult.DataContext;
            if(_currentCategory != null)
                _searchModel?.SortByCategory(_currentCategory);

            _loadingScreenManager.StopLoading(loadingScreen);
        }
    }

    private void AutoCompleteBox_TextChanged(object? sender, TextChangedEventArgs e)
    {
        _model.SearchText = AutoCompleteBox.Text;
    }

    #endregion
}
