using System;
using System.Collections.Generic;
using System.Linq;
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
    public MainView MainView { get; set; }
    public int CurrentStack { get; private set; }

    private SmartTradeNavigationManager() { }

    public override void Initialize(ContentControl mainView, ContentControl targetView)
    {
        Navigator = new ViewNavigator(mainView);
        NavigateWithButton(targetView.GetType(), 0, 0,out _);
    }

    public bool NavigateWithButton(Type? targetViewType, int previousButton, int targetButton, out ContentControl view, bool reinitilizeView = false)
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

        CurrentStack = targetButton;

        if (targetViewType == null)
        {
            view = Navigator.CurrentView;
            return false;
        }

        if (previousButton == targetButton) Commands.Clear();

        if(Commands.TryPeek(out var top)) top.Execute();
        else
        {
            var command = new NavigateToCommand(Navigator, targetViewType);
            if(reinitilizeView) command = new NavigateToCommand(Navigator,(ContentControl) Activator.CreateInstance(targetViewType));
            command.Execute();
        }

        view = Navigator.CurrentView;

        InvokeOnNavigate(view.GetType());
        return previousButton == targetButton;
    }

    public void ReInitializeNavigation(ContentControl targetView)
    {
        if (Navigator == null)
            throw new InvalidOperationException("Navigator not initialized");

        if (targetView.GetType() == typeof(ProductCatalog) || targetView.GetType() == typeof(SellerCatalog) || targetView.GetType() == typeof(AdminCatalog))
        {
            HomeCommands.Clear();
            CartCommands.Clear();
            UserCommands.Clear();

            CurrentStack = 0;
            Commands = HomeCommands;
            OnChangeNavigationStack?.Invoke(0);
        }
        else {
            throw new InvalidOperationException("You can only go to a Catalog or Login when reinitializing");
        }

        Navigator.Reinitialize();
        ICommand command = new NavigateToCommand(Navigator, targetView);
        command.Execute();

        InvokeOnNavigate(targetView.GetType());
    }

    public void AddToStack(ContentControl view, int stack)
    {
        if (stack == 0) HomeCommands.Push(new NavigateToCommand(Navigator, view));
        else if (stack == 1) CartCommands.Push(new NavigateToCommand(Navigator, view));
        else UserCommands.Push(new NavigateToCommand(Navigator, view));
    }

    public void ClearStack(int stack)
    {
        if (stack == 0) HomeCommands.Clear();
        else if (stack == 1) CartCommands.Clear();
        else UserCommands.Clear();
    }

    public bool IsViewOnStack(Type viewType, int stack)
    {
        if(stack == 0) return HomeCommands.Any(c => (c as NavigateToCommand).ViewType == viewType);
        if(stack == 1) return CartCommands.Any(c => (c as NavigateToCommand).ViewType == viewType);
        return UserCommands.Any(c => (c as NavigateToCommand).ViewType == viewType);
    }
}