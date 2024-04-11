using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using SmartTrade.ViewModels;
using SmartTrade.Views;

namespace SmartTrade;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView();

            await ((MainViewModel) singleViewPlatform.MainView.DataContext).InitializeAsync((MainView) singleViewPlatform.MainView);
        }

        base.OnFrameworkInitializationCompleted();
    }
}
