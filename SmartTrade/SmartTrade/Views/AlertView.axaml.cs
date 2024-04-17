using Avalonia.Controls;
using SmartTradeDTOs;
using SmartTrade.ViewModels;

namespace SmartTrade.Views
{
    public partial class AlertView : UserControl
    {
        public AlertView() 
        {
            InitializeComponent();
        }
        public AlertView(ProductDTO products)
        {
            //DataContext = new AlertViewModel(products);
            InitializeComponent();
        }
    }
}
