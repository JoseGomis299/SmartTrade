using System;
using Avalonia.Controls;
using SmartTrade.ViewModels;

namespace SmartTrade.Views
{
    public partial class ShoppingCartView : UserControl
    {
        public ShoppingCartView()
        {
            DataContext = new ShoppingCartModel();
            InitializeComponent();

            CheckOutButton.Click += BuyItems;
            SmartTradeNavigationManager.Instance.OnChangeNavigationStack += ClearReferences;
        }

        private void ClearReferences(int stack)
        {
            ((ShoppingCartModel)DataContext).UnSubscribeFromCartNotifications();
            CheckOutButton.Click -= BuyItems;
            SmartTradeNavigationManager.Instance.OnChangeNavigationStack -= ClearReferences;
            DataContext = null;
        }

        private async void BuyItems(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
             await ((ShoppingCartModel)DataContext).BuyItemsAsync();
        }
    }
}
