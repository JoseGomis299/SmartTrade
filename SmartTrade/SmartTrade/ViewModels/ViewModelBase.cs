using ReactiveUI;
using SmartTrade.Services;

namespace SmartTrade.ViewModels;

public class ViewModelBase : ReactiveObject
{
    protected static SmartTradeService? Service;

    public ViewModelBase()
    {
        if (Service == null)
        {
            var broker = new SmartTradeBroker();
            var proxy = new SmartTradeProxy();

            Service = new SmartTradeService(broker, proxy);
        }
    }
}
