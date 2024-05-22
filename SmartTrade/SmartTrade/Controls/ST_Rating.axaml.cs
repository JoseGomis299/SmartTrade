using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using SmartTrade.ViewModels;

namespace SmartTrade.Controls
{
    public partial class ST_Rating : UserControl
    {

        public string? Description
        {
            get => GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public float Rating
        {
            get => GetValue(RatingProperty);
            set => SetValue(RatingProperty, value);
        }

        public string Points
        {
            get => GetValue(PointsProperty);
            set => SetValue(PointsProperty, value);
        }

        public string User
        {
            get => GetValue(UserProperty);
            set => SetValue(UserProperty, value);
        }

        public static readonly StyledProperty<string?> DescriptionProperty =
         AvaloniaProperty.Register<ST_Rating, string?>(nameof(Description));

        public static readonly StyledProperty<float> RatingProperty =
            AvaloniaProperty.Register<ST_Rating, float>(nameof(Rating));

        public static readonly StyledProperty<string> PointsProperty = 
            AvaloniaProperty.Register<ST_Rating, string>(nameof(Points));

        public static readonly StyledProperty<string> UserProperty =
            AvaloniaProperty.Register<ST_Rating, string>(nameof(User));

        private Bitmap? _fullStar;
        private Bitmap? _halfStar;
        private Bitmap? _emptyStar;

        private List<Image> _stars;

        public ST_Rating()
        {
            InitializeComponent();

            _fullStar = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/Star.png")));
            _halfStar = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/HalfStar.png")));
            _emptyStar = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/VoidStar.png")));

            _stars = new List<Image>() { Star1, Star2, Star3, Star4, Star5 };
        }

        override protected void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);

            if (change.Property == RatingProperty)
            {
                float rating = (float)change.NewValue;
                var fullStars = (int)rating;
                var halfStar = rating - fullStars > 0.5;

                for (int i = 0; i < fullStars; i++)
                {
                    _stars[i].Source = _fullStar;
                }

                if (halfStar)
                {
                    _stars[fullStars].Source = _halfStar;
                }

                for (int i = fullStars + (halfStar ? 1 : 0); i < 5; i++)
                {
                    _stars[i].Source = _emptyStar;
                }

                Points = rating + "/" + 5;
            }
        }

  

    }
}
