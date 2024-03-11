using Avalonia.Controls;
using GetStartedProject;

namespace SmartTrade.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        NavigationManager<ViewNavigator>.Initialize(this, typeof(ProductCatalog));
    }
}
