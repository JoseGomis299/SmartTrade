using System;
using Avalonia.Controls;

namespace SmartTrade.Navigation;

public abstract class Navigator
{
    public ContentControl MainView { get;}
    public ContentControl CurrentView => MainView.Content as ContentControl;
    public abstract void NavigateTo(Type viewType);
    public abstract void NavigateTo(ContentControl view);
    public abstract void Reinitialize();

    protected Navigator(ContentControl mainView)
    {
        MainView = mainView;
    }
}