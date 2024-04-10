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
            DataContext = _model = new LoginModel();
            InitializeComponent();
            SignUpButton.Click += SignUpButton_click;
            RegisterButton.Click += RegisterButton_click;
        }

        private void RegisterButton_click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.NavigateTo(new ChooseProfile());

        }
        
        private void SignUpButton_click(object? sender, RoutedEventArgs e)
        {
            string email = TextBoxEmail.Text;
            string password = TextBoxPassword.Text;
            User user;
            try
            {
                _model.Login(email, password);
                user=_model.Logged;
                if (user is Seller seller)
                {
                    SmartTradeNavigationManager.Instance.ReInitializeNavigation(new SellerCatalog());
                }
                if(user is Consumer consumer)
                {
                    SmartTradeNavigationManager.Instance.ReInitializeNavigation(new ProductCatalog());

                }
                if (user is Admin admin)
                {
                    SmartTradeNavigationManager.Instance.ReInitializeNavigation(new AdminCatalog());
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
