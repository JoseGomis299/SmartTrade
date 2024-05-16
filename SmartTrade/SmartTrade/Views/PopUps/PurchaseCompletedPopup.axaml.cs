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
    public partial class PurchaseCompletedPopup : UserControl
    {
        public Action<bool?> onAccept;
        public PurchaseCompletedPopup()
        {
            InitializeComponent();
            AcceptButton.Click += AcceptButton_Click;
        }

        private async void AcceptButton_Click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.MainView.HidePopUp();
            await SmartTradeNavigationManager.Instance.MainView.ShowCatalogReinitializingAsync();
        }

    }
}

