using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ReactiveUI;
using SmartTrade.Services;
using SmartTrade.Views;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels;

public class ShoppingCartModel : ViewModelBase
{
    public string? ShippingCost { get; set; }
    public string? SubTotal { get; set; }
    public string? Total { get; set; }
    public ObservableCollection<CartItemModel> Products { get; set; }
    public UserType UserType => Service.LoggedType;
    public event Action onCartChanged; 
    public ShoppingCartModel()
    {
        Products = new ObservableCollection<CartItemModel>();

        foreach (var item in Service.CartItems)
        {
            Products.Add(new CartItemModel(item, this));
        }

        EventBus.Subscribe(this, "OnCartChanged", Calculate);
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

        onCartChanged?.Invoke();
    }

    public void UnSubscribeFromCartNotifications()
    {
        EventBus.UnsubscribeFromAllEvents(this);
    }
}