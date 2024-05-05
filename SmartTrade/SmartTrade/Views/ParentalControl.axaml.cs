using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.IdentityModel.Tokens;
using SmartTrade.ViewModels;
using System;

namespace SmartTrade.Views
{
    public partial class ParentalControl : UserControl
    {
        private ProfileModel? _model;

        public ParentalControl()
        {
            InitializeComponent();
            AcceptButton.Click += AcceptButton_Click;
            CancelButton.Click += CancelButton_Click;
        }
        public ParentalControl(ProfileModel model)
        {
            InitializeComponent();
            AcceptButton.Click += AcceptButton_Click;
            CancelButton.Click += CancelButton_Click;
            DataContext = _model = model;

        }
        private void CancelButton_Click(object? sender, RoutedEventArgs e)
        {
            
            ClearErrors();
            SmartTradeNavigationManager.Instance.MainView.HidePopUp();
        }
        private void AcceptButton_Click(object? sender, RoutedEventArgs e)
        {
            bool hasErrors = false;
            ClearErrors();
            if (_model.Password.IsNullOrEmpty())
            {
                TextBoxPassword.BringIntoView();
                TextBoxPassword.Focus();
                TextBoxPassword.ErrorText = "Name cannot be empty";
                hasErrors = true;
            }
            if (hasErrors) return;
            if (_model.IsCorrectPassword())
            {
                _model.IsParentalControlEnabled = false;
                SmartTradeNavigationManager.Instance.MainView.HidePopUp();
            }
            else
            {
                TextBoxPassword.BringIntoView();
                TextBoxPassword.ErrorText = "Incorrect Password";
            }
        }

        private void ClearErrors()
        {
            TextBoxPassword.ErrorText = "";
        }
    }
}
