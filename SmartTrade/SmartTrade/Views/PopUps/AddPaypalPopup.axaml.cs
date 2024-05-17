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
        private int _start = 2;
        public AddPaypalPopup()
        {
            InitializeComponent();
            AcceptButton.Click += AcceptButton_Click;
            CancelButton.Click += CancelButton_Click;

            EmailTextBox.TextBox.TextChanged += CheckErrors;
            PasswordTextBox.TextBox.TextChanged += CheckErrors;

            if (SmartTradeNavigationManager.Instance.CurrentStack == 2)
            {
                SaveCheckBox.IsVisible = false;
            }
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

        private void CheckErrors(object? sender, TextChangedEventArgs e)
        {
            if (--_start >= 0)
            {
                AcceptButton.IsEnabled = false;
                return;
            }
            _hasErrors = false | CheckEmail();
            _hasErrors |= CheckPassword();

            AcceptButton.IsEnabled = !_hasErrors;
        }

        private bool CheckEmail()
        {
            string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(EmailTextBox.Text, pattern))
            {
                EmailTextBox.ErrorText = "Invalid email.";
                return true;
            }
            else
            {
                EmailTextBox.ErrorText = "";
                return false;
            }
        }

        private bool CheckPassword()
        {
            if (PasswordTextBox.Text.IsNullOrEmpty())
            {
                PasswordTextBox.ErrorText = "Please input a password.";
                return true;
            }
            else
            {
                PasswordTextBox.ErrorText = "";
                return false;
            }
        }
    }
}

