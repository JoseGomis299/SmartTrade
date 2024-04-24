using System.Collections.ObjectModel;

namespace SmartTrade.ViewModels;

public class ShoppingCartModel : ViewModelBase
{
    public ObservableCollection<CartItemModel> Products { get; set; }
    public ShoppingCartModel()
    {
        Products = new ObservableCollection<CartItemModel>();

        foreach (var item in Service.CartItems)
        {
            Products.Add(new CartItemModel(item, this));
        }
    }
}