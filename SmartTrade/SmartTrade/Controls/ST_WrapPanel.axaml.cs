using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SmartTrade.ViewModels;

namespace SmartTrade.Controls;

public partial class ST_WrapPanel : UserControl
{
    public ST_WrapPanel()
    {
        LeftProducts = new ObservableCollection<ProductModel>();
        RightProducts = new ObservableCollection<ProductModel>();

        InitializeComponent();
    }

    public ObservableCollection<ProductModel> LeftProducts
    {
        get => GetValue(LeftProductsProperty);
        set => SetValue(LeftProductsProperty, value);
    }

    public ObservableCollection<ProductModel> RightProducts
    {
        get => GetValue(RightProductsProperty);
        set => SetValue(RightProductsProperty, value);
    }

    public ObservableCollection<ProductModel> Products
    {
        get => GetValue(ProductsProperty);
        set => SetValue(ProductsProperty, value);
    }

    public static readonly StyledProperty<ObservableCollection<ProductModel>> ProductsProperty =
        AvaloniaProperty.Register<ST_WrapPanel, ObservableCollection<ProductModel>>(nameof(Products));

    public static readonly StyledProperty<ObservableCollection<ProductModel>> RightProductsProperty =
        AvaloniaProperty.Register<ST_WrapPanel, ObservableCollection<ProductModel>>(nameof(RightProducts));

    public static readonly StyledProperty<ObservableCollection<ProductModel>> LeftProductsProperty =
        AvaloniaProperty.Register<ST_WrapPanel, ObservableCollection<ProductModel>>(nameof(LeftProducts));

    private int _prevCount;

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == ProductsProperty)
        {
            Products.CollectionChanged -= UpdateLists; 
            Products.CollectionChanged += UpdateLists;

            UpdateLists(null, null);
        }
    }

    private void UpdateLists(object? sender, NotifyCollectionChangedEventArgs e)
    {
        int diff = Products.Count - _prevCount;
        int count = _prevCount;

        if(diff == 0)
            return;

        if (diff > 0)
        {
            for (int i = 0; i < diff; i++, count++)
            {
                if (count % 2 == 0)
                {
                    LeftProducts.Add(Products[count]);
                }
                else
                {
                    RightProducts.Add(Products[count]);
                }
            }
        }
        else
        {
            for (int i = 0; i < -diff; i++, count--)
            {
                if (count % 2 == 0)
                {
                    LeftProducts.RemoveAt(LeftProducts.Count - 1);
                }
                else
                {
                    RightProducts.RemoveAt(RightProducts.Count - 1);
                }
            }
        }

        _prevCount = Products.Count;
    }
}