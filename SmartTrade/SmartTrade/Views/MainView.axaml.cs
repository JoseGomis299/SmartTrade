using Avalonia.Controls;
using GetStartedProject;

namespace SmartTrade.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        NavigationManager.Initialize(this, typeof(ProductCatalog));
    }
}
