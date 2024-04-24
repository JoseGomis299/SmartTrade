using Avalonia;
using Avalonia.Controls;

namespace SmartTrade.Controls
{
    public partial class ST_Count : UserControl
    {
        public ST_Count()
        {
            InitializeComponent();

            AddButton.Click += (sender, e) =>
            {
                if (int.TryParse(Count, out var count))
                {
                    count++;
                    Count = count.ToString();
                }
            };

            SubtractButton.Click += (sender, e) =>
            {
                if (int.TryParse(Count, out var count))
                {
                    if (count > 0)
                    {
                        count--;
                        Count = count.ToString();
                    }
                }
            };
        }

        public string? Count
        {
            get => GetValue(CountProperty);
            set => SetValue(CountProperty, value);
        }

        public static readonly StyledProperty<string?> CountProperty =
            AvaloniaProperty.Register<ST_Count, string?>(nameof(Count));
    }
}
