using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Microsoft.IdentityModel.Tokens;

namespace SmartTrade.Controls
{
    public partial class ST_CartProduct : UserControl
    {
        public ST_CartProduct()
        {
            InitializeComponent();

            AddButton.Click += (sender, e) =>
            {
                if (int.TryParse(Count, out var count))
                {
                    count++;
                    Count = count.ToString();

                    if (count > 1)
                    {
                        SubtractButton.IsEnabled = true;
                    }
                }
            };

            SubtractButton.Click += (sender, e) =>
            {
                if (int.TryParse(Count, out var count))
                {
                    if (count > 1)
                    {
                        count--;
                        Count = count.ToString();
                    }

                    if (count <= 1) SubtractButton.IsEnabled = false;
                }
            };

            if (int.TryParse(Count, out var count))
            {
                if (count <= 1)
                {
                    SubtractButton.IsEnabled = false;
                }
            }

            if (EstimatedTime.IsNullOrEmpty())
            {
                ReceiveTextBlock.IsVisible = false;
            }
            else
            {
                ReceiveTextBlock.IsVisible = true;
            }
        }

        public ICommand Command
        {
            get => GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public ICommand DeleteElement
        {
            get => GetValue(DeleteElementProperty);
            set => SetValue(DeleteElementProperty, value);
        }

        public static readonly StyledProperty<ICommand> CommandProperty =
            AvaloniaProperty.Register<ST_Product, ICommand>(nameof(Command));

        public static readonly StyledProperty<ICommand> DeleteElementProperty =
           AvaloniaProperty.Register<ST_Product, ICommand>(nameof(DeleteElement));

        public string? Price
        {
            get => GetValue(PriceProperty);
            set => SetValue(PriceProperty, value);
        }

        public static readonly StyledProperty<string?> PriceProperty =
            AvaloniaProperty.Register<ST_Product, string?>(nameof(Price));

        public string? ShippingCost
        {
            get => GetValue(ShippingCostProperty);
            set => SetValue(ShippingCostProperty, value);
        }

        public static readonly StyledProperty<string?> ShippingCostProperty =
            AvaloniaProperty.Register<ST_Product, string?>(nameof(Price));

        public string? ProductName
        {
            get => GetValue(ProductNameProperty);
            set => SetValue(ProductNameProperty, value);
        }

        public static readonly StyledProperty<string?> ProductNameProperty =
            AvaloniaProperty.Register<ST_Product, string?>(nameof(ProductName));

        public Bitmap? Image
        {
            get => GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        public static readonly StyledProperty<Bitmap?> ImageProperty =
            AvaloniaProperty.Register<ST_Product, Bitmap?>(nameof(Image));

        public string? Count
        {
            get => GetValue(CountProperty);
            set => SetValue(CountProperty, value);
        }

        public static readonly StyledProperty<string?> CountProperty =
            AvaloniaProperty.Register<ST_Product, string?>(nameof(Count));

        public string? EstimatedTime
        {
            get => GetValue(EstimatedTimeProperty);
            set => SetValue(EstimatedTimeProperty, value);
        }

        public static readonly StyledProperty<string?> EstimatedTimeProperty =
            AvaloniaProperty.Register<ST_Product, string?>(nameof(EstimatedTime));

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            if (change.Property == ProductNameProperty)
            {
                if (ProductName.IsNullOrEmpty())
                {
                    ProductNameTextBlock.IsVisible = false;
                }
                else
                {
                    ProductNameTextBlock.IsVisible = true;
                }
            }

            if (change.Property == EstimatedTimeProperty)
            {
                if (EstimatedTime.IsNullOrEmpty())
                {
                    ReceiveTextBlock.IsVisible = false;
                }
                else
                {
                    ReceiveTextBlock.IsVisible = true;
                }
            }
            base.OnPropertyChanged(change);
        }
    }
}
