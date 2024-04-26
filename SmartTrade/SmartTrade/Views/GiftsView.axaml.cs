using System;
using Avalonia.Controls;
using SmartTrade.ViewModels;

namespace SmartTrade.Views
{
    public partial class GiftsView : UserControl
    {
        public GiftsView()
        {
            DataContext = new GiftsModel();
            InitializeComponent();

            CheckOutButton.Click += BuyItems;
            SmartTradeNavigationManager.Instance.OnChangeNavigationStack += ClearReferences;
        }

        private void ClearReferences(int stack)
        {
            ((GiftsModel)DataContext).UnSubscribeFromCartNotifications();
            CheckOutButton.Click -= BuyItems;
            SmartTradeNavigationManager.Instance.OnChangeNavigationStack -= ClearReferences;
            DataContext = null;
        }

        private void BuyItems(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
        }
    }
}
