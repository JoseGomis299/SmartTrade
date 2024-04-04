using Avalonia.Controls;
using SmartTrade.ViewModels;

namespace SmartTrade.Views
{
    public partial class ProductCatalog : UserControl
    {
        public ProductCatalog()
        {
            DataContext = new ProductCatalogModel();
            InitializeComponent();  
        }
    }
}
