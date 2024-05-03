using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using SmartTrade.ViewModels;
using System;

namespace SmartTrade.Views
{
    public partial class EditGiftListView : UserControl
    {
        private Popup _popup;
        public EditGiftListView()
        {
            InitializeComponent();

            AcceptButton.Click += AcceptButton_Click;
            CancelButton.Click += CancelButton_Click;
            _popup = new Popup
            {
                Child = this,
                IsLightDismissEnabled = true
            };
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