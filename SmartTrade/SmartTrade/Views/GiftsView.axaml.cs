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
    public partial class GiftsView : RefreshableUserControl
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

            _addImage = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/Add.png")));
            AddImage.Source = _addImage;

            _editImage = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/EditDark.png")));
            EditImage.Source = _editImage;

            _removeImage = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/Remove.png")));
            RemoveImage.Source = _removeImage;
        }

        protected override void Refresh()
        {
            ComboBoxGiftList.SelectedIndex = -1;
            _model.UpdateView();
            ComboBoxGiftList.SelectedIndex = 0;

            ComboBoxGiftList.SelectionChanged -= OnComboBoxGiftListOnSelectionChanged;
            ComboBoxGiftList.SelectionChanged += OnComboBoxGiftListOnSelectionChanged;
        }

        protected override void Dispose()
        {
            base.Dispose();
            _model.Dispose();
        }

        private void AddButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.MainView.ShowPopUp(new AddGiftListView(this));
        }
        
        private void EditButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.MainView.ShowPopUp(new EditGiftListView(this, _model.GiftLists[_model.ComboBoxIndex]));
        }

        private void OnComboBoxGiftListOnSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            _model.UpdateView(((ComboBox)sender).SelectedIndex);
        }

        private void RemoveButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ComboBoxGiftList.SelectedIndex = -1;
            _model.RemoveGiftList();
            ComboBoxGiftList.SelectedIndex = Math.Min(_model.ComboBoxIndex, _model.GiftListsNames.Count - 1);
        }

        private void BuyItems(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            return;
        }

        public void AddGiftList(string name, DateOnly? date)
        {
            ComboBoxGiftList.SelectedIndex = -1;
            _model.AddGiftList(name, date);
            ComboBoxGiftList.SelectedIndex = _model.GiftListsNames.Count - 1;
        }

        public void EditGiftList(string name, DateOnly? date)
        {
            string oldName = _model.GiftListsNames[_model.ComboBoxIndex];

            ComboBoxGiftList.SelectedIndex = -1;
            _model.EditGiftList(oldName, name, date);
            ComboBoxGiftList.SelectedIndex = _model.ComboBoxIndex;
        }

        public void SetIndexOnList(string giftListName)
        {
            ComboBoxGiftList.SelectedIndex = _model.GiftLists.FindIndex(g => g.Name == giftListName);
        }
    }
}
