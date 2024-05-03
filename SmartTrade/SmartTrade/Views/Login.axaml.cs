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
        public Login()
        {
            DataContext = _model = new LoginModel();
            InitializeComponent();
            SignUpButton.Click += SignUpButton_click;
            RegisterButton.Click += RegisterButton_click;
        }

       
            private void RegisterButton_click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.NavigateTo(new ChooseProfile());

        }

        private void ClearErrors()
        {
            TextBoxEmail.ErrorText = "";
            TextBoxPassword.ErrorText = "";
        }
        private async void SignUpButton_click(object? sender, RoutedEventArgs e)
        {
            ClearErrors();
            bool hasErrors = false;

            if (_model.Email.IsNullOrEmpty())
            {
                TextBoxEmail.BringIntoView();
                TextBoxEmail.Focus();
                TextBoxEmail.ErrorText = "Email cannot be empty";
                hasErrors = true;
            }
            if (_model.Password.IsNullOrEmpty())
            {
                TextBoxPassword.BringIntoView();
                TextBoxPassword.Focus();
                TextBoxPassword.ErrorText = "Password cannot be empty";
                hasErrors = true;
            }

            try
            {
                _model.ValidarEmail();
                await _model.Login(_model.Email, _model.Password);
                if (_model.Logged == null) { return; }
            }
            catch (Exception ex)
            {

                if (ex.Message.Contains("Invalid email. Please enter a valid email"))
                {
                    TextBoxEmail.BringIntoView();
                    TextBoxEmail.ErrorText = ex.Message;
                }

                if (ex.Message.Contains("Email or Password are incorrect"))
                {
                    TextBoxEmail.BringIntoView();
                    TextBoxEmail.ErrorText = ex.Message;
                    TextBoxPassword.ErrorText = ex.Message;     
                }
            }

            await SmartTradeNavigationManager.Instance.MainView.ShowCatalogReinitializingAsync();
        }
    }
}
