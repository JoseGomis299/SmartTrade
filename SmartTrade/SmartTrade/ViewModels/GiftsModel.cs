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
    public ObservableCollection<GiftItemModel> Gifts { get; set; }
    public List<GiftListDTO>? GiftLists => Service.GiftLists;
    public ObservableCollection<string> GiftListsNames { get; set; }

    public GiftsModel()
    {
        Gifts = new ObservableCollection<GiftItemModel>();
        GiftListsNames = new ObservableCollection<string>();

        Service.OnGiftsChanged += Calculate;
    }

    public void UpdateView(int selectedIndex)
    {
        Gifts.Clear();

        if (Service.GiftLists == null) { return; }

        if (GiftLists.Count > selectedIndex && selectedIndex > -1)
        {
            ComboBoxIndex = selectedIndex;

            foreach (GiftDTO gift in GiftLists[selectedIndex].Gifts)
            {
                Gifts.Add(new GiftItemModel(gift, this));
            }
        }
        else ComboBoxIndex = Math.Max(0, Math.Min(ComboBoxIndex, GiftLists.Count-1));

        if (GiftListsNames.Count == 0) { EditButtonIsVisible = false; RemoveButtonIsVisible = false; }
        else {
            EditButtonIsVisible = true; RemoveButtonIsVisible = true;
        }

        if (Gifts.Count == 0) { NoElementsIsVisible = true; NoElementsText = "No elements in the gift list"; }
        else { NoElementsIsVisible = false;}

        RaisePropertyChanges();
        Calculate();
    }

    public void UpdateView()
    {
        GiftListsNames.Clear();
        Gifts.Clear();

        if (Service.GiftLists == null) { return; }

        foreach (GiftListDTO giftList in GiftLists)
        {
            GiftListsNames.Add(giftList.Name);
        }

        if (GiftLists.Count > ComboBoxIndex && ComboBoxIndex > -1)
        {
            foreach (GiftDTO gift in GiftLists[ComboBoxIndex].Gifts)
            {
                Gifts.Add(new GiftItemModel(gift, this));
            }
        }

        if (GiftListsNames.Count == 0) { EditButtonIsVisible = false; RemoveButtonIsVisible = false; }
        else
        {
            EditButtonIsVisible = true; RemoveButtonIsVisible = true;
        }

        if (Gifts.Count == 0) { NoElementsIsVisible = true; NoElementsText = "No elements in the gift list"; }
        else { NoElementsIsVisible = false; }

        RaisePropertyChanges();
        Calculate();
    }

    private void RaisePropertyChanges()
    {
        this.RaisePropertyChanged(nameof(ComboBoxIndex));
        this.RaisePropertyChanged(nameof(EditButtonIsVisible));
        this.RaisePropertyChanged(nameof(RemoveButtonIsVisible));
        this.RaisePropertyChanged(nameof(NoElementsIsVisible));
        this.RaisePropertyChanged(nameof(NoElementsText));
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

        if (Gifts.Count == 0)
        {
            if (Gifts.Count == 0) { NoElementsIsVisible = true; NoElementsText = "No elements in the gift list"; }
            else { NoElementsIsVisible = false; }
        }

        this.RaisePropertyChanged(nameof(NoElementsIsVisible));
        this.RaisePropertyChanged(nameof(NoElementsText));
    }

    public void AddGiftList(string name, DateOnly? date)
    {
        if(GiftLists.FindIndex(gl => gl.Name == name) != -1) { throw new Exception("A gift list with the same name already exists"); }
        Service.AddGiftListAsync(name,date);
        UpdateView();
    }

    public void RemoveGiftList()
    { 
        if(ComboBoxIndex == -1) { return; }

        Service.RemoveGiftListAsync(GiftLists[ComboBoxIndex].Name);
        UpdateView();
    }

    public void EditGiftList(string name, string newName, DateOnly? date)
    {
        if (GiftLists.FindIndex(gl => gl.Name == newName && gl.Name != name) != -1) { throw new Exception("A gift list with the same name already exists"); }
        Service.EditGiftListAsync(name, newName, date);
        UpdateView();
    }

    public void Dispose()
    {
        Service.OnGiftsChanged -= Calculate;
    }
}