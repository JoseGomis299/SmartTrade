using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.IdentityModel.Tokens;
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

            var categories = new[] { "Nutrition", "Clothing", "Toys", "Books" };
            CategoryComboBox.ComboBox.ItemsSource = categories;
            CategoryComboBox.SelectedItem = categories[(int) _model.Category];
        }

        public ValidatePost(Post post)
        {
            DataContext = _model = new ValidatePostModel(post);
            InitializeComponent();

            var categories = new[] { "Nutrition", "Clothing", "Toys", "Books" };
            CategoryComboBox.ComboBox.ItemsSource = categories;
            CategoryComboBox.SelectedItem = categories[(int)_model.Category];

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
            ClearErrors();
            bool hasErrors = false;

            if (_model.Title.IsNullOrEmpty())
            {
                Title.BringIntoView();
                Title.Focus();
                Title.ErrorText = "Title cannot be empty";
                hasErrors = true;
            }

            if (_model.Description.IsNullOrEmpty())
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

            if (_model.MinimumAge.IsNullOrEmpty())
            {
                MinAge.BringIntoView();
                MinAge.Focus();
                MinAge.ErrorText = "Minimum age cannot be empty";
                hasErrors = true;
            }

            if (hasErrors) return;

            _model.ValidatePost();
        }

        private void ClearErrors()
        {
            Title.ErrorText = "";
            Description.ErrorText = "";
            ProductName.ErrorText = "";
            MinAge.ErrorText = "";
        }
    }
}
