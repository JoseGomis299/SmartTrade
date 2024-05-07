using System.Collections.ObjectModel;
using System.Collections.Generic;
using SmartTradeDTOs;
using Avalonia.Media.Imaging;
using ReactiveUI;
using System;
using DynamicData;
using System.Linq;

namespace SmartTrade.ViewModels;

public class GiftsModel : ViewModelBase
{
    public string? ShippingCost { get; set; }
    public string? SubTotal { get; set; }
    public string? Total { get; set; }
    public string? NoElementsText { get; set; }
    public bool? NoElementsIsVisible { get; set; }
    public bool? AddButtonIsVisible { get; set; }
    public bool? EditButtonIsVisible { get; set; }
    public bool? RemoveButtonIsVisible { get; set; }
    public int ComboBoxIndex {  get; set; }
    public ObservableCollection<CartItemModel> Gifts { get; set; }
    List<GiftListDTO>? GiftLists => Service.GiftLists;
    public ObservableCollection<string> GiftListsNames { get; set; }

    public GiftsModel()
    {
        Gifts = new ObservableCollection<CartItemModel>();
        GiftListsNames = new ObservableCollection<string>();
        ComboBoxIndex = 0;

        Service.OnGiftListsChanged += UpdateView;
        Service.OnGiftsChanged += UpdateView;
        Service.OnGiftsChanged += Calculate;
        Calculate();
        UpdateView();
    }

    ~GiftsModel()
    {
        
    }

    public void UpdateView()
    {
            GiftListsNames = new ObservableCollection<string>();

            foreach (GiftListDTO giftList in this.GiftLists)
            {
                GiftListsNames.Add(giftList.Name);
            }

            foreach (GiftDTO gift in GiftLists[ComboBoxIndex].Gifts)
            {
                //Gifts.Add(new CartItemModel());
            }
    }

    public void Calculate()
    {
        float subTotal = 0;
        float shippingCost = 0;

        foreach (var item in Gifts)
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

    public void UnSubscribeFromGiftsNotifications()
    {
        Service.OnGiftListsChanged -= UpdateView;
        Service.OnGiftsChanged -= UpdateView;
        Service.OnGiftsChanged -= Calculate;
    }

    public void AddGiftList(string name, DateOnly? date)
    {
        if(GiftLists.FindIndex(gl => gl.Name == name) != -1) { throw new Exception("A gift list with the same name already exists"); }
        Service.AddGiftListAsync(name,date);
    }

    public void RemoveGiftList()
    { 
        Service.RemoveGiftListAsync( GiftLists[ComboBoxIndex].Name);
    }
}