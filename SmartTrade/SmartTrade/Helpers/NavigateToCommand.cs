using System;
using Avalonia.Controls;

namespace SmartTrade.Navigation;

public class NavigateToCommand : ICommand 
{
    private Type? _viewType;
    private ContentControl? _view;
    private ContentControl? _previousView;
    private Navigator _navigator;

    public NavigateToCommand(Navigator navigator, Type viewType)
    {
        _navigator = navigator;
        _viewType = viewType;
        _previousView = _navigator.CurrentView;
    }

    public NavigateToCommand(Navigator navigator, ContentControl view)
    {
        _navigator = navigator;
        _view = view;
        _previousView = _navigator.CurrentView;
    }

    public NavigateToCommand(NavigateToCommand command, ContentControl view)
    {
        _navigator = command._navigator;
        _viewType = command._viewType;
        _previousView = command._previousView;
        _view = view;
    }

    public void Execute()
    {
        if (_viewType != null)
        {
            _navigator.NavigateTo(_viewType);
        }
        else
        {
            _navigator.NavigateTo(_view);
        }
    }

    public void UnExecute()
    {
        if(_previousView != null)
            _navigator.NavigateTo(_previousView);
    }
}