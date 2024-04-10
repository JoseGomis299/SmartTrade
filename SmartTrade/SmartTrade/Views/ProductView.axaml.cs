using Avalonia.Controls;
using SmartTrade;
using SmartTradeDTOs;
using SmartTrade.Entities;


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
            InitializeComponent();
           // DataContext = new ProductViewModel(post);
        }
    }
}
