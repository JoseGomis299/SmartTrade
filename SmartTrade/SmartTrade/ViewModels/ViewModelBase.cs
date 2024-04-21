using ReactiveUI;
using SmartTrade.Services;

namespace SmartTrade.ViewModels;

public class ViewModelBase : ReactiveObject
{
    protected SmartTradeService? Service => SmartTradeService.Instance;

    public ViewModelBase()
    {
     
    }
}
