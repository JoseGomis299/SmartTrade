using Avalonia.Controls;
using Avalonia.Interactivity;
using SmartTrade.ViewModels;
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
            _model.Login(email, password);
        }
    }
}
