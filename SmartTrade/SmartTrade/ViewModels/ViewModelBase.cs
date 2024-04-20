using ReactiveUI;
using SmartTrade.Services;

namespace SmartTrade.ViewModels;

public class ViewModelBase : ReactiveObject
{
    protected SmartTradeService Service;
    protected SmartTradeProxy Proxy;

    public ViewModelBase()
    {
        ServiceFactory factory = new();

        Service = factory.GetService();
        Proxy = factory.GetProxy();
    }
}
