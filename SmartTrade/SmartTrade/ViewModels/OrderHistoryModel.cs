using System.Collections.ObjectModel;
using System.Collections.Generic;
using SmartTradeDTOs;
using Avalonia.Media.Imaging;
using ReactiveUI;
using System;
using DynamicData;
using System.Linq;
using SmartTradeAPI.Library.Persistence.DTOs;

namespace SmartTrade.ViewModels;

public class OrderHistoryModel : ViewModelBase
{
    public ObservableCollection<PurchaseModel> Purchases { get; set; }

    public OrderHistoryModel()
    {
        Purchases = new ObservableCollection<PurchaseModel>();

        foreach (var item in Service.Purchases)
        {
            Purchases.Add(new PurchaseModel(item, this));
        }

        //Service.OnCartChanged += Calculate;
    }

}