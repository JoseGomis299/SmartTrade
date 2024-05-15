﻿using System.Collections.ObjectModel;
using System.Collections.Generic;
using SmartTradeDTOs;
using Avalonia.Media.Imaging;
using ReactiveUI;
using System;
using DynamicData;
using System.Linq;

namespace SmartTrade.ViewModels;

public class OrderHistoryModel : ViewModelBase
{
    public List<PurchaseDTO>? Purchases => Service.Purchases;

    public OrderHistoryModel()
    {
        
    }

}