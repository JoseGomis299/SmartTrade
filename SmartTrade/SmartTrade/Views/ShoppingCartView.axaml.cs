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
            EventBus.Subscribe<int>(this, "OnChangeNavigationStack", ClearReferences);

            _model.onCartChanged += UpdateView;
            UpdateView();
        }

        private void UpdateView()
        {
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
            EventBus.UnsubscribeFromAllEvents(this);
            DataContext = null;
        }

        private void BuyItems(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if(_model.UserType == UserType.Consumer) 
                SmartTradeNavigationManager.Instance.NavigateTo(typeof(SelectAddressView));
            else
                SmartTradeNavigationManager.Instance.NavigateWithButton(typeof(Login), 2, 2, out _);
        }
    }
}
