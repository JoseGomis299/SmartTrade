﻿using System.Collections.ObjectModel;
using System.Collections.Generic;
using SmartTradeDTOs;
using Avalonia.Media.Imaging;
using ReactiveUI;

namespace SmartTrade.ViewModels;

public class GiftsModel : ViewModelBase
{
    public string? ShippingCost { get; set; }
    public string? SubTotal { get; set; }
    public string? Total { get; set; }
    public ObservableCollection<CartItemModel> Products { get; set; }

    private Dictionary<string, SimplePostDTO> GiftLists;
    public GiftsModel()
    {
        Products = new ObservableCollection<CartItemModel>();
        GiftLists = new Dictionary<string, SimplePostDTO>();

        foreach (var item in Service.CartItems)
        {
            //Products.Add(new CartItemModel(item, this));
        }

        Service.OnCartChanged += Calculate;
        Calculate();
    }

    ~GiftsModel()
    {
        
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
}