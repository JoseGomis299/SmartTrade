using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.IdentityModel.Tokens;
using SmartTrade.Entities;
using SmartTrade.ViewModels;
using System;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartTrade.Views
{
    public partial class SellerRegister : UserControl
    {
        private SellerRegisterModel _model;
        private bool _hasErrors;
        private int _start = 6;
        public SellerRegister()
        {
            InitializeComponent();
            DataContext = new SellerRegisterModel();
            SignInButton.Click += SignInButton_click;
            RegisterConsumerButton.Click += RegisterConsumerButton_click;
            LogInButton.Click += LoginButton_click;

            TextBoxName.TextBox.TextChanged += CheckErrors;
            TextBoxLastNames.TextBox.TextChanged += CheckErrors;
            TextBoxCIF.TextBox.TextChanged += CheckErrors;
            TextBoxEmail.TextBox.TextChanged += CheckErrors;
            TextBoxPassword.TextBox.TextChanged += CheckErrors;
            TextBoxCompany.TextBox.TextChanged += CheckErrors;
        }

        private void LoginButton_click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.NavigateTo(new Login());
        }

        private void RegisterConsumerButton_click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.NavigateTo(new Register());
        }

        private void CheckErrors(object? sender, TextChangedEventArgs e)
        {
            if (--_start >= 0)
            {
                SignInButton.IsEnabled = false;
                return;
            }
            _hasErrors = false | CheckName();
            _hasErrors |= CheckLastName();
            _hasErrors |= CheckCIF();
            _hasErrors |= CheckEmail();
            _hasErrors |= CheckPassword();
            _hasErrors |= CheckCompany();

            SignInButton.IsEnabled = !_hasErrors;
        }

        private bool CheckName()
        {
            if (TextBoxName.Text.IsNullOrEmpty())
            {
                TextBoxName.ErrorText = "Please input your name";
                return true;
            }

            TextBoxName.ErrorText = "";
            return false;
        }

        private bool CheckLastName()
        {
            if (TextBoxLastNames.Text.IsNullOrEmpty())
            {
                TextBoxLastNames.ErrorText = "Please input your last names";
                return true;
            }

            TextBoxLastNames.ErrorText = "";
            return false;
        }

        private bool CheckDNI()
        {
            if (TextBoxCIF.Text.IsNullOrEmpty())
            {
                TextBoxCIF.ErrorText = "Please input your CIF/DNI/NIE";
                return true;
            }

            string patternDNI = @"^\d{8}[A-HJ-NP-TV-Z]$";
            string patternNIE = "^[XYZ]\\d{7}[A-HJ-NP-TV-Z]$";
            string patternCIF = @"^[A-HJ-NP-SUVW][0-9]{7}[0-9A-J]$";
            if (!(Regex.IsMatch(TextBoxCIF.Text, patternDNI)| !Regex.IsMatch(TextBoxCIF.Text, patternNIE)| !Regex.IsMatch(TextBoxCIF.Text, patternCIF)))
            {
                TextBoxCIF.ErrorText = ("Please input a valid CIF/DNI/NIE");
                return true;
            }

            TextBoxCIF.ErrorText = "";
            return false;
        }

        private bool CheckEmail()
        {
            if (TextBoxEmail.Text.IsNullOrEmpty())
            {
                TextBoxEmail.ErrorText = "Please input your Email";
                return true;
            }

            string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            if (!Regex.IsMatch(TextBoxEmail.Text, pattern))
            {
                TextBoxEmail.ErrorText = "Invalid email. Please enter a valid email";
                return true;
            }

            TextBoxEmail.ErrorText = "";
            return false;
        }

        private bool CheckPassword()
        {
            if (TextBoxPassword.Text.IsNullOrEmpty())
            {
                TextBoxPassword.ErrorText = "Please input a password";
                return true;
            }

            TextBoxPassword.ErrorText = "";
            return false;
        }

        private bool CheckCompany()
        {
            if (TextBoxCompany.Text.IsNullOrEmpty())
            {
                TextBoxCompany.ErrorText = "Please input your company";
                return true;
            }

            TextBoxCompany.ErrorText = "";
            return false;
        }

        private async void SignInButton_click(object? sender, RoutedEventArgs e)
        {
            try
            {
                await _model.RegisterSeller();
                await SmartTradeNavigationManager.Instance.MainView.ShowCatalogReinitializingAsync();
            }
            catch (Exception ex) 
            {
                if (ex.Message.Contains("Existing user"))
                {
                    TextBoxEmail.ErrorText = ex.Message;
                    TextBoxEmail.BringIntoView();
                    _hasErrors = true;
                    SignInButton.IsEnabled = false;
                }
            }
            
        }
    }
}
