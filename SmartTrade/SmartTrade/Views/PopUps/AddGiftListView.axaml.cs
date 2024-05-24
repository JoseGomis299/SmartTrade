using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using Microsoft.IdentityModel.Tokens;
using SmartTrade.ViewModels;
using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SmartTrade.Views
{
    public partial class AddGiftListView : UserControl
    {
        private GiftsView GiftsView;
        private bool _hasErrors;
        private int _start = 2;
        public AddGiftListView(GiftsView view)
        {
            InitializeComponent();

            GiftsView = view;

            AcceptButton.Click += AcceptButton_Click;
            CancelButton.Click += CancelButton_Click;
            TextBoxName.TextBox.TextChanged += CheckErrors;
            CalendarDate.TemplateApplied += SetComponentsCalendarDatePicker;
        }

        public AddGiftListView()
        {
            InitializeComponent();
        }

        private void AcceptButton_Click(object? sender, RoutedEventArgs e)
        {
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

                SmartTradeNavigationManager.Instance.MainView.HidePopUp();
            }
            catch(Exception ex) { TextBoxName.ErrorText = ex.Message;}

            

        }
        private void CancelButton_Click(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.MainView.HidePopUp();
        }

        private void CheckErrors(object? sender, TextChangedEventArgs e)
        {
            if (--_start >= 0)
            { 
                AcceptButton.IsEnabled = false;
                return;
            }
            _hasErrors = false | CheckName();
            _hasErrors |= CheckDate();

            AcceptButton.IsEnabled = !_hasErrors;
        }

        private void SetComponentsCalendarDatePicker(object? sender, Avalonia.Controls.Primitives.TemplateAppliedEventArgs e)
        {
            var textBoxField = sender.GetType().GetField("_textBox", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            TextBox textBox = (TextBox)textBoxField.GetValue(sender);
            textBox.TextChanged += CheckErrors;

            CalendarDate.BlackoutDates.AddDatesInPast();
        }

        private bool CheckName()
        {
            if (TextBoxName.Text.IsNullOrEmpty())
            {
                TextBoxName.ErrorText = "Please input your name";
                return true;
            }

            TextBoxName.ErrorText = "";
            return false;
        }

        private bool CheckDate()
        {
            string date = CalendarDate.FindDescendantOfType<TextBox>().Text;

            string pattern = @"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}$";
            if (!Regex.IsMatch(CalendarDate.Text, pattern) && !date.IsNullOrEmpty())
            {
                CalendarDateError.Text = "Please enter a valid date with format dd/MM/yyyy";
                CalendarDateError.IsVisible = true;
                return true;
            }

            CalendarDateError.Text = "";
            CalendarDateError.IsVisible = false;
            return false;
        }
    }
}
