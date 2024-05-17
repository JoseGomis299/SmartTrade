using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.IdentityModel.Tokens;
using SmartTrade.Entities;
using SmartTrade.ViewModels;
using System;

namespace SmartTrade.Views
{
    public partial class Login : UserControl
    {
        private LoginModel? _model;
        private bool _EmailError;
        private bool _PasswordError;
        private int _start = 2;
        public Login()
        {
            DataContext = _model = new LoginModel();
            InitializeComponent();
            SignUpButton.Click += SignUpButton_click;
            RegisterButton.Click += RegisterButton_click;

            TextBoxEmail.TextBox.TextChanged += CheckEmail;
            TextBoxPassword.TextBox.TextChanged += CheckPassword;
        }

       
        private void RegisterButton_click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.NavigateTo(new ChooseProfile());
        }

        private void CheckEmail(object? sender, TextChangedEventArgs e)
        {
            if (--_start >= 0)
            {
                SignUpButton.IsEnabled = false;
                return;
            }

            if (TextBoxEmail.TextBox.Text.IsNullOrEmpty())
            {
                TextBoxEmail.ErrorText = "Please input your email";
                SignUpButton.IsEnabled = false;
                _EmailError = true;
                return;
            }
            
            string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(TextBoxEmail.Text, pattern))
            {
                TextBoxEmail.ErrorText = "Invalid email";
                SignUpButton.IsEnabled = false;
                _EmailError = true;
            }
            else
            {
                TextBoxEmail.ErrorText = "";
                _EmailError = false;
                SignUpButton.IsEnabled = !_EmailError & !_PasswordError;
            }
        }

        private void CheckPassword(object? sender, TextChangedEventArgs e)
        {
            if (--_start >= 0)
            {
                SignUpButton.IsEnabled = false;
                return;
            }

            if (TextBoxPassword.Text.IsNullOrEmpty())
            {
                TextBoxPassword.ErrorText = "Please input your password";
                SignUpButton.IsEnabled = false;
                _PasswordError = true;
            }
            else
            {
                TextBoxPassword.ErrorText = "";
                _PasswordError = false;
                SignUpButton.IsEnabled = !_EmailError & !_PasswordError;
            }
        }

        private async void SignUpButton_click(object? sender, RoutedEventArgs e)
        {
            try
            {
                await _model.Login(_model.Email, _model.Password);
                await SmartTradeNavigationManager.Instance.MainView.ShowCatalogReinitializingAsync();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Email or Password are incorrect"))
                {
                    TextBoxEmail.BringIntoView();
                    TextBoxEmail.ErrorText = ex.Message;
                    TextBoxPassword.ErrorText = ex.Message;     
                }
            }

        }
    }
}
