using System.Collections.Generic;
using ReactiveUI;
using System.Collections.ObjectModel;
using SmartTrade.Entities;
using SmartTrade.Views;
using System.Threading.Tasks;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels;

public class CheckoutModel : ViewModelBase
{
    public List<CartItemModel> CartItems { get; set; }
    public ObservableCollection<ProductModel> CartProducts { get; set; }
    public string? DeliveryStreetAndNumber { get; }
    public string? DeliveryDoor { get; }
    public string? DeliveryProvinceAndCity { get; }
    public string? BillingStreetAndNumber { get; }
    public string? BillingDoor { get; }
    public string? BillingProvinceAndCity { get; }
    public string? PaymentMethod { get; }
    public string? PaymentMethodType { get; }
    public string? ShippingCost { get; private set; }
    public string? SubTotal { get; private set; }
    public string? Total { get; private set; }

    public CheckoutModel()
    {
        CartItems = new List<CartItemModel>();
        CartProducts = new ObservableCollection<ProductModel>();

        if (Service.CartItems == null)
        {
            return;
        }

        foreach (var item in Service.CartItems)
        {
            CartItems.Add(new CartItemModel(item));

            for (int i = 0; i < item.Quantity; i++)
            {
                CartProducts.Add(new ProductModel(new SimplePostDTO(item.Post)));
            }
        }

        this.RaisePropertyChanged(nameof(CartProducts));
    }

    public CheckoutModel(Address selectedAddress, Address selectedBillingAddress, PaymentMethodModel bizum, PaymentMethodModel creditCard, PaymentMethodModel paypal) : this()
    {
        DeliveryStreetAndNumber = selectedAddress.Street + ", " + selectedAddress.Number;
        DeliveryDoor = "Door " + selectedAddress.Door;
        DeliveryProvinceAndCity = selectedAddress.Province + ", " + selectedAddress.City + " " + selectedAddress.PostalCode;

        BillingStreetAndNumber = selectedBillingAddress.Street + ", " + selectedBillingAddress.Number;
        BillingDoor = "Door " + selectedBillingAddress.Door;
        BillingProvinceAndCity = selectedBillingAddress.Province + ", " + selectedBillingAddress.City + " " + selectedBillingAddress.PostalCode;

        PaymentMethodType = creditCard != null ? "Credit Card" : paypal != null ? "Paypal" : bizum != null ? "Bizum" : "";
        PaymentMethod = PaymentMethodType switch
        {
            "Credit Card" => $"{creditCard.Name}\n{creditCard.Number}",
            "Paypal" => $"{paypal.Name}",
            "Bizum" => $"{bizum.Number}",
        };

        Calculate();
    }


    public void Calculate()
    {
        float subTotal = 0;
        float shippingCost = 0;

        foreach (var item in CartItems)
        {
            subTotal += float.Parse(item.Price.Substring(0, item.Price.Length - 1)) * int.Parse(item.Quantity);
            shippingCost += float.Parse(item.ShippingCost.Substring(0, item.ShippingCost.Length - 1));
        }

        SubTotal = subTotal + "€";
        ShippingCost = shippingCost + "€";
        Total = (subTotal + shippingCost) + "€";

        this.RaisePropertyChanged(nameof(SubTotal));
        this.RaisePropertyChanged(nameof(ShippingCost));
        this.RaisePropertyChanged(nameof(Total));
    }

    public async Task CompleteOrder()
    {
        if (Service.Logged == null)
        {
            SmartTradeNavigationManager.Instance.NavigateWithButton(typeof(Login), 2, 2, out _);
            return;
        }

        foreach (var item in CartItems)
        {
            await Service.BuyItemAsync(item.Post, item.Offer, int.Parse(item.Quantity), int.Parse(item.EstimatedTime));
            await Service.DeleteItemFromCartAsync(item.Offer.Id);
        }

        SmartTradeNavigationManager.Instance.MainView.ReinitializeHomeNextTime = true;
        CartItems.Clear();
        this.RaisePropertyChanged(nameof(CartItems));
        Calculate();
    }
}