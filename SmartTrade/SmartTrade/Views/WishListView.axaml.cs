using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using SmartTrade.ViewModels;

namespace SmartTrade.Views
{
    public partial class WishListView : UserControl
    {
        public WishListView()
        {
            DataContext = new WishListModel();
            InitializeComponent();
            ShareWishListButton.Click += ShareWishListButton_click;
        }

        private void ShareWishListButton_click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.MainView.ShowPopUp(new ShareWishView());
        }
    }
}