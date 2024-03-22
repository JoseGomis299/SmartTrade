using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using SmartTrade.ViewModels;
using System.Collections.ObjectModel;
using SmartTradeLib.Entities;
using Avalonia.Media;

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
            _model.PublishPost();
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
