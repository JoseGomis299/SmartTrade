using System;
using Avalonia.Controls;

namespace GetStartedProject;

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
    }

    public NavigateToCommand(Navigator navigator, ContentControl view)
    {
        _navigator = navigator;
        _view = view;
    }

    public void Execute()
    {
        _previousView = _navigator.CurrentView;

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