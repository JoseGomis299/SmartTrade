using Avalonia.Controls;
using SmartTrade.Services;
using SmartTrade.ViewModels;
using SmartTradeDTOs;

namespace SmartTrade.Views
{
    public partial class Profile : UserControl
    {
        private ProfileModel _model;
        public Profile()
        {
            DataContext = _model = new ProfileModel();
            InitializeComponent();

            LogoutButton.Click += LogOut;

            if (_model.LoggedType == UserType.Seller)
            {
                AddPostButton.IsVisible = true;
                AddPostButton.Click += (sender, args) =>
                {
                    SmartTradeNavigationManager.Instance.NavigateTo(new RegisterPost());
                };
            }
            else { AddPostButton.IsVisible = false; }
        }

        private async void LogOut(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            _model.LogOut();
            await SmartTradeNavigationManager.Instance.MainView.ShowCatalogReinitializingAsync();
        }
    }
}
