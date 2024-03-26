using Avalonia.Controls;
using System;
using System.Linq;
using Avalonia.Input;
using Avalonia.VisualTree;
using SmartTradeLib.Entities;
using SmartTradeLib.Persistence;

namespace SmartTrade.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        NavigationManager.OnNavigate += HandleNavigation;
        InitializeComponent();

        IDAL dal = new EntityFrameworkDAL(new SmartTradeContext());
        NavigationManager.Initialize(ViewContent, new ValidatePost(dal.GetAll<Post>().First()));
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
