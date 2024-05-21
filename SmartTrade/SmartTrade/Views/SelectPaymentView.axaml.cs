using Avalonia.Controls;
using SmartTrade.Entities;
using SmartTrade.ViewModels;

namespace SmartTrade.Views
{
    public partial class SelectPaymentView : UserControl
    {
        private SelectPaymentMethodModel _model;
        private CheckOutData _checkOutData;
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

        public void SelectAddresses(Address selectedAddress, Address selectedBillingAddress)
        {
            _checkOutData = new CheckOutData(selectedAddress, selectedBillingAddress);
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

            _checkOutData.PaymentMethod = _model.SelectedPaymentMethod;
            SmartTradeNavigationManager.Instance.NavigateTo(new CheckOutView(_checkOutData));
        }

        private void Back(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.NavigateTo(typeof(SelectAddressView));
        }
    }

    public class CheckOutData
    {
        public Address DeliveryAddress { get; set; }
        public Address BillingAddress { get; set; }
        public PaymentMethodModel PaymentMethod { get; set; }

        public CheckOutData(Address deliveryAddress, Address billingAddress)
        {
            DeliveryAddress = deliveryAddress;
            BillingAddress = billingAddress;
        }
    }
}
