﻿using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartTrade.ViewModels;

public abstract class CatalogModel : ViewModelBase
{
    public abstract Task LoadProductsAsync();
    public abstract void SortByCategory(int? category);

}