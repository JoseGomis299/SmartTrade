using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Microsoft.IdentityModel.Tokens;
using SmartTrade.ViewModels;
using System;
using System.Globalization;
using SmartTrade.Entities;
using System.Text.RegularExpressions;

namespace SmartTrade.Views
{
    public partial class AddCreditCardPopup : UserControl
    {
        public Action<bool?> onAccept;
        private bool _hasErrors;
        private int _start = 4;

        public AddCreditCardPopup()
        {
            InitializeComponent();
            AcceptButton.Click += AcceptButton_Click;
            CancelButton.Click += CancelButton_Click;

            NameTextBox.TextBox.TextChanged += CheckErrors;
            ExpiryDateTextBox.TextBox.TextChanged += CheckErrors;
            NumberTextBox.TextBox.TextChanged += CheckErrors;
            CvvTextBox.TextBox.TextChanged += CheckErrors;

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

        public CreditCardInfo GetCreditCard()
        {
            int month = int.Parse(ExpiryDateTextBox.Text.Substring(0, 2));
            int year = int.Parse(ExpiryDateTextBox.Text.Substring(3, 2));
            DateTime expiryDate = new DateTime(year, month, 1);

            return new CreditCardInfo(NumberTextBox.Text, expiryDate, CvvTextBox.Text, NameTextBox.Text);
        }

        private void CheckErrors(object? sender, TextChangedEventArgs e)
        {
            if (--_start >= 0)
            {
                AcceptButton.IsEnabled = false;
                return;
            }
            _hasErrors = false | CheckExpiryDate();
            _hasErrors |= CheckNumber();
            _hasErrors |= CheckCvv();
            _hasErrors |= CheckName();

            AcceptButton.IsEnabled = !_hasErrors;
        }

        private bool CheckExpiryDate()
        {
            if (ExpiryDateTextBox.Text.IsNullOrEmpty())
            {
                ExpiryDateTextBox.ErrorText = "Please input an expiry date.";
                return true;
            }
            
            string pattern = @"^(0[1-9]|1[0-2])\/[0-9]{2}$";
            if (!Regex.IsMatch(ExpiryDateTextBox.Text, pattern))
            {
                ExpiryDateTextBox.ErrorText = "Invalid expiry date.";
                return  true;
            }

            ExpiryDateTextBox.ErrorText = "";
            return false;
        }

        private bool CheckNumber()
        {
            if (NumberTextBox.Text.IsNullOrEmpty())
            {
                NumberTextBox.ErrorText = "Please input a credit card number.";
                return true;
            }

            string pattern = @"^[0-9]{16}$";
            if (!Regex.IsMatch(NumberTextBox.Text, pattern))
            {
                NumberTextBox.ErrorText = "Invalid credit card number. Write at least 16 numbers.";
                return true;
            }

            NumberTextBox.ErrorText = "";
            return false;
        }

        private bool CheckCvv()
        {
            if (CvvTextBox.TextBox.Text.IsNullOrEmpty())
            {
                CvvTextBox.ErrorText = "Please input the CVV.";
                return true;
            }

            string pattern = @"^[0-9]{3}$";
            if (!Regex.IsMatch(CvvTextBox.Text, pattern))
            {
                CvvTextBox.ErrorText = "Invalid CVV. Write ar least 3 numbers";
                return true;
            }

            CvvTextBox.ErrorText = "";
            return false;
        }

        private bool CheckName()
        {
            if (NameTextBox.Text.IsNullOrEmpty())
            {
                NameTextBox.ErrorText = "Please input a name";
                return true;
            }

            NameTextBox.ErrorText = "";
            return false;
        }
    }
}

