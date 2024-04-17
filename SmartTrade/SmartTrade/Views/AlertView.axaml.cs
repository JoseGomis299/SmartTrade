using Avalonia.Controls;
using SmartTradeDTOs;
using SmartTrade.ViewModels;
using System.Collections.Generic;

namespace SmartTrade.Views
{
    public partial class AlertView : UserControl
    {
        public AlertView()
        {
            DataContext = new AlertViewModel();
            InitializeComponent();
        }
    }
}
