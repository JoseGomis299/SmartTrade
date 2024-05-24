using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using SmartTrade.Services;
using SmartTrade.ViewModels;
using SmartTradeDTOs;
using DynamicData;

namespace SmartTrade.Views
{
    public partial class SendView : UserControl
    {
        private Bitmap? _starSelected { get; set; }
        private Bitmap? _starVoid { get; set; }
        private int _rating;
        private SendViewModel _model;
        private PurchaseModel _purchase;

        public SendView()
        {
            InitializeComponent();
        }

        public SendView(PurchaseModel purchase)
        {
            DataContext = _model = new SendViewModel(purchase);
            _purchase = purchase;
            RatingDTO? ratingData = _model.GetRatingFromPurchase(purchase);
            
            InitializeComponent();

            RatingStar1.Click += Star1Click;
            RatingStar2.Click += Star2Click;
            RatingStar3.Click += Star3Click;
            RatingStar4.Click += Star4Click;
            RatingStar5.Click += Star5Click;
            UploadRatingButton.Click += UploadRating;



            _starSelected = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/Star.png")));
            _starVoid = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/VoidStar.png")));

            InitializeRating(ratingData);

            if (purchase.CalculateState() != "Received")
            {
                RatingPanel.IsVisible = false;
            }
        }

        private void InitializeRating(RatingDTO? ratingData)
        {
            if (ratingData == null)
            {
                SetVoidStars();
                _rating = 0;
            }
            else
            {
                _rating = ratingData.Points;
                Review.Text = ratingData.Description;
                var stars = new List<Image> { Star1, Star2, Star3, Star4, Star5 };

                for (int i = 0; i < ratingData.Points; i++)
                {
                    stars[i].Source = _starSelected;
                }

                for (int i = ratingData.Points; i < 5; i++)
                {
                    stars[i].Source = _starVoid;
                }
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

        private void UploadRating(object? sender, RoutedEventArgs e)
        {
            if (_rating == 0 || !Review.Text.IsNullOrEmpty())
            {
                SmartTradeService.Instance.CreateRatingAsync(_purchase.Post, _rating, Review.Text);

                SetVoidStars();
                Review.Text = "";
            }
        }

        #endregion
    }
}
