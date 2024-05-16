using Avalonia.Controls;
using SmartTrade.Entities;
using SmartTrade.ViewModels;

namespace SmartTrade.Views
{
    public partial class SelectPaymentView : UserControl
    {
        private SelectPaymentMethodModel _model;
        private Address _selectedAddress;
        private Address _selectedBillingAddress;

        public SelectPaymentView()
        {
            DataContext = _model = new SelectPaymentMethodModel();
            InitializeComponent();

            AddBizumButton.Click += AddBizum;
            AddCreditCardButton.Click += AddCreditCard;
            AddPaypalButton.Click += AddPaypal;
            NextButton.Click += Next;
            BackButton.Click += Back;
        }

        public SelectPaymentView(Address selectedAddress, Address selectedBillingAddress) : this()
        {
            _selectedAddress = selectedAddress;
            _selectedBillingAddress = selectedBillingAddress;
        }

        public void SelectAddresses(Address selectedAddress, Address selectedBillingAddress)
        {
            _selectedAddress = selectedAddress;
            _selectedBillingAddress = selectedBillingAddress;
        }

        private void AddBizum(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            AddBizumPopup popup = new AddBizumPopup();
            popup.onAccept = async (save) =>
            {
                await _model.AddBizumAsync(popup.GetBizum(), save);
            };

            SmartTradeNavigationManager.Instance.MainView.ShowPopUp(popup);
        }

        private void AddCreditCard(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            AddCreditCardPopup popup = new AddCreditCardPopup();
            popup.onAccept = async (save) =>
            {
                await _model.AddCreditCardAsync(popup.GetCreditCard(), save);
            };

            SmartTradeNavigationManager.Instance.MainView.ShowPopUp(popup);
        }

        private void AddPaypal(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            AddPaypalPopup popup = new AddPaypalPopup();
            popup.onAccept = async (save) =>
            {
                await _model.AddPaypalAsync(popup.GetPaypal(), save);
            };

            SmartTradeNavigationManager.Instance.MainView.ShowPopUp(popup);
        }

        private void Next(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (_model.SelectedBizum == null && _model.SelectedCreditCard == null && _model.SelectedPaypal == null)
            {
                SelectAPaymentMethodText.IsVisible = true;
                return;
            }
            
            SelectAPaymentMethodText.IsVisible = false;
            SmartTradeNavigationManager.Instance.NavigateTo(new CheckOutView(_selectedAddress, _selectedBillingAddress, _model.SelectedBizum, _model.SelectedCreditCard, _model.SelectedPaypal));
        }

        private void Back(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.NavigateTo(typeof(SelectAddressView));
        }
    }
}
