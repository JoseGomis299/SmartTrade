using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Avalonia;
using Avalonia.Controls;
using SmartTrade.ViewModels;

namespace SmartTrade.Controls
{
    public partial class ST_Carousel : UserControl
    {
        private bool _subtitleVisibility;

        public ST_Carousel()
        {
            InitializeComponent();

            SubtitleTextBlock.IsVisible = false;
        }

        public ObservableCollection<ProductModel>? DataSource
        {
            get => GetValue(DataSourceProperty);
            set => SetValue(DataSourceProperty, value);
        }

        public static readonly StyledProperty<ObservableCollection<ProductModel>?> DataSourceProperty =
            AvaloniaProperty.Register<ST_Carousel, ObservableCollection<ProductModel>?>(nameof(DataSource));

        public string? Title
        {
            get => GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly StyledProperty<string?> TitleProperty =
            AvaloniaProperty.Register<ST_Carousel, string?>(nameof(Title));

        public string? Subtitle
        {
            get => GetValue(SubtitleProperty);
            set => SetValue(SubtitleProperty, value);
        }

        public static readonly StyledProperty<string?> SubtitleProperty =
            AvaloniaProperty.Register<ST_Carousel, string?>(nameof(Subtitle));

        private void Hide()
        {
           TitleTextBlock.IsVisible = false;
           SubtitleTextBlock.IsVisible = false;
           Carousel.IsVisible = false;
        }

        private void Show()
        {
            TitleTextBlock.IsVisible = true;
            SubtitleTextBlock.IsVisible = _subtitleVisibility;
            Carousel.IsVisible = true;
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            if (change.Property == DataSourceProperty)
            {
                if (DataSource.Count == 0)
                {
                    Hide();

                    DataSource.CollectionChanged += (sender, args) =>
                    {
                        if (DataSource.Count > 0)
                        {
                            Show();
                        }
                    };
                }
                else
                {
                    Show();
                }
            }else if (change.Property == SubtitleProperty)
            {
                if (TitleTextBlock.IsVisible && !string.IsNullOrEmpty(Subtitle))
                {
                    _subtitleVisibility = true;
                    SubtitleTextBlock.IsVisible = true;
                }
                else
                {
                    _subtitleVisibility = false;
                    SubtitleTextBlock.IsVisible = false;
                }
            }
            base.OnPropertyChanged(change);
        }
    }
}
