using Avalonia.Controls;
using SmartTradeDTOs;
using SmartTrade.Entities;
using SmartTrade.ViewModels;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Threading.Tasks;
using Avalonia.Interactivity;
using SmartTrade.Services;


namespace SmartTrade.Views
{
    public partial class ProductView : UserControl
    {
        private Bitmap? _alertActivated;
        private Bitmap? _alertDeactivated;
        private Bitmap? _wishListDeactivated;
        private PostDTO _post;
        private Bitmap? _isEco;
        private Bitmap? _isNotEco;

        private bool _isAlertActivated;
        private bool _isWishActivated;

        public ProductViewModel _model => (ProductViewModel)DataContext;

        public ProductView() 
        {
            InitializeComponent();
        }

        public ProductView(PostDTO post)
        {
            _post = post;
            DataContext = new ProductViewModel(post);
            InitializeComponent();

            _alertActivated = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/AlertSelected.png")));
            _alertDeactivated = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/Alert.png")));
            _isEco = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/IconoSostenible.png")));
            _isNotEco = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/IconoNOSostenible.png")));
            _wishListDeactivated = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/WishListHeart.png")));
            AlertImage.Source = _alertDeactivated;
            WishListImage.Source = _wishListDeactivated;
            EcoImage.Source = _isNotEco;

            NextImageButton.Click += NextImage;
            PreviousImageButton.Click += PreviousImage;
            ((ProductViewModel)DataContext).OnOfferChanged += SetImageNavigationButtonsVisibility;
            AlertToggle.Click += ToggleAlert;
            WishListToggle.Click += ToggleWishList;


            SetToggleVisibility();
            SetAlertImage();
            SetWishListImageAsync();
            SetEcoImage();
            SetImageNavigationButtonsVisibility();

            SmartTradeNavigationManager.Instance.OnNavigate += OnNavigateAsync;

            AddToCartButton.Click += AddItemToCart;
            AddToWishListButton.Click += AddItemToWishListAsync;

            AddButton.Click += OnAddButtonOnClick;
            SubtractButton.Click += OnSubtractButtonOnClick;

            if (int.TryParse(_model.Quantity, out var count))
            {
                if (count <= 1)
                {
                    SubtractButton.IsEnabled = false;
                }
            }

            AddToWishListButton.IsVisible = _model.Logged != null;
            WishListToggle.IsVisible = _model.Logged != null;
        }

        private void ToggleWishList(object? sender, RoutedEventArgs e)
        {
            if (_model.Logged == null)
            {
                return;
            }

            if (WishListToggle.IsChecked == true)
            {
                _isWishActivated = true;
            }
            else
            {
                _isWishActivated = false;
            }
        }

        private async void AddItemToWishListAsync(object? sender, RoutedEventArgs e)
        {
            await _model.AddItemToWishListAsync();
        }
        
        private void OnSubtractButtonOnClick(object? sender, RoutedEventArgs e)
        {
            if (int.TryParse(_model.Quantity, out var count))
            {
                if (count > 1)
                {
                    count--;
                    _model.Quantity = count.ToString();
                }
                
                if(count <= 1) SubtractButton.IsEnabled = false;
            }
        }

        private void OnAddButtonOnClick(object? sender, RoutedEventArgs e)
        {
            if (int.TryParse(_model.Quantity, out var count))
            {
                count++;
                _model.Quantity = count.ToString();

                if (count > 1)
                {
                    SubtractButton.IsEnabled = true;
                }
            }
        }

        private async void AddItemToCart(object? sender, RoutedEventArgs e)
        {
            await _model.AddItemToCartAsync();
            SmartTradeNavigationManager.Instance.NavigateWithButton(typeof(ShoppingCartView), 1, 1, out _, true);
        }

        private void NextImage(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ImageCarousel.Next();
            SetImageNavigationButtonsVisibility();
        }

        private void PreviousImage(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ImageCarousel.Previous();
            SetImageNavigationButtonsVisibility();
        }

        private void SetImageNavigationButtonsVisibility()
        {
            if (ImageCarousel.Items.Count > 1)
            {
                NextImageButton.IsVisible = true;
                PreviousImageButton.IsVisible = true;
            }
            else
            {
                NextImageButton.IsVisible = false;
                PreviousImageButton.IsVisible = false;
            }

            if (ImageCarousel.SelectedIndex == 0)
            {
                PreviousImageButton.IsVisible = false;
            }
            else if (ImageCarousel.SelectedIndex == ImageCarousel.Items.Count - 1)
            {
                NextImageButton.IsVisible = false;
            }
        }

        private void SetAlertImage()
        {
            if (_model.Logged == null || !(_post.Offers[0].Product.UsersWithAlertsInThisProduct.Contains(_model.Logged.Email)))
            {
                AlertToggle.IsChecked = false;
              //  AlertImage.Source = _alertDeactivated;
            }
            else
            {
                AlertToggle.IsChecked = true;
               // AlertImage.Source = _alertActivated;
            }
        }

        private void SetWishListImageAsync()
        {
            if (_model.Logged == null || !(_model.IsPostInWishes(_post)))
            {
                WishListToggle.IsChecked = false;
            }
            else
            {
                WishListToggle.IsChecked = true;
            }
        }

        private void SetEcoImage()
        {
            if (int.TryParse(_post.EcologicPrint, out int ecologicPrint) && ecologicPrint < 10)
            {
                EcoImage.Source = _isEco;
            }
            else
            {
                EcoImage.Source = _isNotEco;
            }
        }

        private void ToggleAlert(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (_model.Logged == null)
            {
                return;
            }

            if (AlertToggle.IsChecked == true)
            {
                _post.Offers[0].Product.UsersWithAlertsInThisProduct.Add(_model.Logged.Email);
                _isAlertActivated = true;
                //AlertImage.Source = _alertActivated;
            }
            else
            {
                _post.Offers[0].Product.UsersWithAlertsInThisProduct.Remove(_model.Logged.Email);
                _isAlertActivated = false;
                //AlertImage.Source = _alertDeactivated;
            }
        }

        private async void OnNavigateAsync(Type type)
        {
            if (_model.Logged == null)
            {
                return;
            }

            if (type != typeof(ProductView) && SmartTradeNavigationManager.Instance.Navigator.PreviousView.GetType() == typeof(ProductView))
            {
                if (_isAlertActivated)
                {
                    await _model.CreateAlertAsync(_post.Offers[0].Product.Id);
                }

                if (_isWishActivated)
                {
                    await _model.AddItemToWishListAsync();
                }
                else
                {
                    await _model.DeleteFromWishListAsync();
                }
            }
        }

        private void SetToggleVisibility()
        {
            if (_model.Logged == null || _model.Logged.GetUserType() != UserType.Consumer)
            {
                AlertToggle.IsVisible = false;
            }
        }
    }
}
