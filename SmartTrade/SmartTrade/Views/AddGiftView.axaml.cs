using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using System.Collections.Generic;
using SmartTrade.ViewModels;
using System;

namespace SmartTrade.Views
{
    public partial class AddGiftView : UserControl
    {
        private Popup _popup;
        private ProductView ProductView;
        List<String> GiftListNames;
        public AddGiftView(ProductView view, List<string> giftListNames)
        {
            InitializeComponent();

            ProductView = view;
            GiftListNames = giftListNames;

            foreach (var name in giftListNames)
            {
                ComboBoxGiftList.Items.Add(name);
            }
            
            AcceptButton.Click += AcceptButton_Click;
            CancelButton.Click += CancelButton_Click;
            _popup = new Popup
            {
                Child = this,
                IsLightDismissEnabled = true
            };
        }

        public AddGiftView()
        {
            InitializeComponent();
        }

        private void AcceptButton_Click(object? sender, RoutedEventArgs e)
        {
            ProductView.AddGift(GiftListNames[ComboBoxGiftList.SelectedIndex]);
            SmartTradeNavigationManager.Instance.MainView.HidePopUp();
        }
        private void CancelButton_Click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.MainView.HidePopUp();
        }

        
    }
}
