using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using SmartTrade.Controls;
using SmartTrade.ViewModels;
using SmartTradeDTOs;

namespace SmartTrade.Views
{
    public partial class GiftsView : UserControl
    {
        private Bitmap? _editImage;
        public GiftsView()
        {
            DataContext = new GiftsModel();
            InitializeComponent();

            EditButton.Click += EditButton_Click;
            CheckOutButton.Click += BuyItems;
            SmartTradeNavigationManager.Instance.OnChangeNavigationStack += ClearReferences;

            _editImage = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/Edit.png")));
            EditImage.Source = _editImage;

        }

        private void EditButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.MainView.ShowPopUp(new EditGiftListView());
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
