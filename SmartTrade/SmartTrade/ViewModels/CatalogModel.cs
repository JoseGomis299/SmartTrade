using System.Threading.Tasks;

namespace SmartTrade.ViewModels;

public abstract class CatalogModel : ViewModelBase
{
    public abstract Task LoadProductsAsync();

}