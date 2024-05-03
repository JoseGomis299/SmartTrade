using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Threading.Tasks;
using SmartTrade.Services;
using SmartTrade.ViewModels;

namespace SmartTrade.Views
{
    public partial class WishListView : RefreshableUserControl
    {
        public WishListView()
        {
            DataContext = new WishListModel();
            InitializeComponent();
            ShareWishListButton.Click += ShareWishListButton_click;

            SetButtonVisibility();
        }

        private void SetButtonVisibility()
        {
            if (SmartTradeService.Instance.WishList == null)
            {
                ShareWishListButton.IsVisible = false;
            }
            else
            {
                ShareWishListButton.IsVisible = true;
            }
        }

        protected override void Refresh()
        {

        }

        protected override async Task RefreshAsync()
        {
            await ((WishListModel)DataContext).LoadWishListAsync();
        }

        private void ShareWishListButton_click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.MainView.ShowPopUp(new ShareWishView());
        }
    }
}