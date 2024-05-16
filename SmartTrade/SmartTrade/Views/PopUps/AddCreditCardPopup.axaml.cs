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

        public AddCreditCardPopup()
        {
            InitializeComponent();
            AcceptButton.Click += AcceptButton_Click;
            CancelButton.Click += CancelButton_Click;

            ExpiryDateTextBox.TextBox.TextChanged += CheckErrors;
            NumberTextBox.TextBox.TextChanged += CheckErrors;
            CvvTextBox.TextBox.TextChanged += CheckErrors;
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
            _hasErrors &= CheckExpiryDate();
            _hasErrors &= CheckNumber();
            _hasErrors &= CheckCvv();
        }

        private bool CheckExpiryDate()
        {
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
            string pattern = @"^[0-9]{3}$";
            if (!Regex.IsMatch(CvvTextBox.Text, pattern))
            {
                CvvTextBox.ErrorText = "Invalid CVV. Write ar least 3 numbers";
                return true;
            }

            CvvTextBox.ErrorText = "";
            return false;
        }


    }
}

