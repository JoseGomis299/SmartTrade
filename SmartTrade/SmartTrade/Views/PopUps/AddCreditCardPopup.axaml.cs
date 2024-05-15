using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Microsoft.IdentityModel.Tokens;
using SmartTrade.ViewModels;
using System;
using System.Globalization;
using SmartTrade.Entities;

namespace SmartTrade.Views
{
    public partial class AddCreditCardPopup : UserControl
    {
        public Action<bool?> onAccept;
        public AddCreditCardPopup()
        {
            InitializeComponent();
            AcceptButton.Click += AcceptButton_Click;
            CancelButton.Click += CancelButton_Click;
        }

        private void CancelButton_Click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.MainView.HidePopUp();
        }

        private void AcceptButton_Click(object? sender, RoutedEventArgs e)
        {
            onAccept?.Invoke(SaveCheckBox.IsChecked);
            SmartTradeNavigationManager.Instance.MainView.HidePopUp();
        }

        public CreditCardInfo GetCreditCard()
        {
            DateTime.TryParseExact(ExpiryDateTextBox.Text, "MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime expiryDate);
            return new CreditCardInfo(NumberTextBox.Text, expiryDate, CvvTextBox.Text, NameTextBox.Text);
        }
    }
}

