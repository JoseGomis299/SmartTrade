using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using SmartTrade.ViewModels;
using System;

namespace SmartTrade.Views
{
    public partial class AddGiftListView : UserControl
    {
        private Popup _popup;
        private GiftsView GiftsView;
        public AddGiftListView(GiftsView view)
        {
            InitializeComponent();

            GiftsView = view;

            AcceptButton.Click += AcceptButton_Click;
            CancelButton.Click += CancelButton_Click;
            TextBoxName.TextBox.TextChanged += TextBox_TextChanged;
            _popup = new Popup
            {
                Child = this,
                IsLightDismissEnabled = true
            };
        }

        public AddGiftListView() { }

        private void TextBox_TextChanged(object? sender, TextChangedEventArgs e)
        {
            if (TextBoxName.Text == "")
            {
                TextBoxName.ErrorMessage.Text = "The name of the list can't be empty";
            }
            else
            {
                TextBoxName.ErrorMessage.Text = "";
            }
        }

        private void AcceptButton_Click(object? sender, RoutedEventArgs e)
        {
            if(TextBoxName.Text == "") { return; }
            
            DateTime? dateTime = CalendarDate.SelectedDate;
            try
            {
                if (dateTime.HasValue)
                {
                    GiftsView.AddGiftList(TextBoxName.Text, DateOnly.FromDateTime((DateTime)CalendarDate.SelectedDate));
                }
                else
                {
                    GiftsView.AddGiftList(TextBoxName.Text, null);
                }
            }
            catch(Exception ex) { TextBoxName.ErrorMessage.Text = ex.Message; }

            SmartTradeNavigationManager.Instance.MainView.HidePopUp();

        }
        private void CancelButton_Click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.MainView.HidePopUp();
        }

        
    }
}
