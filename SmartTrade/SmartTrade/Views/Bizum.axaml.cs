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
            //CancelButton.Click += CancelButton_Click;
            _popup = new Popup
            {
                Child = this,
                IsLightDismissEnabled = true
            };
        }
        public Bizum(RegisterModel model)
        {
            InitializeComponent();
            DataContext = _model = model;
            AcceptButton.Click += AcceptButton_Click;
            //CancelButton.Click += CancelButton_Click;
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
        private void AcceptButton_Click(object? sender, RoutedEventArgs e)
        {
            string number = TextBoxNumber.Text;
            ClearErrors();
            bool hasErrors = false;

            if (_model.BizumNumber.IsNullOrEmpty())
            {
                TextBoxNumber.BringIntoView();
                TextBoxNumber.Focus();
                TextBoxNumber.ErrorText = "Title cannot be empty";
                hasErrors = true;
            }
            try
            {
                if (hasErrors) return;
                _model.ValidarTelefono();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Wrong phone number. Only digits are allowed"))
                {
                    TextBoxNumber.ErrorMessage.BringIntoView();
                    TextBoxNumber.ErrorMessage.Text = ex.Message;
                }

            }
        }
        //private void CancelButton_Click(object? sender, RoutedEventArgs e)

       
    }
}

