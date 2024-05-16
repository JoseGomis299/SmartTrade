﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using ReactiveUI;
using SmartTrade.Entities;
using SmartTrade.Views;
using SmartTradeAPI.Library.Persistence.DTOs;
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
    public DateTime PurchaseDate { get; set; }
    public DateTime EstimatedDate { get; set; }

    public ICommand OpenProductCommand { get; }

    public PurchaseModel(PurchaseDTO purchaseDTO, OrderHistoryModel OrderHistoryModel)
    {
        Post = purchaseDTO.Post;
        Offer = purchaseDTO.Offer;

        OpenProductCommand = ReactiveCommand.Create(OpenProduct);

        Name = purchaseDTO.Post.Title;
        Price = purchaseDTO.Offer.Price + "€";
        ShippingCost = purchaseDTO.Offer.ShippingCost + "€";
        Image = purchaseDTO.Offer.Product.Images[0].ToBitmap();
        Quantity = purchaseDTO.Quantity.ToString();
        PurchaseDate = purchaseDTO.PurchaseDate;

    }

    private void OpenProduct()
    {
        //Cambiar para la vista de seguimiento
        var view = new SendView();
        SmartTradeNavigationManager.Instance.NavigateTo(view);
    }

}