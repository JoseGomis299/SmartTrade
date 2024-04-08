using Avalonia.Controls;
using System;
using Avalonia.Input;
using SmartTrade.ViewModels;

namespace SmartTrade.Views;

public partial class MainView : UserControl
{
    private MainViewModel _model;

    public MainView()
    {
        _model = new MainViewModel();
        NavigationManager.OnNavigate += HandleNavigation;
        InitializeComponent();

        NavigationManager.Initialize(ViewContent, new ProductCatalog());

        AutoCompleteBox.TextChanged += AutoCompleteBox_TextChanged;
        AutoCompleteBox.KeyDown += AutoCompleteBox_KeyDown;

        AutoCompleteBox.ItemsSource = _model.SearchAutoComplete;
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

    private void AutoCompleteBox_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key.Equals(Key.Enter))
        {
            NavigationManager.NavigateToOverriding(new SearchResult(_model.LoadProducts()));
        }
    }

    private void AutoCompleteBox_TextChanged(object? sender, TextChangedEventArgs e)
    {
        _model.SearchText = AutoCompleteBox.Text;
    }
}
