using Avalonia.Controls;
using SmartTrade;
using SmartTradeLib.Entities;


namespace SmartTrade.Views
{
    public partial class ProductView : UserControl
    {
        public ProductView()
        {
            InitializeComponent();
        }

        public ProductView(Post post)
        {
            InitializeComponent();
           // DataContext = new ProductViewModel(post);
        }
    }
}
