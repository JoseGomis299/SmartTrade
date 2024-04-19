using Avalonia.Controls;
using System;
using Avalonia.Input;
using Avalonia.Interactivity;
using SmartTrade.ViewModels;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System.Threading.Tasks;

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
    private Bitmap? _addToCart;
    private Bitmap? _alertImage;

    int _selectedButton = 0;

    bool _isLoadingHome = false;
    bool _isLoadingCart = false;
    bool _isLoadingUser = false;
    
    private int? CurrentCategory;
    public event Action<int?> OnCategorySelected;

    public bool ShowingPopUp { get; set; }

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

        SideBarButton.Click += SideBarButton_Click;
        SideBar.PaneClosing += SideBar_PaneClosing;
        ListBoxDepartments.SelectionChanged += ListBoxDepartments_SelectionChanged;
       
        AlertButton.Click += OnAlertButtonOnClick;

        ProfileButton.Click += OnProfileButtonOnClick;
        HomeButton.Click += OnHomeButtonOnClick;
        HomeButton2.Click += OnHomeButtonOnClick;
        ShoppingCartButton.Click += OnShoppingCartButtonOnClick;
        ShoppingCartButton2.Click += OnShoppingCartButtonOnClick;

        _homeImage = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/Home.png")));
        _userImage = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/User.png")));
        _cartImage = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/Cart.png")));
        _cartImageSelected = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/CartSelected.png")));
        _userImageSelected = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/UserSelected.png")));
        _homeImageSelected = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/HomeSelected.png")));
        _alertImage = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/Alert.png")));

        HomeImage.Source = _homeImageSelected;
        UserImage.Source = _userImage;
        CartImage.Source = _cartImage;
        CartImage2.Source = _cartImage;
        HomeImage2.Source = _homeImage;
        AlertImage.Source = _alertImage;

        SetButtonVisibility();

        SmartTradeNavigationManager.Instance.MainView = this;
    }

    private void SetButtonVisibility()
    {
        if (SmartTradeService.Instance.Logged != null && (SmartTradeService.Instance.Logged.IsAdmin || SmartTradeService.Instance.Logged.IsSeller))
        {
            ShoppingCartButton.IsVisible = false;
            ShoppingCartButton2.IsVisible = false;
            AddToCartButton.IsVisible = false;
            AlertButton.IsVisible = false;
        }
        else if (SmartTradeService.Instance.Logged != null)
        {
            ShoppingCartButton.IsVisible = true;
            ShoppingCartButton2.IsVisible = true;
            AddToCartButton.IsVisible = true;
            AlertButton.IsVisible = true;
        }
        else
        {
            ShoppingCartButton.IsVisible = true;
            ShoppingCartButton2.IsVisible = true;
            AddToCartButton.IsVisible = true;
            AlertButton.IsVisible = false;
        }
    }

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
        CurrentCategory = ListBoxDepartments.SelectedIndex;
        SideBar.IsPaneOpen = false;

        if (ListBoxDepartments.SelectedIndex == -1)
        {
            CurrentCategory = null;
        }

        OnCategorySelected?.Invoke(CurrentCategory);
    }

    #endregion

    #region Buttons

    private void OnShoppingCartButtonOnClick(object? sender, RoutedEventArgs e)
    {
        if (_isLoadingCart)
        {
            SmartTradeNavigationManager.Instance.NavigateWithButton(null, _selectedButton, 1, out _);
            ShowLoadingScreen();
            return;
        }

        HideLoadingScreen();
        SmartTradeNavigationManager.Instance.NavigateWithButton(typeof(ShoppingCartView), _selectedButton, 1, out _);
    }

    private async void OnHomeButtonOnClick(object? sender, RoutedEventArgs e)
    {
        await ShowCatalogAsync();
    }

    private void OnProfileButtonOnClick(object? sender, RoutedEventArgs e)
    {
        if (_isLoadingUser)
        {
            SmartTradeNavigationManager.Instance.NavigateWithButton(null, _selectedButton, 2, out _);
            ShowLoadingScreen();
            return;
        }

        HideLoadingScreen();
        if (SmartTradeService.Instance.Logged == null)
            SmartTradeNavigationManager.Instance.NavigateWithButton(typeof(Login), _selectedButton, 2, out _);
        else SmartTradeNavigationManager.Instance.NavigateWithButton(typeof(Profile), _selectedButton, 2, out _);
    }

    public async Task ShowCatalogAsync()
    {
        if (_isLoadingHome)
        {
            SmartTradeNavigationManager.Instance.NavigateWithButton(null, _selectedButton, 0, out _);
            ShowLoadingScreen();
            return;
        }

        HideLoadingScreen();
        if (SmartTradeService.Instance.Logged == null)
        {
            await NavigateTo(typeof(ProductCatalog));
            return;
        }

        if (SmartTradeService.Instance.Logged.IsSeller)
        {
            await NavigateTo(typeof(SellerCatalog));
            return;
        }

        if (SmartTradeService.Instance.Logged.IsAdmin)
        {
            await NavigateTo(typeof(AdminCatalog));
            return;
        }

        await NavigateTo(typeof(ProductCatalog));

        async Task NavigateTo(Type catalogType)
        {
            if (_selectedButton != 0 || (_selectedButton == 0 && SmartTradeNavigationManager.Instance.Navigator.CurrentView.GetType() != catalogType))
            {
                SmartTradeNavigationManager.Instance.NavigateWithButton(catalogType, _selectedButton, 0, out _);
                return;
            }

            if (SmartTradeNavigationManager.Instance.NavigateWithButton(catalogType, _selectedButton, 0, out var catalog, true))
            {
                int loadingScreen = StartLoading();
                var model = (CatalogModel)catalog.DataContext;
                await model.LoadProductsAsync();

                OnCategorySelected -= model.SortByCategory;
                OnCategorySelected += model.SortByCategory;
                OnCategorySelected?.Invoke(CurrentCategory);
 

                if (_selectedButton == 0) SmartTradeNavigationManager.Instance.NavigateToWithoutSaving(catalog);
                StopLoading(loadingScreen);
            }
        }
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

        HomeImage2.Source = HomeImage.Source;
        CartImage2.Source = CartImage.Source;
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

        if (_selectedButton == 2)
        {
            SearchBar.IsVisible = false;
            return;
        }

        if (type == typeof(ProductView))
        {
            BottomBar.IsVisible = false;
            ShoppingCart.IsVisible = true;
            return;
        }

        ResetVisibility();
    }
    private void ResetVisibility()
    {
        SearchBar.IsVisible = true;
        BottomBar.IsVisible = true;
        ShoppingCart.IsVisible = false;

    }

    private int StartLoading()
    {
        ShowLoadingScreen();

        if (_selectedButton == 0)  
            _isLoadingHome = true;
        else if(_selectedButton == 1)
            _isLoadingCart = true;
        else if(_selectedButton == 2)
            _isLoadingUser = true;

        return _selectedButton;
    }

    private void ShowLoadingScreen()
    {
        Loading.IsVisible = true;
        ViewContent.IsVisible = false;
    }

    private void HideLoadingScreen()
    {
        Loading.IsVisible = false;
        ViewContent.IsVisible = true;
    }

    private void StopLoading(int i)
    {
        if (i == 0)
            _isLoadingHome = false;
        else if (i == 1)
            _isLoadingCart = false;
        else if (i == 2)
            _isLoadingUser = false;

        HideLoadingScreen();
    }

    public async Task ShowCatalogReinitializingAsync()
    {
        //if (_isLoadingHome)
        //{
        //    SmartTradeNavigationManager.Instance.NavigateWithButton(null, _selectedButton, 0, out _);
        //    ShowLoadingScreen();
        //    return;
        //}

        SetButtonVisibility();
        HideLoadingScreen();
        if (SmartTradeService.Instance.Logged == null || SmartTradeService.Instance.Logged.IsConsumer)
        {
            await NavigateTo(new ProductCatalog());
            return;
        }

        if (SmartTradeService.Instance.Logged.IsSeller)
        {
            await NavigateTo(new SellerCatalog());
            return;
        }

        if (SmartTradeService.Instance.Logged.IsAdmin)
        {
            await NavigateTo(new AdminCatalog());
            return;
        }

        async Task NavigateTo(UserControl catalog)
        {
            SmartTradeNavigationManager.Instance.ReInitializeNavigation(catalog);
            int loadingScreen = StartLoading();
            var model = (CatalogModel)catalog.DataContext;
            await model.LoadProductsAsync();

            OnCategorySelected -= model.SortByCategory;
            OnCategorySelected += model.SortByCategory;
            OnCategorySelected?.Invoke(CurrentCategory);

            if (_selectedButton == 0) SmartTradeNavigationManager.Instance.NavigateToWithoutSaving(catalog);
            StopLoading(loadingScreen);
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

    private async void AutoCompleteBox_KeyDown(object? sender, KeyEventArgs e)
    {

        if (e.Key.Equals(Key.Enter))
        {
            int loadingScreen = StartLoading();
            SearchResult searchResult = new SearchResult(await _model.LoadProductsAsync());
            if(_selectedButton ==  loadingScreen) SmartTradeNavigationManager.Instance.NavigateToOverriding(searchResult);
            else SmartTradeNavigationManager.Instance.AddToStack(searchResult, loadingScreen);

            var search = (SearchResultModel)searchResult.DataContext;
            OnCategorySelected -= search.SortByCategory;
            OnCategorySelected += search.SortByCategory;
            OnCategorySelected?.Invoke(CurrentCategory);

            StopLoading(loadingScreen);
        }
    }

    private void AutoCompleteBox_TextChanged(object? sender, TextChangedEventArgs e)
    {
        _model.SearchText = AutoCompleteBox.Text;
    }

    #endregion
}
