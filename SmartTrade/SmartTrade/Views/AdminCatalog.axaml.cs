using Avalonia.Controls;
using SmartTrade.ViewModels;

namespace SmartTrade.Views
{
    public partial class AdminCatalog : UserControl
    {
        public AdminCatalog()
        {
            DataContext = new AdminCatalogModel();
            InitializeComponent();
        }
    }
}
