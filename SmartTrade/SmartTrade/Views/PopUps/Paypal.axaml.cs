using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.IdentityModel.Tokens;
using SmartTrade.ViewModels;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartTrade.Views
{
    public partial class Paypal : UserControl
    {
        public event Action<string[]> DatosPasados;
        private RegisterModel? _model;
        private bool _hasErrors;
        private int _start = 2;
        
        public Paypal() 
        {
            InitializeComponent();
            AcceptButton.Click += AcceptButton_Click;
            
            CancelButton.Click += CancelButton_Click;
        }
        public Paypal(RegisterModel model, Action onAccept)
        {
            DataContext = _model = model;

            InitializeComponent();
            AcceptButton.Click += AcceptButton_Click;
            CancelButton.Click += CancelButton_Click;
            AcceptButton.Click += (sender, e) => onAccept();

            TextBoxEmail.TextBox.TextChanged += CheckErrors;
            TextBoxPassword.TextBox.TextChanged += CheckErrors;
        }

        private void CancelButton_Click(object? sender, RoutedEventArgs e)
        {
            _model.PaypalEmail = "";
            _model.PaypalPassword = "";
            SmartTradeNavigationManager.Instance.MainView.HidePopUp();
        }

        private void AcceptButton_Click(object? sender, RoutedEventArgs e)
        {
            _model.PaypalEmail = TextBoxEmail.TextBox.Text;
            _model.PaypalPassword = TextBoxPassword.TextBox.Text;
            SmartTradeNavigationManager.Instance.MainView.HidePopUp();
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
            if (!System.Text.RegularExpressions.Regex.IsMatch(TextBoxEmail.Text, pattern))
            {
                TextBoxEmail.ErrorText = "Invalid email.";
                return true;
            }
            else
            {
                TextBoxEmail.ErrorText = "";
                return false;
            }
        }

        private bool CheckPassword()
        {
            if (TextBoxPassword.Text.IsNullOrEmpty())
            {
                TextBoxPassword.ErrorText = "Please input a password.";
                return true;
            }
            else
            {
                TextBoxPassword.ErrorText = "";
                return false;
            }
        }
    }
}
