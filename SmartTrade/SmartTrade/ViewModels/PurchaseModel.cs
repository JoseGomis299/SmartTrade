﻿using System;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using ReactiveUI;
using SmartTrade.Entities;
using SmartTrade.Views;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels;

public class PurchaseModel : ViewModelBase
{
    public string Name { get; set; }
    public string Price { get; set; }
    public string ShippingCost { get; set; }
    public Bitmap Image { get; set; }
    public PostDTO Post { get; set; }
    public OfferDTO Offer { get; set; }
    public string Quantity { get; set; }
    public string DeliveryState { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime EstimatedDate { get; set; }
    public int DeliveryStateInt { get; set; }
    public Address DeliveryAddress { get; set; }
    public Address FacturationAddress { get; set; }

    public ICommand OpenProductCommand { get; }

    public PurchaseModel(PurchaseDTO purchaseDTO, OrderHistoryModel OrderHistoryModel)
    {
        Post = purchaseDTO.Post;
        Offer = purchaseDTO.Offer;
        Service.CachePost(Post);

        OpenProductCommand = ReactiveCommand.Create(OpenProduct);

        Name = purchaseDTO.Post.Title;
        Price = purchaseDTO.Offer.Price + "€";
        ShippingCost = purchaseDTO.Offer.ShippingCost + "€";
        Image = purchaseDTO.Offer.Product.Images[0].ToBitmap();
        Quantity = "Quantity: " + purchaseDTO.Quantity.ToString();
        PurchaseDate = purchaseDTO.PurchaseDate;
        EstimatedDate = purchaseDTO.ExpectedDate;
        DeliveryState = "Delivery state: " + CalculateState();
        DeliveryStateInt = CalculateStateInt();
        DeliveryAddress = purchaseDTO.DeliveryAddress;
        FacturationAddress = purchaseDTO.BillingAddress;
    }

    private void OpenProduct()
    {
        //Cambiar para la vista de seguimiento
        var view = new SendView(this);
        SmartTradeNavigationManager.Instance.NavigateTo(view);
    }

    public string CalculateState()
    {
        if (DateTime.Now.CompareTo(EstimatedDate) >= 0) { return "Received"; }

        int daysSincePurchase = DateTime.Now.DayOfYear - PurchaseDate.DayOfYear;
        
        return daysSincePurchase <= 2 ? "Pending Shipping" : "Shipped";
    }

    public int CalculateStateInt()
    {
        if (DateTime.Now.CompareTo(EstimatedDate) >= 0) { return 2; }

        int daysSincePurchase = DateTime.Now.DayOfYear - PurchaseDate.DayOfYear;

        return daysSincePurchase <= 2 ? 0 : 1;
    }
}