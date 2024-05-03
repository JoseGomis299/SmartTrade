using Avalonia.Controls;
using Avalonia.Interactivity;
using SmartTrade.Services;
using SmartTrade.ViewModels;
using SmartTradeDTOs;
using System;

namespace SmartTrade.Views
{
    public partial class Profile : UserControl
    {
        private ProfileModel _model;
        public Profile()
        {
            DataContext = _model = new ProfileModel();
            InitializeComponent();

            LogoutButton.Click += LogOut;
            WhisListButton.Click += WishListButton_Click;

            if (_model.LoggedType == UserType.Seller)
            {
                AddPostButton.IsVisible = true;
                AddPostButton.Click += (sender, args) =>
                {
                    SmartTradeNavigationManager.Instance.NavigateTo(new RegisterPost());
                };
            }
            else { AddPostButton.IsVisible = false; }
        }

        private void WishListButton_Click(object? sender, RoutedEventArgs e)
        {
            var view = new WishListView();
            SmartTradeNavigationManager.Instance.NavigateTo(view);
        }

        private async void LogOut(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            await _model.LogOut();
            await SmartTradeNavigationManager.Instance.MainView.ShowCatalogReinitializingAsync();
        }
    }
}
