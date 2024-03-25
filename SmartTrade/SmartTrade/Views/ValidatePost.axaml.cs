using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using SmartTrade.ViewModels;
using SmartTradeLib.Entities;

namespace SmartTrade.Views
{
    public partial class ValidatePost : UserControl
    {
        private ValidatePostModel? _model;

        public ValidatePost()
        {
            DataContext = _model = new ValidatePostModel();
            InitializeComponent();

            CategoryComboBox.ComboBox.ItemsSource = new[] { "Nutrition", "Clothing", "Toys", "Books" };
        }

        public ValidatePost(Post post)
        {
            DataContext = _model = new ValidatePostModel(post);
            InitializeComponent();

            CategoryComboBox.ComboBox.ItemsSource = new[] { "Nutrition", "Clothing", "Toys", "Books" };
            CategoryComboBox.SelectedItem = _model.Category.ToString();

            ConfirmButton.Click += Validate;
            RejectButton.Click += Reject;
            CancelButton.Click += Cancel;
        }

        private void Cancel(object? sender, RoutedEventArgs e)
        {
            NavigationManager.NavigateBack();
        }

        private void Reject(object? sender, RoutedEventArgs e)
        {
            _model.RejectPost();
        }

        private void Validate(object? sender, RoutedEventArgs e)
        {
            _model.ValidatePost();
        }
    }
}
