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
                    SmartTradeNavigationManager.Instance.NavigateTo(new SellerCatalog());

                }
                if(user is Consumer consumer)
                {
                    SmartTradeNavigationManager.Instance.NavigateTo(new ProductCatalog());

                }
                if (user is Admin admin)
                {
                    SmartTradeNavigationManager.Instance.NavigateTo(new ValidatePost());
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Contraseña incorrecta"))
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
