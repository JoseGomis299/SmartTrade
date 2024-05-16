using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Microsoft.IdentityModel.Tokens;
using SmartTrade.ViewModels;
using System;
using SmartTrade.Entities;
using System.Text.RegularExpressions;

namespace SmartTrade.Views
{
    public partial class AddBizumPopup : UserControl
    {
        public Action<bool?> onAccept;
        private bool _hasErrors;

        public AddBizumPopup()
        {
            InitializeComponent();
            AcceptButton.Click += AcceptButton_Click;
            CancelButton.Click += CancelButton_Click;

            NumberTextBox.TextBox.TextChanged += CheckNumber;

            if (SmartTradeNavigationManager.Instance.CurrentStack == 2)
            {
                SaveCheckBox.IsVisible = false;
            }
        }

        private void CancelButton_Click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.MainView.HidePopUp();
        }

        private void AcceptButton_Click(object? sender, RoutedEventArgs e)
        {
            if (_hasErrors) return;

            onAccept?.Invoke(SaveCheckBox.IsChecked);
            SmartTradeNavigationManager.Instance.MainView.HidePopUp();
        }

        public BizumInfo GetBizum()
        {
            return new BizumInfo(NumberTextBox.Text);
        }

        private void CheckNumber(object? sender, TextChangedEventArgs e)
        {
            string pattern = @"^[0-9]{9}$";
            if (!Regex.IsMatch(NumberTextBox.Text, pattern))
            {
                NumberTextBox.ErrorText = "Invalid telephone number.";
                _hasErrors = true;
            }
            else
            {
                NumberTextBox.ErrorText = "";
                _hasErrors = false;
            }
        }

    }
}

