using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using SmartTrade.ViewModels;
using System;

namespace SmartTrade.Views
{
    public partial class AddGiftView : UserControl
    {
        private Popup _popup;
        private ProductView ProductView;
        public AddGiftView(ProductView view)
        {
            InitializeComponent();

            ProductView = view;

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

        }
        private void CancelButton_Click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.MainView.HidePopUp();
        }

        
    }
}
