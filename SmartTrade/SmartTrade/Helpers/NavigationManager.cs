using System;
using System.Collections.Generic;
using Avalonia.Controls;
using SmartTrade.Navigation;

namespace SmartTrade;

public class NavigationManager
{
    private static Navigator? _navigator;
    private static Stack<ICommand> _commands = new();

    public static void Initialize(ContentControl mainView, Type targetViewType)
    {
        if (mainView is UserControl) _navigator = new ViewNavigator(mainView);

        NavigateTo(targetViewType);
    }

    public static void Initialize(ContentControl mainView, ContentControl targetView)
    {
        if (mainView is UserControl) _navigator = new ViewNavigator(mainView);

        NavigateTo(targetView);
    }

    public static void NavigateTo(Type targetViewType) 
    {
        if(_navigator == null)
            throw new InvalidOperationException("Navigator not initialized");

        ICommand command = new NavigateToCommand(_navigator, targetViewType);
        _commands.Push(command);
        command.Execute();
    }

    public static void NavigateTo(ContentControl targetView)
    {
        if (_navigator == null)
            throw new InvalidOperationException("Navigator not initialized");

        ICommand command = new NavigateToCommand(_navigator, targetView);
        _commands.Push(command);
        command.Execute();
    }

    public static void NavigateBack()
    {
        if (_commands.Count > 0)
        {
            var command = _commands.Pop();
            command.UnExecute();
        }
    }

}