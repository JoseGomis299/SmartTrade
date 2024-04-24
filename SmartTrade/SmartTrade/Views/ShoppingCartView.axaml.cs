using Avalonia.Controls;
using SmartTrade.ViewModels;

namespace SmartTrade.Views
{
    public partial class ShoppingCartView : UserControl
    {
        public ShoppingCartView()
        {
            DataContext = new ShoppingCartModel();
            InitializeComponent();
        }
    }
}
