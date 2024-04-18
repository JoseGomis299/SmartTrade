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
        private Bitmap? _alertImage;

        public ProductView() 
        {
            InitializeComponent();
        }

        public ProductView(PostDTO post)
        {
            DataContext = new ProductViewModel(post);
            InitializeComponent();

            _alertImage = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/Alert.png")));
            AlertImage.Source = _alertImage;
        }
    }
}
