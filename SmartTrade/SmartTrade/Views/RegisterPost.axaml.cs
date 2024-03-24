using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using SmartTrade.ViewModels;
using System.Collections.ObjectModel;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml.Templates;
using SmartTradeLib.Entities;
using Avalonia.Media;
using Microsoft.IdentityModel.Tokens;
using ReactiveUI;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.VisualTree;
using Avalonia;

namespace SmartTrade.Views
{
    public partial class RegisterPost : UserControl
    {
        private RegisterPostModel? _model;
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
        }

        private void OnConfirmButtonOnClick(object? sender, RoutedEventArgs e)
        {
            ClearErrors();
            bool hasErrors = false;

            if (_model.Title.IsNullOrEmpty())
            {
                Title.BringIntoView();
                Title.Focus();
                Title.ErrorText = "Title cannot be empty";
                hasErrors = true;
            }

            if(_model.Description.IsNullOrEmpty())
            {
                Description.BringIntoView();
                Description.Focus();
                Description.ErrorText = "Description cannot be empty";
                hasErrors = true;
            }

            if (_model.ProductName.IsNullOrEmpty())
            {
                ProductName.BringIntoView();
                ProductName.Focus();
                ProductName.ErrorText = "Product name cannot be empty";
                hasErrors = true;
            }


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

            if (hasErrors) return;

            _model.PublishPost();
        }

        private void ClearErrors()
        {
            Title.ErrorText = "";
            Description.ErrorText = "";
            ProductName.ErrorText = "";
            StockErrorMessage.Text = "";
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
    }
}
