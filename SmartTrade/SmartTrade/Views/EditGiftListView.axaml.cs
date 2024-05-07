using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using SmartTrade.ViewModels;
using System;
using SmartTradeDTOs;

namespace SmartTrade.Views
{
    public partial class EditGiftListView : UserControl
    {
        private Popup _popup;
        private GiftsView _giftsView;

        public EditGiftListView(GiftsView view, GiftListDTO giftList)
        {
            InitializeComponent();

            _giftsView = view;

            AcceptButton.Click += AcceptButton_Click;
            CancelButton.Click += CancelButton_Click;
            TextBoxName.TextBox.TextChanged += TextBox_TextChanged;
            _popup = new Popup
            {
                Child = this,
                IsLightDismissEnabled = true
            };

            TextBoxName.Text = giftList.Name;
            CalendarDate.SelectedDate = giftList.Date?.ToDateTime(new TimeOnly(0, 0));
        }

        public EditGiftListView()
        {
            InitializeComponent();
        }
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
            if (TextBoxName.Text == "") { return; }

            DateTime? dateTime = CalendarDate.SelectedDate;
            try
            {
                if (dateTime.HasValue)
                {
                    _giftsView.EditGiftList(TextBoxName.Text, DateOnly.FromDateTime((DateTime)CalendarDate.SelectedDate));
                }
                else
                {
                    _giftsView.EditGiftList(TextBoxName.Text, null);
                }
                SmartTradeNavigationManager.Instance.MainView.HidePopUp();

            }
            catch (Exception ex) { TextBoxName.ErrorText = ex.Message; }
        }
        private void CancelButton_Click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.MainView.HidePopUp();
        }


    }
}
