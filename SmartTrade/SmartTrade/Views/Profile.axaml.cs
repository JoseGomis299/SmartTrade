using Avalonia.Controls;
using SmartTrade.ViewModels;

namespace SmartTrade.Views
{
    public partial class Profile : UserControl
    {
        public Profile()
        {
            DataContext = new ProfileModel();
            InitializeComponent();

            LogoutButton.Click += LogOut;

            if (SmartTradeService.Instance.Logged != null && SmartTradeService.Instance.Logged.IsSeller)
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
            SmartTradeService.Instance.LogOut();
            await SmartTradeNavigationManager.Instance.MainView.ShowCatalogReinitializingAsync();
        }
    }
}
