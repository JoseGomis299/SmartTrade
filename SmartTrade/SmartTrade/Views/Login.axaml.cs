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
            string email = TextBoxEmail.Text;
            string password = TextBoxPassword.Text;
            ClearErrors();
            bool hasErrors = false;

            if (_model.Email.IsNullOrEmpty())
            {
                TextBoxEmail.BringIntoView();
                TextBoxEmail.Focus();
                TextBoxEmail.ErrorText = "Title cannot be empty";
                hasErrors = true;
            }
            if (_model.Password.IsNullOrEmpty())
            {
                TextBoxPassword.BringIntoView();
                TextBoxPassword.Focus();
                TextBoxPassword.ErrorText = "Title cannot be empty";
                hasErrors = true;
            }

            try
            {
                await _model.Login(email, password);

                if (_model.Logged.IsSeller)
                {
                    SellerCatalog sellerCatalog = new SellerCatalog();

                    SmartTradeNavigationManager.Instance.ReInitializeNavigation(sellerCatalog);
                    await ((SellerCatalogModel) sellerCatalog.DataContext).LoadProductsAsync();
                }
                if(_model.Logged.IsConsumer)
                {
                    ProductCatalog productCatalog = new ProductCatalog();

                    SmartTradeNavigationManager.Instance.ReInitializeNavigation(productCatalog);
                    await ((ProductCatalogModel) productCatalog.DataContext).LoadProductsAsync();
                }
                if (_model.Logged.IsAdmin)
                {
                    AdminCatalog adminCatalog = new AdminCatalog();

                    SmartTradeNavigationManager.Instance.ReInitializeNavigation(adminCatalog);
                    await ((AdminCatalogModel) adminCatalog.DataContext).LoadProductsAsync();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Incorrect password"))
                {
                    TextBoxPassword.ErrorMessage.BringIntoView();
                    TextBoxPassword.ErrorMessage.Text = ex.Message;
                }

                if (ex.Message.Contains("Unregistered user"))
                {
                    TextBoxEmail.ErrorMessage.BringIntoView();
                    TextBoxEmail.ErrorMessage.Text = ex.Message;
                }
            }
        }
    }
}
