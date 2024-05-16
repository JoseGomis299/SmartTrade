using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Microsoft.IdentityModel.Tokens;

namespace SmartTrade.Controls
{
    public partial class ST_OrderItem : UserControl
    {
        public ST_OrderItem()
        {
            InitializeComponent();
        }

        public ICommand Command
        {
            get => GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly StyledProperty<ICommand> CommandProperty =
            AvaloniaProperty.Register<ST_Product, ICommand>(nameof(Command));

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

        public string? DeliveryState
        {
            get => GetValue(DeliveryStateProperty);
            set => SetValue(DeliveryStateProperty, value);
        }

        public static readonly StyledProperty<string?> DeliveryStateProperty =
            AvaloniaProperty.Register<ST_Product, string?>(nameof(DeliveryState));
    }
}
