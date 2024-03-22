using SmartTradeLib.BusinessLogic;

namespace SmartTrade.ViewModels;

public class MainViewModel : ViewModelBase
{
    public static ISmartTradeService SmartTradeService { get; } = new SmartTradeService();
    public string Greeting => "Welcome to Avalonia!";
}
