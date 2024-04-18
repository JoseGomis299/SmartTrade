using Avalonia.Controls;
using SmartTrade;
using SmartTradeDTOs;
using SmartTrade.Entities;
using SmartTrade.ViewModels;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;


namespace SmartTrade.Views
{
    public partial class ProductView : UserControl
    {
        private Bitmap? _alertActivated;
        private Bitmap? _alertDeactivated;
        private PostDTO _post;

        public ProductView() 
        {
            InitializeComponent();
        }

        public ProductView(PostDTO post)
        {
            DataContext = new ProductViewModel(post);
            InitializeComponent();

            _alertActivated = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/AlertSelected.png")));
            _alertDeactivated = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/Alert.png")));

            SetToggleVisibility();
            SetAlertImage();
        }

        private void SetAlertImage()
        {
            if (SmartTradeService.Instance.Logged == null /*|| _post.Offers[0].Product.UsersWithAlertsInThisProduct[0] != SmartTradeService.Instance.Logged.Name*/)
            {
                AlertToggle.IsChecked = false;
                AlertImage.Source = _alertDeactivated;
            }
            else
            {
                AlertToggle.IsChecked = true;
                AlertImage.Source = _alertActivated;
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
