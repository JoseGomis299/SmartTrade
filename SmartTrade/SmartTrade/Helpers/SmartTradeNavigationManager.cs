using System;
using System.Collections.Generic;
using Avalonia.Controls;
using SmartTrade.Navigation;
using SmartTrade.Views;

namespace SmartTrade;

public class SmartTradeNavigationManager : NavigationManager
{
    protected Stack<ICommand> HomeCommands = new();
    protected Stack<ICommand> UserCommands = new();
    protected Stack<ICommand> CartCommands = new();

    protected int PreviousStack;

    protected static SmartTradeNavigationManager _instance;
    public static SmartTradeNavigationManager Instance => _instance ??= new SmartTradeNavigationManager();

    public override void Initialize(ContentControl mainView, ContentControl targetView)
    {
        Navigator = new ViewNavigator(mainView);
        NavigateWithButton(targetView, 0);
    }

    public void NavigateWithButton(ContentControl targetView, int previousButton)
    {
        if (Navigator == null)
            throw new InvalidOperationException("Navigator not initialized");

        if (targetView.GetType() == typeof(ProductCatalog) || targetView.GetType() == typeof(SellerCatalog) || targetView.GetType() == typeof(AdminCatalog))
        {
            Commands = HomeCommands;
        }
        else if (targetView.GetType() == typeof(Profile))
        {
            Commands = UserCommands;
        }
        else if (targetView.GetType() == typeof(ShoppingCartView))
        {
            Commands = CartCommands;
        }

        if(previousButton == GetButtonId(targetView)) Commands.Clear();

        if(Commands.TryPeek(out var top)) top.Execute();
        else
        {
            ICommand command = new NavigateToCommand(Navigator, targetView);
            command.Execute();
        }

        InvokeOnNavigate(targetView.GetType());
    }

    private int GetButtonId(ContentControl currentView)
    {
        if (currentView.GetType() == typeof(ProductCatalog) || currentView.GetType() == typeof(SellerCatalog) || currentView.GetType() == typeof(AdminCatalog))
        {
            return 0;
        }

        if (currentView.GetType() == typeof(Profile))
        {
            return 2;
        }
        return 1;
    }
}