using Avalonia.Controls;
using Avalonia.Interactivity;
using SmartTrade.ViewModels;
using SmartTradeLib.BusinessLogic;
using SmartTradeLib.Entities;
using System;

namespace SmartTrade.Views
{
    public partial class Login : UserControl
    {
        private LoginModel? _model;
        public Login()
        {
            InitializeComponent();
            SignUpButton.Click += SignUpButton_click;
        }

        private void SignUpButton_click(object? sender, RoutedEventArgs e)
        {
            string email = TextBoxEmail.Text;
            string password = TextBoxPassword.Text;
            User user;
            try
            {
                _model.Login(email, password);
                user=_model.getloggeduser();
                if (user is Seller seller)
                {
                    NavigationManager.NavigateTo(new SellerCatalog());

                }
                if(user is Consumer consumer)
                {
                    NavigationManager.NavigateTo(new ProductCatalog());

                }
                if (user is Admin admin)
                {
                    NavigationManager.NavigateTo(new ValidatePost());
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Contraseņa incorrecta"))
                {
                    TextBoxPassword.ErrorMessage.BringIntoView();
                    TextBoxPassword.ErrorMessage.Text = ex.Message;
                }

                if (ex.Message.Contains("Usuario no registrado"))
                {
                    TextBoxEmail.ErrorMessage.BringIntoView();
                    TextBoxEmail.ErrorMessage.Text = ex.Message;
                }
            }
        }
    }
}
