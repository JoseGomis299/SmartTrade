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
        private bool _hasErrors;
        private int _start = 6;

        public ValidatePost()
        {
            DataContext = _model = new ValidatePostModel();
            InitializeComponent();

            var categories = new[] { "Nutrition", "Clothing", "Toys", "Books" };
            CategoryComboBox.ComboBox.ItemsSource = categories;
            CategoryComboBox.SelectedItem = categories[(int)_model.Category];
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

            Title.TextBox.TextChanged += CheckErrors;
            Description.TextBox.TextChanged += CheckErrors;
            Use.TextBox.TextChanged += CheckErrors;
            ProductName.TextBox.TextChanged += CheckErrors;
            MinAge.TextBox.TextChanged += CheckErrors;
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
            await _model.EditPostAsync();
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
