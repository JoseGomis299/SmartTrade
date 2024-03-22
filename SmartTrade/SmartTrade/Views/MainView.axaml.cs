using Avalonia.Controls;
using System;

namespace SmartTrade.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        NavigationManager.OnNavigate += HandleNavigation;
        InitializeComponent();
        NavigationManager.Initialize(ViewContent, typeof(RegisterPost));
    }

    private void HandleNavigation(Type type)
    {

    }
}
