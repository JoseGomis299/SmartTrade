using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using SmartTrade.Controls;
using SmartTrade.ViewModels;
using SmartTradeDTOs;

namespace SmartTrade.Views
{
    public partial class OrderHistoryView :UserControl
    {
        public OrderHistoryView()
        {
            DataContext = new OrderHistoryModel();
            InitializeComponent();
        }

    }
}
