using Avalonia.Controls;
using SmartTrade;
using SmartTradeDTOs;
using SmartTrade.Entities;
using SmartTrade.ViewModels;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Threading.Tasks;


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
            if (SmartTradeService.Instance.Logged == null || !(_post.Offers[0].Product.UsersWithAlertsInThisProduct.Contains(SmartTradeService.Instance.Logged.Email)))
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
            if (SmartTradeService.Instance.Logged == null)
            {
                return;
            }

            if (AlertToggle.IsChecked == true)
            {
                _post.Offers[0].Product.UsersWithAlertsInThisProduct.Add(SmartTradeService.Instance.Logged.Email);
                _isAlertActivated = true;
                //AlertImage.Source = _alertActivated;
            }
            else
            {
                _post.Offers[0].Product.UsersWithAlertsInThisProduct.Remove(SmartTradeService.Instance.Logged.Email);
                _isAlertActivated = false;
                //AlertImage.Source = _alertDeactivated;
            }
        }

        private async void OnNavigateAsync(Type type)
        {
            if (SmartTradeService.Instance.Logged == null)
            {
                return;
            }

            if (_isAlertActivated && type != typeof(ProductView) && SmartTradeNavigationManager.Instance.Navigator.PreviousView.GetType() == typeof(ProductView))
            {
                await SmartTradeService.Instance.CreateAlertAsync(_post.Offers[0].Product.Id);
            }
        }

        private void SetToggleVisibility()
        {
            if (SmartTradeService.Instance.Logged == null || (SmartTradeService.Instance.Logged.IsAdmin || SmartTradeService.Instance.Logged.IsSeller))
            {
                AlertToggle.IsVisible = false;
            }
        }
    }
}
