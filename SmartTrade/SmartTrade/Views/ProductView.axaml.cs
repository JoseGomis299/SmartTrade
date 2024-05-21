using Avalonia.Controls;
using SmartTradeDTOs;
using SmartTrade.Entities;
using SmartTrade.ViewModels;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Avalonia.Interactivity;
using SmartTrade.Services;


namespace SmartTrade.Views
{
    public partial class ProductView : RefreshableUserControl
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

            AddToGiftsButton.Click += AddToGiftsButtonOnClick;

            AddToCartButton.Click += AddItemToCart;
            AddButton.Click += OnAddButtonOnClick;
            SubtractButton.Click += OnSubtractButtonOnClick;
            EditPostButton.Click += (_, _) =>
            {
                SmartTradeNavigationManager.Instance.NavigateTo(new ValidatePost(_post));
            };

            if (int.TryParse(_model.Quantity, out var count))
            {
                if (count <= 1)
                {
                    SubtractButton.IsEnabled = false;
                }
            }

            WishListToggle.IsVisible = _model.Logged != null;
            SellerPanel.IsVisible = _model.Logged != null && _model.Logged.GetUserType() == UserType.Seller;
            AddToCartPanel.IsVisible = _model.Logged == null || _model.Logged.GetUserType() != UserType.Seller;
            AddToGiftsButton.IsVisible = _model.Logged != null && _model.Logged.GetUserType() != UserType.Seller; ;
        }

        protected override void Refresh()
        {
            SetToggleVisibility();
            SetAlertImage();
            SetWishListImageAsync();
            SetEcoImage();
            SetImageNavigationButtonsVisibility();
        }

        private async void ToggleWishList(object? sender, RoutedEventArgs e)
        {
            if (_model.Logged == null)
            {
                return;
            }

            if (WishListToggle.IsChecked == true)
            {
                _isWishActivated = true;
                await _model.AddItemToWishListAsync();
            }
            else
            {
                _isWishActivated = false;
                await _model.DeleteFromWishListAsync();
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

        private async Task AddGift(string giftListName)
        {
            await _model.AddGiftAsync(giftListName);
        }

        public async Task AddGiftPopup(string giftListName)
        {
            await this.AddGift(giftListName);
            SmartTradeNavigationManager.Instance.NavigateWithButton(typeof(GiftsView), 1, 1, out _, true);
        }

        private void AddToGiftsButtonOnClick(object? sender, RoutedEventArgs e)
        {
            List<String> giftListNames = _model.GetGiftListNames();
            if (giftListNames.Count == 0)
            {
                GiftsButtonErrorText.Text = "Make a gift list first";
                return;
            }
            SmartTradeNavigationManager.Instance.MainView.ShowPopUp(new AddGiftView(this, giftListNames));
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
            if (_model.Logged == null || !(_model.IsPostInAlert(_post.ProductName)))
            {
                AlertToggle.IsChecked = false;
            }
            else
            {
                AlertToggle.IsChecked = true;
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
                _model.AssignAlertToProduct(_post.ProductName);
                _isAlertActivated = true;
            }
            else
            {
                _model.UnAssignAlertToProduct(_post.ProductName);
                _isAlertActivated = false;
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
