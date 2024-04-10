using System;
using System.Collections.Generic;
using Avalonia.Controls;
using SmartTrade.Navigation;
using SmartTrade.Views;

namespace SmartTrade;

public class SmartTradeNavigationManager : NavigationManager
{
    public event Action<int> OnChangeNavigationStack;

    protected Stack<ICommand> HomeCommands = new();
    protected Stack<ICommand> UserCommands = new();
    protected Stack<ICommand> CartCommands = new();

    protected static SmartTradeNavigationManager? _instance;
    public static SmartTradeNavigationManager Instance => _instance ??= new SmartTradeNavigationManager();

    public override void Initialize(ContentControl mainView, ContentControl targetView)
    {
        Navigator = new ViewNavigator(mainView);
        NavigateWithButton(targetView, 0, 0);
    }

    public bool NavigateWithButton(ContentControl targetView, int previousButton, int targetButton)
    {
        if (Navigator == null)
            throw new InvalidOperationException("Navigator not initialized");

        if (targetButton == 0)
        {
            Commands = HomeCommands;
            OnChangeNavigationStack?.Invoke(0);
        }
        else if (targetButton == 2)
        {
            Commands = UserCommands;
            OnChangeNavigationStack?.Invoke(2);
        }
        else if (targetButton == 1)
        {
            Commands = CartCommands;
            OnChangeNavigationStack?.Invoke(1);
        }

        if (previousButton == targetButton) Commands.Clear();

        if(Commands.TryPeek(out var top)) top.Execute();
        else
        {
            ICommand command = new NavigateToCommand(Navigator, targetView);
            if (previousButton != targetButton) command = new NavigateToCommand(Navigator, targetView.GetType());

            command.Execute();
        }

        InvokeOnNavigate(targetView.GetType());
        return previousButton == targetButton;
    }

    public void ReInitializeNavigation(ContentControl targetView)
    {
        if (Navigator == null)
            throw new InvalidOperationException("Navigator not initialized");

        if (targetView.GetType() == typeof(ProductCatalog) || targetView.GetType() == typeof(SellerCatalog) || targetView.GetType() == typeof(AdminCatalog))
        {
            HomeCommands.Clear();
            UserCommands.Clear();
            CartCommands.Clear();

            Commands = HomeCommands;
            OnChangeNavigationStack?.Invoke(0);
        }
        else {
            throw new InvalidOperationException("You can only go to a Catalog when reinitializing");
        }

        ICommand command = new NavigateToCommand(Navigator, targetView);
        command.Execute();

        InvokeOnNavigate(targetView.GetType());
    }
}