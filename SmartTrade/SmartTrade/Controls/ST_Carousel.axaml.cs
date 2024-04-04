using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using SmartTrade.ViewModels;

namespace SmartTrade.Controls
{
    public partial class ST_Carousel : UserControl
    {
        public ST_Carousel()
        {
            InitializeComponent();
        }

        public ICollection<ProductModel> DataSource
        {
            get => GetValue(DataSourceProperty);
            set => SetValue(DataSourceProperty, value);
        } 

        public static readonly StyledProperty<ICollection<ProductModel>> DataSourceProperty =
            AvaloniaProperty.Register<ST_Carousel, ICollection<ProductModel>>(nameof(DataSource));

        public string? Title
        {
            get => GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly StyledProperty<string?> TitleProperty =
            AvaloniaProperty.Register<ST_Carousel, string?>(nameof(Title));
    }
}
