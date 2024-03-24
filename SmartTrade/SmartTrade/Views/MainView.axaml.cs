using Avalonia.Controls;
using System;
using Avalonia.Input;
using Avalonia.VisualTree;

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
        if (type == typeof(RegisterPost))
        {
            SearchBar.IsVisible = false;
            return;
        }

        ResetVisibility();
    }
    private void ResetVisibility()
    {
        SearchBar.IsVisible = true;
        BottomBar.IsVisible = true;
    }
}
