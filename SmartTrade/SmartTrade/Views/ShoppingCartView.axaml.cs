using System;
using Avalonia.Controls;
using Microsoft.IdentityModel.Tokens;
using SmartTrade.ViewModels;
using SmartTradeDTOs;

namespace SmartTrade.Views
{
    public partial class ShoppingCartView : UserControl
    {
        private ShoppingCartModel _model;
        public ShoppingCartView()
        {
            DataContext = _model = new ShoppingCartModel();
            InitializeComponent();

            CheckOutButton.Click += BuyItems;
            SmartTradeNavigationManager.Instance.OnChangeNavigationStack += ClearReferences;

            if (_model.Products.IsNullOrEmpty())
            {
                PricePannel.IsVisible = false;
                EmptyText.IsVisible = true;
            }
            else
            {
                PricePannel.IsVisible = true;
                EmptyText.IsVisible = false;
            }
        }

        private void ClearReferences(int stack)
        {
            _model.UnSubscribeFromCartNotifications();
            CheckOutButton.Click -= BuyItems;
            SmartTradeNavigationManager.Instance.OnChangeNavigationStack -= ClearReferences;
            DataContext = null;
        }

        private async void BuyItems(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if(_model.UserType == UserType.Consumer) 
                SmartTradeNavigationManager.Instance.NavigateTo(new SelectAddressView());
            else
                SmartTradeNavigationManager.Instance.NavigateWithButton(typeof(Login), 2, 2, out _);

            // await ((ShoppingCartModel)DataContext).BuyItemsAsync();
        }
    }
}
