using Avalonia.Controls;
using SmartTrade.Entities;
using SmartTrade.ViewModels;

namespace SmartTrade.Views
{
    public partial class CheckOutView : UserControl
    {
        public CheckOutView()
        {
            DataContext = new CheckoutModel();
            InitializeComponent();
        }

        public CheckOutView(CheckOutData data)
        {
            DataContext = new CheckoutModel(data);
            InitializeComponent();

            BackButton.Click += Back;
            CompleteOrderButton.Click += CompleteOrder;
        }

        private void Back(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.NavigateTo(typeof(SelectPaymentView));
        }

        private async void CompleteOrder(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            await ((CheckoutModel)DataContext).CompleteOrder();
        }
    }
}
