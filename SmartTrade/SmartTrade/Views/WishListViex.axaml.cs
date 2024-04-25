using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace SmartTrade.Views
{
    public partial class WishListViex : UserControl
    {
        public WishListViex()
        {
            InitializeComponent();
            ShareWishListButton.Click += ShareWishListButton_click;
        }

        private void ShareWishListButton_click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.MainView.ShowPopUp(new ShareWishView());
        }
    }
}
