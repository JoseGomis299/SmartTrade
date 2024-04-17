using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Microsoft.IdentityModel.Tokens;
using SmartTrade.ViewModels;
using System;

namespace SmartTrade.Views
{
    public partial class Bizum : UserControl
    {
        private RegisterModel? _model;
        private Popup _popup;

        public Bizum()
        {
            InitializeComponent();
            AcceptButton.Click += AcceptButton_Click;
            CancelButton.Click += CancelButton_Click;
            _popup = new Popup
            {
                Child = this,
                IsLightDismissEnabled = true
            };
        }

        public Bizum(RegisterModel model)
        {
            DataContext = _model = model;

            InitializeComponent();
            AcceptButton.Click += AcceptButton_Click;
            CancelButton.Click += CancelButton_Click;
            _popup = new Popup
            {
                Child = this,
                IsLightDismissEnabled = true
            };
        }

        private void ClearErrors()
        {
            TextBoxNumber.ErrorText = "";
        }

        private void CancelButton_Click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.MainView.HidePopUp();
        }

        private void AcceptButton_Click(object? sender, RoutedEventArgs e)
        {
            string number = TextBoxNumber.Text;
            ClearErrors();
            bool hasErrors = false;

            if (_model.BizumNumber.IsNullOrEmpty())
            {
                TextBoxNumber.BringIntoView();
                TextBoxNumber.Focus();
                TextBoxNumber.ErrorText = "Telephone cannot be empty";
                hasErrors = true;
            }
            try
            {
                if (hasErrors) return;
                _model.ValidarTelefono();
                SmartTradeNavigationManager.Instance.MainView.HidePopUp();
            }
            catch (Exception ex)
            {
                TextBoxNumber.ErrorText = ex.Message;
            }
        }
        //private void CancelButton_Click(object? sender, RoutedEventArgs e)

       
    }
}

