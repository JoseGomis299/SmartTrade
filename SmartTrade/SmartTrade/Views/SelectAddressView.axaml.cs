using Avalonia.Controls;
using SmartTrade.ViewModels;

namespace SmartTrade.Views
{
    public partial class SelectAddressView : UserControl
    {
        private SelectAddressModel _model;

        public SelectAddressView()
        {
            DataContext = _model = new SelectAddressModel();
            InitializeComponent();

            AddAddressButton.Click += AddAddress;
            NextButton.Click += Next;
            BackButton.Click += Back;
        }

        private void AddAddress(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            AddAddress popup = new AddAddress();
            popup.onAccept = async (save) =>
            {
                await _model.AddAddressAsync(popup.GetAddress(), save);
            };

            SmartTradeNavigationManager.Instance.MainView.ShowPopUp(popup);
        }

        private void Next(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.NavigateTo(new SelectPaymentView(_model.SelectedAddress, _model.SelectedBillingAddress));
        }

        private void Back(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.NavigateTo(new ShoppingCartView());
        }
    }
}
