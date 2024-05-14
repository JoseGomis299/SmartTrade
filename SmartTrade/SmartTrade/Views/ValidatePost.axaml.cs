using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.IdentityModel.Tokens;
using SmartTrade.ViewModels;
using SmartTradeDTOs;

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

        public ValidatePost(PostDTO post)
        {
            DataContext = _model = new ValidatePostModel(post);
            InitializeComponent();

            var categories = new[] { "Nutrition", "Clothing", "Toys", "Books" };
            CategoryComboBox.ComboBox.ItemsSource = categories;
            CategoryComboBox.SelectedItem = categories[(int)_model.Category];

            ConfirmButton.Click += ValidateAsync;
            RejectButton.Click += Reject;
            CancelButton.Click += Cancel;

            if (_model.Logged.IsAdmin)
            {
                RejectButton.Content = "Reject";
                ConfirmButton.Content = "Validate";
                ViewTitle.Text = "Validate Post";
            }
            else
            {
                RejectButton.Content = "Remove Post";
                ConfirmButton.Content = "Confirm";
                ViewTitle.Text = "Edit Post";
            }
        }

        private void Cancel(object? sender, RoutedEventArgs e)
        {
            SmartTradeNavigationManager.Instance.NavigateBack();
        }

        private async void Reject(object? sender, RoutedEventArgs e)
        {
            await _model.RejectPostAsync();
        }

        private async void ValidateAsync(object? sender, RoutedEventArgs e)
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

            if (_model.Description.IsNullOrEmpty())
            {
                Use.BringIntoView();
                Use.Focus();
                Use.ErrorText = "How to use/consume cannot be empty";
                hasErrors = true;
            }

            if (_model.ProductName.IsNullOrEmpty())
            {
                ProductName.BringIntoView();
                ProductName.Focus();
                ProductName.ErrorText = "ProductView name cannot be empty";
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

           await _model.EditPostAsync();
        }

        private void ClearErrors()
        {
            Title.ErrorText = "";
            Description.ErrorText = "";
            ProductName.ErrorText = "";
            MinAge.ErrorText = "";
            Use.ErrorText = "";
        }
    }
}
