using System;
using System.Collections.Generic;
using Avalonia.Controls;
using SmartTrade.Navigation;

namespace SmartTrade;

public class NavigationManager
{
    public event Action<Type> OnNavigate;

    protected Navigator? Navigator;
    protected Stack<ICommand> Commands = new();

    protected static NavigationManager _instance;
    public static NavigationManager Instance => _instance ??= new NavigationManager();

    public virtual void Initialize(ContentControl mainView, ContentControl targetView)
    {
        Navigator = new ViewNavigator(mainView);

        ICommand command = new NavigateToCommand(Navigator, targetView);
        command.Execute();

        OnNavigate?.Invoke(targetView.GetType());
    }

    public virtual void NavigateTo(Type targetViewType) 
    {
        if(Navigator == null)
            throw new InvalidOperationException("Navigator not initialized");

        ICommand command = new NavigateToCommand(Navigator, targetViewType);
        Commands.Push(command);
        command.Execute();

        OnNavigate?.Invoke(targetViewType);
    }

    public virtual void NavigateTo(ContentControl targetView)
    {
        if (Navigator == null)
            throw new InvalidOperationException("Navigator not initialized");

        ICommand command = new NavigateToCommand(Navigator, targetView);
        Commands.Push(command);
        command.Execute();

        OnNavigate?.Invoke(targetView.GetType());
    }

    public virtual void NavigateToOverriding(ContentControl targetView)
    {
        if (Navigator == null)
            throw new InvalidOperationException("Navigator not initialized");

        ICommand command = new NavigateToCommand(Navigator, targetView);
        if (Commands.Count > 0 && Navigator.CurrentView.GetType() == targetView.GetType())
        {
            command = new NavigateToCommand((NavigateToCommand)Commands.Peek(), targetView);
            Commands.Pop();
        }
        Commands.Push(command);
        command.Execute();

        OnNavigate?.Invoke(targetView.GetType());
    }

    public virtual bool NavigateBack()
    {
        if (Commands.Count > 0)
        {
            var command = Commands.Pop();
            command.UnExecute();

            OnNavigate?.Invoke(Navigator.CurrentView.GetType());
            return true;
        }

        return false;
    }

    protected void InvokeOnNavigate(Type viewType) => OnNavigate?.Invoke(viewType);

}