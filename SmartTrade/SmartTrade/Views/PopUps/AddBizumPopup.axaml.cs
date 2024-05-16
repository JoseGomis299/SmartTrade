using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Microsoft.IdentityModel.Tokens;
using SmartTrade.ViewModels;
using System;
using SmartTrade.Entities;

namespace SmartTrade.Views
{
    public partial class AddBizumPopup : UserControl
    {
        public Action<bool?> onAccept;
        public AddBizumPopup()
        {
            InitializeComponent();
            AcceptButton.Click += AcceptButton_Click;
            CancelButton.Click += CancelButton_Click;
        }

        private void CancelButton_Click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.MainView.HidePopUp();
        }

        private void AcceptButton_Click(object? sender, RoutedEventArgs e)
        {
            onAccept?.Invoke(SaveCheckBox.IsChecked);
            SmartTradeNavigationManager.Instance.MainView.HidePopUp();
        }

        public BizumInfo GetBizum()
        {
            return new BizumInfo(NumberTextBox.Text);
        }
    }
}

