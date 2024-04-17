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
        }

        private async void LogOut(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            SmartTradeService.Instance.LogOut();
            await SmartTradeNavigationManager.Instance.MainView.ShowCatalogReinitializingAsync();
        }
    }
}
