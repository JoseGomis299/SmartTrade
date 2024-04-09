using Avalonia.Controls;
using SmartTrade.ViewModels;

namespace SmartTrade.Views
{
    public partial class SellerCatalog : UserControl
    {
        public SellerCatalog()
        {
            DataContext = new SellerCatalogModel();
            InitializeComponent();
        }
    }
}
