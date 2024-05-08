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
            ParentalToggleButton.Click += ParentalToggleButton_Click;
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

            SetWishListButtonVisibility();
            SetParentalToggleButtonVisibility();
            ParentalToggleButton.IsChecked = _model.IsParentalControlEnabled;
        }

        private void ParentalToggleButton_Click(object? sender, RoutedEventArgs e)
        {

            if (_model.IsParentalControlEnabled)
            {
                SmartTradeNavigationManager.Instance.MainView.ShowPopUp(new ParentalControl(_model));
            }
            else
            {
                _model.IsParentalControlEnabled = true;
                ParentalToggleButton.IsChecked = _model.IsParentalControlEnabled;
                _model.UpdateParentalControlStatus();
            }

        }
        private void SetWishListButtonVisibility()
        {
            WhisListButton.IsVisible = false;

            if (_model.LoggedType != UserType.Consumer)
            {
                WhisListButton.IsVisible = false;
            }
        }

        private void SetParentalToggleButtonVisibility()
        {
            if (_model.LoggedType != UserType.Consumer)
            {
                WhisListButton.IsVisible = false;
            }
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
