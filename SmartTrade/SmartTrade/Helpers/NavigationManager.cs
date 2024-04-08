using System;
using System.Collections.Generic;
using Avalonia.Controls;
using SmartTrade.Navigation;

namespace SmartTrade;

public class NavigationManager
{
    public static event Action<Type> OnNavigate;

    private static Navigator? _navigator;
    private static Stack<ICommand> _commands = new();

    public static void Initialize(ContentControl mainView, Type targetViewType)
    {
       _navigator = new ViewNavigator(mainView);

       ICommand command = new NavigateToCommand(_navigator, targetViewType);
       command.Execute();

       OnNavigate?.Invoke(targetViewType);
    }

    public static void Initialize(ContentControl mainView, ContentControl targetView)
    {
        _navigator = new ViewNavigator(mainView);

        ICommand command = new NavigateToCommand(_navigator, targetView);
        command.Execute();

        OnNavigate?.Invoke(targetView.GetType());
    }

    public static void NavigateTo(Type targetViewType) 
    {
        if(_navigator == null)
            throw new InvalidOperationException("Navigator not initialized");

        ICommand command = new NavigateToCommand(_navigator, targetViewType);
        _commands.Push(command);
        command.Execute();

        OnNavigate?.Invoke(targetViewType);
    }

    public static void NavigateTo(ContentControl targetView)
    {
        if (_navigator == null)
            throw new InvalidOperationException("Navigator not initialized");

        ICommand command = new NavigateToCommand(_navigator, targetView);
        _commands.Push(command);
        command.Execute();

        OnNavigate?.Invoke(targetView.GetType());
    }

    public static void NavigateToOverriding(ContentControl targetView)
    {
        if (_navigator == null)
            throw new InvalidOperationException("Navigator not initialized");

        ICommand command = new NavigateToCommand(_navigator, targetView);
        if (_commands.Count > 0 && _navigator.CurrentView.GetType() == targetView.GetType())
        {
            command = new NavigateToCommand((NavigateToCommand)_commands.Peek(), targetView);
            _commands.Pop();
        }
        _commands.Push(command);
        command.Execute();

        OnNavigate?.Invoke(targetView.GetType());
    }

    public static bool NavigateBack()
    {
        if (_commands.Count > 0)
        {
            var command = _commands.Pop();
            command.UnExecute();

            OnNavigate?.Invoke(_navigator.CurrentView.GetType());
            return true;
        }

        return false;
    }

}