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

            SetFieldsVisibility();
           
            ParentalToggleButton.IsChecked = _model.IsParentalControlEnabled;

            AddBizumButton.Click += AddBizum;
            AddCreditCardButton.Click += AddCreditCard;
            AddPaypalButton.Click += AddPaypal;
            AddAddressButton.Click += AddAddress;
        }

        private void SetFieldsVisibility()
        {
            SetWishListButtonVisibility();
            SetParentalToggleButtonVisibility();
            PaymentMethodsPanel.IsVisible = _model.LoggedType == UserType.Consumer;
            AddressesPanel.IsVisible = _model.LoggedType == UserType.Consumer;
        }

        private void AddBizum(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            AddBizumPopup popup = new AddBizumPopup();
            popup.onAccept = async (save) =>
            {
                await _model.AddBizumAsync(popup.GetBizum());
            };

            SmartTradeNavigationManager.Instance.MainView.ShowPopUp(popup);
        }

        private void AddCreditCard(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            AddCreditCardPopup popup = new AddCreditCardPopup();
            popup.onAccept = async (save) =>
            {
                await _model.AddCreditCardAsync(popup.GetCreditCard());
            };

            SmartTradeNavigationManager.Instance.MainView.ShowPopUp(popup);
        }

        private void AddPaypal(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            AddPaypalPopup popup = new AddPaypalPopup();
            popup.onAccept = async (save) =>
            {
                await _model.AddPaypalAsync(popup.GetPaypal());
            };

            SmartTradeNavigationManager.Instance.MainView.ShowPopUp(popup);
        }

        private void AddAddress(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            AddAddress popup = new AddAddress();
            popup.onAccept = async (save) =>
            {
                await _model.AddAddressAsync(popup.GetAddress());
            };

            SmartTradeNavigationManager.Instance.MainView.ShowPopUp(popup);
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
                ParentalToggleButton.IsVisible = false;
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
