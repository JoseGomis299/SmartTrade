using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Microsoft.IdentityModel.Tokens;
using SmartTrade.ViewModels;
using System;
using System.Text.RegularExpressions;

namespace SmartTrade.Views
{
    public partial class Bizum : UserControl
    {
        private RegisterModel? _model;
        private Popup _popup;
        private bool _hasErrors;
        private bool _start = true;

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

        public Bizum(RegisterModel model, Action onAccept)
        {
            DataContext = _model = model;

            InitializeComponent();
            AcceptButton.Click += AcceptButton_Click;
            AcceptButton.Click += (sender, e) => onAccept();
            CancelButton.Click += CancelButton_Click;

            TextBoxNumber.TextBox.TextChanged += CheckNumber;

            _popup = new Popup
            {
                Child = this,
                IsLightDismissEnabled = true
            };
        }

        private void CancelButton_Click(object? sender, RoutedEventArgs e)
        {
            _model.BizumNumber = "";
            SmartTradeNavigationManager.Instance.MainView.HidePopUp();
            
            
        }

        private void AcceptButton_Click(object? sender, RoutedEventArgs e)
        {
            _model.BizumNumber = TextBoxNumber.TextBox.Name;
            SmartTradeNavigationManager.Instance.MainView.HidePopUp();
        }

        private void CheckNumber(object? sender, TextChangedEventArgs e)
        {
            if (_start)
            {
                _start = false;
                AcceptButton.IsEnabled = false;
                return;
            }

            if (TextBoxNumber.TextBox.Text.IsNullOrEmpty())
            {
                TextBoxNumber.ErrorText = "Please enter a telephone number.";
                AcceptButton.IsEnabled = false;
                _hasErrors = true;
                return;
            }

            string pattern = @"^[0-9]{9}$";
            if (!Regex.IsMatch(TextBoxNumber.Text, pattern))
            {
                TextBoxNumber.ErrorText = "Invalid telephone number.";
                AcceptButton.IsEnabled = false;
                _hasErrors = true;
            }
            else
            {
                TextBoxNumber.ErrorText = "";
                AcceptButton.IsEnabled = true;
                _hasErrors = false;
            }
        }
    }
}

