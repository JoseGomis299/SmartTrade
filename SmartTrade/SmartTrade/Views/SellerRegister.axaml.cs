using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.IdentityModel.Tokens;
using SmartTrade.Entities;
using SmartTrade.ViewModels;
using System;
using System.Runtime.Intrinsics.X86;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartTrade.Views
{
    public partial class SellerRegister : UserControl
    {
        private SellerRegisterModel _model;
        
        public SellerRegister()
        {
            InitializeComponent();
            DataContext = new SellerRegisterModel();
            SignInButton.Click += SignInButton_click;
            RegisterConsumerButton.Click += RegisterConsumerButton_click;
            LogInButton.Click += LoginButton_click;
        }

        private void LoginButton_click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.NavigateTo(new Login());
        }

        private void RegisterConsumerButton_click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.NavigateTo(new Register());
        }
        private void ClearErrors()
        {
            TextBoxName.ErrorText = "";
            TextBoxEmail.ErrorText = "";
            TextBoxLastNames.ErrorText = "";
            TextBoxPassword.ErrorText = "";
            TextBoxCIF.ErrorText = "";
            TextBoxIBAN.ErrorText = "";
            TextBoxCompany.ErrorText = "";
        }
        private async void SignInButton_click(object? sender, RoutedEventArgs e)
        {
            ClearErrors();
            bool hasErrors = false;

            if (_model.Name.IsNullOrEmpty())
            {
                TextBoxName.BringIntoView();
                TextBoxName.Focus();
                TextBoxName.ErrorText = "Name cannot be empty";
                hasErrors = true;
            }
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
            if (_model.CIF.IsNullOrEmpty())
            {
                TextBoxCIF.BringIntoView();
                TextBoxCIF.Focus();
                TextBoxCIF.ErrorText = "CIF cannot be empty";
                hasErrors = true;
            }
            if (_model.Company.IsNullOrEmpty())
            {
                TextBoxCompany.BringIntoView();
                TextBoxCompany.Focus();
                TextBoxCompany.ErrorText = "Company cannot be empty";
                hasErrors = true;
            }
            if (_model.IBAN.IsNullOrEmpty())
            {
                TextBoxIBAN.BringIntoView();
                TextBoxIBAN.Focus();
                TextBoxIBAN.ErrorText = "IBAN cannot be empty";
                hasErrors = true;
            }
            if (_model.LastNames.IsNullOrEmpty())
            {
                TextBoxLastNames.BringIntoView();
                TextBoxLastNames.Focus();
                TextBoxLastNames.ErrorText = "Last Names cannot be empty";
                hasErrors = true;
            }
            try
            {
                if (hasErrors) return;
                await _model.RegisterSeller();

                await SmartTradeNavigationManager.Instance.MainView.ShowCatalogReinitializingAsync();
            }
            catch (Exception ex) 
            {
                if (ex.Message.Contains("Existing user"))
                {
                    TextBoxEmail.BringIntoView();
                    TextBoxEmail.ErrorText = ex.Message;
                }

                if (ex.Message.Contains("Incorrect CIF/DNI. Please enter a valid CIF/DNI"))
                {
                    TextBoxCIF.BringIntoView();
                    TextBoxCIF.ErrorText = ex.Message;
                }
            }
            
        }
    }
}
