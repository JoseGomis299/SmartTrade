using Avalonia.Controls;
using Avalonia.Interactivity;
using GalaSoft.MvvmLight.Views;
using Microsoft.IdentityModel.Tokens;
using SmartTrade.ViewModels;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartTrade.Views
{
    public partial class Paypal : UserControl
    {
        public event Action<string[]> DatosPasados;
        private Window _ventana;
        private RegisterModel? _model;
        public Paypal()
        {
            InitializeComponent();
            DataContext = _model = new RegisterModel();
            AcceptButton.Click += AcceptButton_Click;
            CancelButton.Click += CancelButton_Click;
        }

        private void ClearErrors()
        {
            TextBoxEmail.ErrorText = "";
            TextBoxPassword.ErrorText = "";
        }
        private void AcceptButton_Click(object? sender, RoutedEventArgs e)
        {
            string  paypalEmail = TextBoxEmail.Text;
            string paypalPassword = TextBoxPassword.Text;
            

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
                if (hasErrors)return;
                _model.PaypalEmail = paypalEmail;
                _model.PaypalPassword = paypalPassword;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Wrong email. Please enter a valid email"))
                {
                    TextBoxEmail.ErrorMessage.BringIntoView();
                    TextBoxEmail.ErrorMessage.Text = ex.Message;
                }
            }
        }
        private void CancelButton_Click(object? sender, RoutedEventArgs e) => _ventana.Close();

        
    }
}
