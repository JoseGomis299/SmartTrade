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
        }

       

        private void CancelButton_Click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.MainView.HidePopUp();
            string email = "";
            string password = "";
            _model.PaypalEmail = email;
            _model.PaypalPassword = password;
        }

        private void ClearErrors()
        {
            TextBoxEmail.ErrorText = "";
            TextBoxPassword.ErrorText = "";
        }
        private void AcceptButton_Click(object? sender, RoutedEventArgs e)
        {

            ClearErrors();
            bool hasErrors = false;

            if (_model.PaypalEmail.IsNullOrEmpty())
            {
                TextBoxEmail.BringIntoView();
                TextBoxEmail.Focus();
                TextBoxEmail.ErrorText = "Email cannot be empty";
                hasErrors = true;
            }
            if (_model.PaypalPassword.IsNullOrEmpty())
            {
                TextBoxPassword.BringIntoView();
                TextBoxPassword.Focus();
                TextBoxPassword.ErrorText = "Password cannot be empty";
                hasErrors = true;
            }
            try 
            {
                if (hasErrors) return;
                SmartTradeNavigationManager.Instance.MainView.HidePopUp();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Incorrect password"))
                {
                    TextBoxPassword.ErrorText = ex.Message;
                }

                if (ex.Message.Contains("Unregistered user"))
                {
                    TextBoxEmail.ErrorMessage.BringIntoView();
                }
            }
        }

        
    }
}
