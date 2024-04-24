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
        private PostDTO _post;
        private Bitmap? _isEco;
        private Bitmap? _isNotEco;

        private bool _isAlertActivated;

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
            AlertImage.Source = _alertDeactivated;
            EcoImage.Source = _isNotEco;

            NextImageButton.Click += NextImage;
            PreviousImageButton.Click += PreviousImage;
            ((ProductViewModel)DataContext).OnOfferChanged += SetImageNavigationButtonsVisibility;
            AlertToggle.Click += ToggleAlert;


            SetToggleVisibility();
            SetAlertImage();
            SetEcoImage();
            SetImageNavigationButtonsVisibility();

            SmartTradeNavigationManager.Instance.OnNavigate += OnNavigateAsync;

            AddToCartButton.Click += AddItemToCart;
        }

        private void AddItemToCart(object? sender, RoutedEventArgs e)
        {
            _model.AddItemToCart();
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

            if (_isAlertActivated && type != typeof(ProductView) && SmartTradeNavigationManager.Instance.Navigator.PreviousView.GetType() == typeof(ProductView))
            {
                await _model.CreateAlertAsync(_post.Offers[0].Product.Id);
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
