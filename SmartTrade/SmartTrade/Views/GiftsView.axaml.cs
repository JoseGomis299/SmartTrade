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
        private Bitmap? _addImage;
        private Bitmap? _editImage;
        private Bitmap? _removeImage;

        public GiftsModel _model => (GiftsModel)DataContext;
        public GiftsView()
        {
            DataContext = new GiftsModel();
            InitializeComponent();

            AddButton.Click += AddButton_Click;
            EditButton.Click += EditButton_Click;
            RemoveButton.Click += RemoveButton_Click;
            CheckOutButton.Click += BuyItems;
            SmartTradeNavigationManager.Instance.OnChangeNavigationStack += ClearReferences;

            _addImage = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/Add.png")));
            AddImage.Source = _addImage;

            _editImage = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/EditDark.png")));
            EditImage.Source = _editImage;

            _removeImage = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/Remove.png")));
            RemoveImage.Source = _removeImage;

        }

        private void AddButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.MainView.ShowPopUp(new AddGiftListView(this));
        }

        private void EditButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.MainView.ShowPopUp(new EditGiftListView(this));
        }

        private void RemoveButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            _model.RemoveGiftList();
        }

        private void ClearReferences(int stack)
        {
            ((GiftsModel)DataContext).UnSubscribeFromGiftsNotifications();
            CheckOutButton.Click -= BuyItems;
            SmartTradeNavigationManager.Instance.OnChangeNavigationStack -= ClearReferences;
            DataContext = null;
        }

        private void BuyItems(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            return;
        }

        public void AddGiftList(string name, DateOnly? date)
        {
            _model.AddGiftList(name, date);
        }
    }
}
