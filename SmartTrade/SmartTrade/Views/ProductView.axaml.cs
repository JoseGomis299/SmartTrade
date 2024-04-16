using Avalonia.Controls;
using SmartTrade;
using SmartTradeDTOs;
using SmartTrade.Entities;
using SmartTrade.ViewModels;


namespace SmartTrade.Views
{
    public partial class ProductView : UserControl
    {
        public ProductView() 
        {
            InitializeComponent();
        }

        public ProductView(PostDTO post)
        {
            DataContext = new ProductViewModel(post);
            InitializeComponent();
        }
    }
}
