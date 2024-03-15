using Avalonia.Controls;

namespace SmartTrade.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        NavigationManager.Initialize(this, typeof(ProductCatalog));
    }
}
