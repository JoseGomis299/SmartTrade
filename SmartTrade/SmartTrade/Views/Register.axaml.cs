using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using SmartTrade.ViewModels;
using System.Collections.ObjectModel;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml.Templates;
using SmartTradeLib.Entities;
using Avalonia.Media;
using Microsoft.IdentityModel.Tokens;
using ReactiveUI;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.VisualTree;
using Avalonia;
namespace SmartTrade.Views
{
    public partial class Register : UserControl
    {
        public Register()
        {
            InitializeComponent();
        }
    }
}
