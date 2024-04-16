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
        private PaypalModel? _model;
        private INavigationService _navigationService;

        public Paypal(Window ventana)
        {
            InitializeComponent();
            DataContext = _model = new PaypalModel();
            AcceptButton.Click += AcceptButton_Click;
            CancelButton.Click += CancelButton_Click;
            _ventana = ventana;
        }

        private void ClearErrors()
        {
            TextBoxEmail.ErrorText = "";
            TextBoxPassword.ErrorText = "";
        }
        private void AcceptButton_Click(object? sender, RoutedEventArgs e)
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
                if (hasErrors)return;
                email = TextBoxEmail.Text;
                password = TextBoxPassword.Text;
                var datos = new string[] { email,password};
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
        private void CancelButton_Click(object? sender, RoutedEventArgs e) => _ventana.Close();

        
    }
}
