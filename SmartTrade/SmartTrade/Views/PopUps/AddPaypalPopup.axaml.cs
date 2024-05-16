using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Microsoft.IdentityModel.Tokens;
using SmartTrade.ViewModels;
using System;
using SmartTrade.Entities;

namespace SmartTrade.Views
{
    public partial class AddPaypalPopup : UserControl
    {
        public Action<bool?> onAccept;

        private bool _hasErrors;
        public AddPaypalPopup()
        {
            InitializeComponent();
            AcceptButton.Click += AcceptButton_Click;
            CancelButton.Click += CancelButton_Click;

            EmailTextBox.TextBox.TextChanged += CheckEmail;
        }

        private void CancelButton_Click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.MainView.HidePopUp();
        }

        private void AcceptButton_Click(object? sender, RoutedEventArgs e)
        {
            if (_hasErrors) return;

            onAccept?.Invoke(SaveCheckBox.IsChecked);
            SmartTradeNavigationManager.Instance.MainView.HidePopUp();
        }

        public PayPalInfo GetPaypal()
        {
            return new PayPalInfo(EmailTextBox.Text, PasswordTextBox.Text);
        }

        private void CheckEmail(object? sender, TextChangedEventArgs e)
        {
            string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(EmailTextBox.Text, pattern))
            {
                EmailTextBox.ErrorText = "Invalid email.";
                _hasErrors = true;
            }
            else
            {
                EmailTextBox.ErrorText = "";
                _hasErrors = false;
            }
        }
    }
}

