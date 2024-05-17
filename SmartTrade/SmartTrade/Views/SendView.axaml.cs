using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;

namespace SmartTrade.Views
{
    public partial class SendView : UserControl
    {
        private Bitmap? _starSelected { get; set; }
        private Bitmap? _starVoid { get; set; }
        private int _rating;

        public SendView()
        {
            InitializeComponent();

            RatingStar1.Click += Star1Click;
            RatingStar2.Click += Star2Click;
            RatingStar3.Click += Star3Click;
            RatingStar4.Click += Star4Click;
            RatingStar5.Click += Star5Click;


            _starSelected = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/Star.png")));
            _starVoid = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/VoidStar.png")));
        }

        private void Star1Click(object? sender, RoutedEventArgs e)
        {
            if ((bool)!RatingStar1.IsChecked)
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
                return;
            }
        }

        private void Star2Click(object? sender, RoutedEventArgs e)
        {
            if ((bool)!RatingStar2.IsChecked)
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
                return;
            }
        }

        private void Star3Click(object? sender, RoutedEventArgs e)
        {
            if ((bool)!RatingStar3.IsChecked)
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
                return;
            }
        }

        private void Star4Click(object? sender, RoutedEventArgs e)
        {
            if ((bool)!RatingStar4.IsChecked)
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
                return;
            }
        }

        private void Star5Click(object? sender, RoutedEventArgs e)
        {
            if ((bool)!RatingStar5.IsChecked)
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
                return;
            }
        }
    }
}
