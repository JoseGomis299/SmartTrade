using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using SmartTrade.Services;
using SmartTrade.ViewModels;

namespace SmartTrade.Views
{
    public partial class SendView : UserControl
    {
        private Bitmap? _starSelected { get; set; }
        private Bitmap? _starVoid { get; set; }
        private int _rating;
        private SendViewModel _model;

        public SendView()
        {
            InitializeComponent();
        }

        public SendView(PurchaseModel purchase)
        {
            InitializeComponent();

            RatingStar1.Click += Star1Click;
            RatingStar2.Click += Star2Click;
            RatingStar3.Click += Star3Click;
            RatingStar4.Click += Star4Click;
            RatingStar5.Click += Star5Click;

            DataContext = _model = new SendViewModel(purchase);


            _starSelected = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/Star.png")));
            _starVoid = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/VoidStar.png")));

            SetVoidStars();
            _rating = 0;

            if (purchase.CalculateState() != "Received")
            {
                RatingPanel.IsVisible = false;
            }
        }

        #region Rating

        private void SetVoidStars()
        {
            Star1.Source = _starVoid;
            Star2.Source = _starVoid;
            Star3.Source = _starVoid;
            Star4.Source = _starVoid;
            Star5.Source = _starVoid;
        }

        private void Star1Click(object? sender, RoutedEventArgs e)
        {
            if (Star1.Source == _starVoid || Star2.Source == _starSelected)
            {
                Star1.Source = _starSelected;
                Star2.Source = _starVoid;
                Star3.Source = _starVoid;
                Star4.Source = _starVoid;
                Star5.Source = _starVoid;

                _rating = 1;
            }
            else
            {
                Star1.Source = _starVoid;

                _rating = 0;
            }
        }

        private void Star2Click(object? sender, RoutedEventArgs e)
        {
            if (Star2.Source == _starVoid || Star3.Source == _starSelected)
            {
                Star1.Source = _starSelected;
                Star2.Source = _starSelected;
                Star3.Source = _starVoid;
                Star4.Source = _starVoid;
                Star5.Source = _starVoid;

                _rating = 2;
            }
            else
            {
                Star2.Source = _starVoid;

                _rating = 1;
            }
        }

        private void Star3Click(object? sender, RoutedEventArgs e)
        {
            if (Star3.Source == _starVoid || Star4.Source == _starSelected)
            {
                Star1.Source = _starSelected;
                Star2.Source = _starSelected;
                Star3.Source = _starSelected;
                Star4.Source = _starVoid;
                Star5.Source = _starVoid;

                _rating = 3;
            }
            else
            {
                Star3.Source = _starVoid;

                _rating = 2;
            }
        }

        private void Star4Click(object? sender, RoutedEventArgs e)
        {
            if (Star4.Source == _starVoid || Star5.Source == _starSelected)
            {
                Star1.Source = _starSelected;
                Star2.Source = _starSelected;
                Star3.Source = _starSelected;
                Star4.Source = _starSelected;
                Star5.Source = _starVoid;

                _rating = 4;
            }
            else
            {
                Star4.Source = _starVoid;

                _rating = 3;
            }
        }

        private void Star5Click(object? sender, RoutedEventArgs e)
        {
            if (Star5.Source == _starVoid)
            {
                Star1.Source = _starSelected;
                Star2.Source = _starSelected;
                Star3.Source = _starSelected;
                Star4.Source = _starSelected;
                Star5.Source = _starSelected;

                _rating = 5;
            }
            else
            {
                Star5.Source = _starVoid;

                _rating = 4;
            }
        }

        #endregion
    }
}
