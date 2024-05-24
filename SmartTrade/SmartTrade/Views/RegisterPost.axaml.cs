using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.IdentityModel.Tokens;
using SmartTrade.ViewModels;
using SmartTrade.Entities;

namespace SmartTrade.Views
{
    public partial class RegisterPost : UserControl
    {
        private RegisterPostModel? _model;
        private bool _hasErrors;
        private int _start = 6;
        public RegisterPost()
        {
            DataContext = _model = new RegisterPostModel();
            InitializeComponent();

            CategoryComboBox.ComboBox.ItemsSource = new[] { "Nutrition", "Clothing", "Toys", "Books" };
            CategoryComboBox.SelectedItem = "Nutrition";
            _model.AddStock();

            AddStockButton.Click += AddStock;
            CategoryComboBox.ComboBox.SelectionChanged += CategoryChanged;
            ConfirmButton.Click += OnConfirmButtonOnClick;
            CancelButton.Click += OnCancelButtonOnClick;

            Title.TextBox.TextChanged += CheckErrors;
            Description.TextBox.TextChanged += CheckErrors;
            Use.TextBox.TextChanged += CheckErrors;
            ProductName.TextBox.TextChanged += CheckErrors;
            MinAge.TextBox.TextChanged += CheckErrors;
        }

        private void OnCancelButtonOnClick(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.NavigateBack();
        }

        private async void OnConfirmButtonOnClick(object? sender, RoutedEventArgs e)
        {
            try
            {
                _model.CheckErrors();
            }

            catch (Exception exception)
            {
                StockErrorMessage.BringIntoView();
                StockErrorMessage.Text = exception.Message;
                return;
            }

            await _model.PublishPostAsync();
            SmartTradeNavigationManager.Instance.NavigateBack();
        }

        private void AddStock(object? sender, RoutedEventArgs e)
        {
            _model.AddStock();
        }

        private void CategoryChanged(object? sender, SelectionChangedEventArgs e)
        {
            switch (CategoryComboBox.SelectedItem)
            {
                case "Nutrition":
                    _model.Category = Category.Nutrition;
                    break;
                case "Clothing":
                    _model.Category = Category.Clothing;
                    break;
                case "Toys":
                    _model.Category = Category.Toy;
                    break;
                case "Books":
                    _model.Category = Category.Book;
                    break;
            }
        }

        private void CheckErrors(object? sender, TextChangedEventArgs e)
        {
            if (--_start >= 0)
            {
                ConfirmButton.IsEnabled = false;
                return;
            }
            _hasErrors = false | CheckTitle();
            _hasErrors |= CheckDescription();
            _hasErrors |= CheckUse();
            _hasErrors |= CheckProductName();
            _hasErrors |= CheckMinimumAge();

            ConfirmButton.IsEnabled = !_hasErrors;
        }

        private bool CheckTitle()
        {
            if (Title.Text.IsNullOrEmpty())
            {
                Title.ErrorText = "Please enter a Title";
                return true;
            }

            Title.ErrorText = "";
            return false;
        }

        private bool CheckDescription()
        {
            if (Description.Text.IsNullOrEmpty())
            {
                Description.ErrorText = "Please enter a Title";
                return true;
            }

            Description.ErrorText = "";
            return false;
        }

        private bool CheckUse()
        {
            if (Use.Text.IsNullOrEmpty())
            {
                Use.ErrorText = "Please enter a Title";
                return true;
            }

            Use.ErrorText = "";
            return false;
        }

        private bool CheckProductName()
        {
            if (ProductName.Text.IsNullOrEmpty())
            {
                ProductName.ErrorText = "Please enter a Title";
                return true;
            }

            ProductName.ErrorText = "";
            return false;
        }

        private bool CheckMinimumAge()
        {
            if (MinAge.Text.IsNullOrEmpty())
            {
                MinAge.ErrorText = "Please enter a Title";
                return true;
            }

            MinAge.ErrorText = "";
            return false;
        }
    }
}
