using DynamicData;
using System.Threading.Tasks;
using ReactiveUI;
using SmartTrade.Controls;
using SmartTrade.Services;

namespace SmartTrade.ViewModels;

public class ViewModelBase : ReactiveObject
{
    protected SmartTradeService? Service => SmartTradeService.Instance;

    public ViewModelBase()
    {
     
    }
}