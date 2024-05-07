using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ReactiveUI;
using SmartTrade.Views;

namespace SmartTrade.ViewModels;

public class ShoppingCartModel : ViewModelBase
{
    public string? ShippingCost { get; set; }
    public string? SubTotal { get; set; }
    public string? Total { get; set; }
    public ObservableCollection<CartItemModel> Products { get; set; }
    public ShoppingCartModel()
    {
        Products = new ObservableCollection<CartItemModel>();

        foreach (var item in Service.CartItems)
        {
            Products.Add(new CartItemModel(item, this));
        }

        Service.OnCartChanged += Calculate;
        Calculate();
    }


    public void Calculate()
    {
        float subTotal = 0;
        float shippingCost = 0;

        foreach (var item in Products)
        {
            subTotal += float.Parse(item.Price.Substring(0, item.Price.Length-1)) * int.Parse(item.Quantity);
            shippingCost += float.Parse(item.ShippingCost.Substring(0, item.ShippingCost.Length-1));
        }

        SubTotal = subTotal + "€";
        ShippingCost = shippingCost + "€";
        Total = (subTotal + shippingCost) + "€";

        this.RaisePropertyChanged(nameof(SubTotal));
        this.RaisePropertyChanged(nameof(ShippingCost));
        this.RaisePropertyChanged(nameof(Total));
    }

    public void UnSubscribeFromCartNotifications()
    {
        Service.OnCartChanged -= Calculate;
    }

    public async Task BuyItemsAsync()
    {
        if (Service.Logged == null)
        {
            SmartTradeNavigationManager.Instance.NavigateWithButton(typeof(Login), 2, 2, out _);
            return;
        }

        foreach (var item in Products)
        {
            await Service.BuyItemAsync(item.Post, item.Offer, int.Parse(item.Quantity));
            await Service.DeleteItemFromCartAsync(item.Offer.Id);
        }

        SmartTradeNavigationManager.Instance.MainView.ReinitializeHomeNextTime = true;
        Products.Clear();
        this.RaisePropertyChanged(nameof(Products));
        Calculate();
    }
}