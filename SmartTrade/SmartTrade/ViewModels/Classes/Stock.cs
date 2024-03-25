using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using SmartTradeLib.Entities;

namespace SmartTrade.ViewModels;

public class Stock
{
    public event Action OnValuesChanged;
    public string? StockQuantity { get; set; }
    public string? Price { get; set; }
    public string? ShippingCost { get; set; }
    public ObservableCollection<ImageSource> Images { get; set; } = new ObservableCollection<ImageSource>();

    public ObservableCollection<CategoryAttribute> CategoryAttributes { get; set; } =
        new ObservableCollection<CategoryAttribute>();

    public ICommand AddImagesCommand { get; set; }
    public ICommand RemoveFromStock { get; set; }

    private PostModificationModel? _model;

    // Constructor for creating a new stock from an offer (Used in Validate Post)
    public Stock(Offer offer, PostModificationModel model)
    {
        _model = model;

        AddImagesCommand = ReactiveCommand.CreateFromTask(AddImage);
        RemoveFromStock = ReactiveCommand.Create(() => { model.Stocks.Remove(this); });

        foreach (var image in offer.Product.Images)
        {
            Images.Add(new ImageSource(image.ImageSource, this));
        }

        StockQuantity = offer.Stock.ToString();
        Price = offer.Price.ToString();
        ShippingCost = offer.ShippingCost.ToString();


        foreach (var attribute in (offer.Product.GetCategories().First().GetAttributes()))
        {
            CategoryAttributes.Add(new CategoryAttribute(attribute));
        }

        string[] attributes = offer.Product.GetAttributes();
        for (var i = 0; i < CategoryAttributes.Count; i++)
        {
            CategoryAttributes[i].Value = attributes[i];
        }

        foreach (var attribute in CategoryAttributes)
        {
            attribute.OnValueChanged += OnAttributeChanged;
        }

        foreach (var stock in model.Stocks)
        {
            stock.OnValuesChanged += CopyAttributesFromFirst;
        }
    }


    // Constructor for creating a new stock from a category
    public Stock(Category category, PostModificationModel model)
    {
        _model = model;

        AddImagesCommand = ReactiveCommand.CreateFromTask(AddImage);
        RemoveFromStock = ReactiveCommand.Create(RemoveStock);

        StockQuantity = "0";
        Price = "0";
        ShippingCost = "0";

        foreach (var attribute in category.GetAttributes())
        {
            CategoryAttributes.Add(new CategoryAttribute(attribute));
        }

        foreach (var attribute in CategoryAttributes)
        {
            attribute.OnValueChanged += OnAttributeChanged;
        }
    }

    // Constructor for creating a new stock from an existing stock (Used when there is already a stock in the model)
    public Stock(Stock other, Category category, PostModificationModel model)
    {
        _model = model;

        AddImagesCommand = ReactiveCommand.CreateFromTask(AddImage);
        RemoveFromStock = ReactiveCommand.Create(RemoveStock);

        StockQuantity = other.StockQuantity;
        Price = other.Price;
        ShippingCost = other.ShippingCost;

        Images.Clear();
        foreach (var image in other.Images)
        {
            Images.Add(new ImageSource(image.Bytes, this));
        }

        foreach (var attribute in other.CategoryAttributes)
        {
            CategoryAttributes.Add(new CategoryAttribute(attribute.Name)
            {
                Value = attribute.Value
            });
        }

        foreach (var i in category.GetNonRepeatableAttributes())
        {
            CategoryAttributes[i].IsEnabled = false;
        }

        other.OnValuesChanged += CopyAttributesFromFirst;
    }

    private void CopyAttributesFromFirst()
    {
        if (_model == null) return;

        for (var i = 0; i < CategoryAttributes.Count; i++)
        {
            if (!CategoryAttributes[i].IsEnabled)
                CategoryAttributes[i].Value = _model.Stocks[0].CategoryAttributes[i].Value;
        }
    }

    private void OnAttributeChanged()
    {
        OnValuesChanged?.Invoke();
    }

    public void ChangeCategory(Category category)
    {
        foreach (var attribute in CategoryAttributes)
        {
            attribute.OnValueChanged -= OnAttributeChanged;
        }

        CategoryAttributes.Clear();

        foreach (var attribute in category.GetAttributes())
        {
            CategoryAttributes.Add(new CategoryAttribute(attribute));
        }

        if (_model != null && _model.Stocks[0] == this)
        {
            foreach (var attribute in CategoryAttributes)
            {
                attribute.OnValueChanged += OnAttributeChanged;
            }
        }
    }

    private void RemoveStock()
    {
        if (_model == null) return;

        bool isFirst = _model.Stocks[0] == this;

        _model.Stocks.Remove(this);

        if (isFirst && _model.Stocks.Count >= 1)
        {
            foreach (var attribute in _model.Stocks[0].CategoryAttributes)
            {
                attribute.IsEnabled = true;
                attribute.OnValueChanged += _model.Stocks[0].OnAttributeChanged;
            }

            for (int i = 1; i < _model.Stocks.Count; i++)
            {
                _model.Stocks[0].OnValuesChanged += _model.Stocks[i].CopyAttributesFromFirst;
            }

            _model.Stocks[0].OnValuesChanged -= _model.Stocks[0].CopyAttributesFromFirst;
        }
    }
    private async Task AddImage()
    {
        if (App.Current.ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            UserControl? mainView = (UserControl?)singleViewPlatform.MainView;

            foreach (var image in await mainView.OpenFileDialogMultiple("Select images", "png jpg jpeg"))
            {
                Images.Add(new ImageSource(image, this));
            }
        }
    }
}