using System;
using System.Collections.Generic;
using Avalonia.Controls;
using SmartTrade.Navigation;
using SmartTrade.Services;
using SmartTrade.Views;

namespace SmartTrade;

public class NavigationManager
{
    public Navigator? Navigator;
    protected Stack<ICommand> Commands = new();

    protected static NavigationManager _instance;
    public static NavigationManager Instance => _instance ??= new NavigationManager();

    protected NavigationManager()
    {
        EventBus.RegisterEvent("OnNavigate");
    }

    public virtual void Initialize(ContentControl mainView, ContentControl targetView)
    {
        Navigator = new ViewNavigator(mainView);

        ICommand command = new NavigateToCommand(Navigator, targetView);
        command.Execute();

        EventBus.Publish("OnNavigate", targetView.GetType());
    }

    public virtual void NavigateTo(Type targetViewType) 
    {
        if(Navigator == null)
            throw new InvalidOperationException("Navigator not initialized");

        ICommand command = new NavigateToCommand(Navigator, targetViewType);
        Commands.Push(command);
        command.Execute();

        EventBus.Publish("OnNavigate", targetViewType);
    }

    public virtual void NavigateTo(ContentControl targetView)
    {
        if (Navigator == null)
            throw new InvalidOperationException("Navigator not initialized");

        ICommand command = new NavigateToCommand(Navigator, targetView);
        Commands.Push(command);
        command.Execute();

        EventBus.Publish("OnNavigate", targetView.GetType());
    }

    public virtual void NavigateToWithoutSaving(ContentControl targetView)
    {
        if (Navigator == null)
            throw new InvalidOperationException("Navigator not initialized");

        ICommand command = new NavigateToCommand(Navigator, targetView);
        command.Execute();

        EventBus.Publish("OnNavigate", targetView.GetType());
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

        EventBus.Publish("OnNavigate", targetView.GetType());
    }

    public virtual bool NavigateBack()
    {
        if (Commands.Count > 0)
        {
            var command = Commands.Pop();
            command.UnExecute();

            EventBus.Publish("OnNavigate", Navigator.CurrentView.GetType());
            return true;
        }

        return false;
    }

    protected void InvokeOnNavigate(Type viewType) => EventBus.Publish("OnNavigate", viewType);

}