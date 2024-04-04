using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace SmartTrade.Views
{
    public partial class ChooseProfile : UserControl
    {
        public ChooseProfile()
        {
            InitializeComponent();
            PurchaseButton.Click += PurchaseButton_Click;
            SellButton.Click += SellButton_Click;
        }

        private void PurchaseButton_Click(object? sender, RoutedEventArgs e)
        {
            NavigationManager.NavigateTo(new Register());
        }

        private void SellButton_Click(object? sender, RoutedEventArgs e)
        {
            NavigationManager.NavigateTo(new SellerRegister());

        }


    }
}
