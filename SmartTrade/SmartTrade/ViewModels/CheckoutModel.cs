using System.Collections.Generic;
using ReactiveUI;
using System.Collections.ObjectModel;
using SmartTrade.Entities;
using SmartTrade.Views;
using System.Threading.Tasks;
using SmartTrade.Helpers;
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

    private Address _billingAddress;
    private Address _deliveryAddress;

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

    public CheckoutModel(CheckOutData data) : this()
    {
        _deliveryAddress = data.DeliveryAddress;
        _billingAddress = data.BillingAddress;

        DeliveryStreetAndNumber = data.DeliveryAddress.Street + ", " + data.DeliveryAddress.Number;
        DeliveryDoor = "Door " + data.DeliveryAddress.Door;
        DeliveryProvinceAndCity = data.DeliveryAddress.Province + ", " + data.DeliveryAddress.City + " " + data.DeliveryAddress.PostalCode;

        BillingStreetAndNumber = data.BillingAddress.Street + ", " + data.BillingAddress.Number;
        BillingDoor = "Door " + data.BillingAddress.Door;
        BillingProvinceAndCity = data.BillingAddress.Province + ", " + data.BillingAddress.City + " " + data.BillingAddress.PostalCode;

        PaymentMethodType =data.PaymentMethod.Type;
        PaymentMethod = PaymentMethodType switch
        {
            "Credit Card" => $"{data.PaymentMethod.Name}\n{data.PaymentMethod.Number}",
            "Paypal" => $"{data.PaymentMethod.Name}",
            "Bizum" => $"{data.PaymentMethod.Number}",
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
        int currentlyLoading = LoadingScreenManager.Instance.StartLoading();

        if (Service.Logged == null)
        {
            SmartTradeNavigationManager.Instance.NavigateWithButton(typeof(Login), 2, 2, out _);
            return;
        }

        foreach (var item in CartItems)
        {
            await Service.BuyItemAsync(item.Post, item.Offer, int.Parse(item.Quantity), int.Parse(item.EstimatedTime.Substring(0, item.EstimatedTime.Length - 5)), _deliveryAddress, _billingAddress);
            await Service.DeleteItemFromCartAsync(item.Offer.Id);
        }

        LoadingScreenManager.Instance.StopLoading(currentlyLoading);
        SmartTradeNavigationManager.Instance.MainView.ShowPopUp(new PurchaseCompletedPopup());
    }
}