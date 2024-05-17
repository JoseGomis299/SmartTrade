using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Microsoft.IdentityModel.Tokens;
using SmartTrade.ViewModels;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SmartTrade.Views
{
    public partial class AddCreditCart : UserControl
    {
        private RegisterModel? _model;
        private bool _hasErrors;
        private int _start = 4;
        public AddCreditCart()
        {
            InitializeComponent();
            AcceptButton.Click += AcceptButton_Click;
            CancelButton.Click += CancelButton_Click;
        }
        public AddCreditCart(RegisterModel model, Action onAccept)
        {
            DataContext = _model = model;

            InitializeComponent();
            AcceptButton.Click += AcceptButton_Click;
            AcceptButton.Click += (sender, e) => onAccept(); 
            CancelButton.Click += CancelButton_Click;


            TextBoxName.TextBox.TextChanged += CheckErrors;
            TextBoxExpiryDate.TextBox.TextChanged += CheckErrors;
            TextBoxNumber.TextBox.TextChanged += CheckErrors;
            TextBoxCVV.TextBox.TextChanged += CheckErrors;
        }
        private void CancelButton_Click(object? sender, RoutedEventArgs e)
        {
            _model.CreditCardName = "";
            _model.CreditCardNumber = "";
            _model.CreditCardExpiryDate = "";
            _model.CreditCardCVV = "";
            SmartTradeNavigationManager.Instance.MainView.HidePopUp();
        }
        
        private void AcceptButton_Click(object? sender, RoutedEventArgs e)
        {
            _model.CreditCardName = TextBoxName.TextBox.Text;
            _model.CreditCardNumber = TextBoxNumber.TextBox.Text;
            _model.CreditCardExpiryDate = TextBoxExpiryDate.TextBox.Text;
            _model.CreditCardCVV = TextBoxCVV.TextBox.Text;
            SmartTradeNavigationManager.Instance.MainView.HidePopUp();
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
            if (TextBoxExpiryDate.Text.IsNullOrEmpty())
            {
                TextBoxExpiryDate.ErrorText = "Please input an expiry date.";
                return true;
            }

            string pattern = @"^(0[1-9]|1[0-2])\/[0-9]{2}$";
            if (!Regex.IsMatch(TextBoxExpiryDate.Text, pattern))
            {
                TextBoxExpiryDate.ErrorText = "Invalid expiry date.";
                return true;
            }

            TextBoxExpiryDate.ErrorText = "";
            return false;
        }

        private bool CheckNumber()
        {
            if (TextBoxNumber.Text.IsNullOrEmpty())
            {
                TextBoxNumber.ErrorText = "Please input a credit card number.";
                return true;
            }

            string pattern = @"^[0-9]{16}$";
            if (!Regex.IsMatch(TextBoxNumber.Text, pattern))
            {
                TextBoxNumber.ErrorText = "Invalid credit card number. Write at least 16 numbers.";
                return true;
            }

            TextBoxNumber.ErrorText = "";
            return false;
        }

        private bool CheckCvv()
        {
            if (TextBoxCVV.TextBox.Text.IsNullOrEmpty())
            {
                TextBoxCVV.ErrorText = "Please input the CVV.";
                return true;
            }

            string pattern = @"^[0-9]{3}$";
            if (!Regex.IsMatch(TextBoxCVV.Text, pattern))
            {
                TextBoxCVV.ErrorText = "Invalid CVV. Write ar least 3 numbers";
                return true;
            }

            TextBoxCVV.ErrorText = "";
            return false;
        }

        private bool CheckName()
        {
            if (TextBoxName.Text.IsNullOrEmpty())
            {
                TextBoxName.ErrorText = "Please input a name";
                return true;
            }

            TextBoxName.ErrorText = "";
            return false;
        }
    }
}
