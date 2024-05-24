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
using Avalonia;
using DynamicData;


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
        private Bitmap? _starSelected;
        private Bitmap? _halfStar;

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
            _starSelected = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/Star.png")));
            _halfStar = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/HalfStar.png")));
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
            AddToGiftsButton.IsVisible = _model.Logged != null && _model.Logged.GetUserType() != UserType.Seller;
            SetStars();
        }

        private void SetStars()
        {   
            var stars = new List<Image> { Star1, Star2, Star3, Star4, Star5 };

            float rating = _post.AveragePoints;
            var fullStars = (int)rating;
            var halfStar = rating - fullStars >= 0.5;

            for (int i = 0; i < fullStars; i++)
            {
                stars[i].Source = _starSelected;
            }

            if (halfStar)
            {
                stars[fullStars].Source = _halfStar;
            }
        }

        protected override void Refresh()
        {
            if (_model.Logged is null || _model.Logged.IsConsumer)
            {
                SetAlertImage();
                SetWishListImage();
            }

            SetToggleVisibility();
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
            await AddGift(giftListName);
            var view = new GiftsView();
            view.SetIndexOnList(giftListName);
            SmartTradeNavigationManager.Instance.NavigateTo(view);
        }

        private void AddToGiftsButtonOnClick(object? sender, RoutedEventArgs e)
        {
            List<String> giftListNames = _model.GetGiftListNames();
            if (giftListNames.Count == 0)
            {
                SmartTradeNavigationManager.Instance.MainView.ShowPopUp(new AddGiftListView(new GiftsView()));
            }

            giftListNames = _model.GetGiftListNames();

            if (giftListNames.Count == 0)
            {
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

        private void SetWishListImage()
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
                WishListToggle.IsVisible = false;
            }
        }
    }
}
