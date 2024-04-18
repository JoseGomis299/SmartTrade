using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using SmartTradeDTOs;
using SmartTrade.Entities;

namespace SmartTrade.ViewModels;

public class Stock
{
    public event Action OnValuesChanged;
    public string? StockQuantity { get; set; }
    public string? Price { get; set; }
    public string? ShippingCost { get; set; }
    public ObservableCollection<ImageSource> Images { get; set; } = new();
    public ObservableCollection<CategoryAttribute> CategoryAttributes { get; set; } = new();

    public ICommand AddImagesCommand { get; set; }
    public ICommand RemoveFromStock { get; set; }

    private PostModificationModel? _model;

    #region Constructors

    // Constructor for creating a new stock from an offer (Used in Validate Post)
    public Stock(OfferDTO offer, ValidatePostModel model)
    {
        InitializeValues(model);

        foreach (var image in offer.Product.Images)
        {
            Images.Add(new ImageSource(image, this));
        }

        StockQuantity = offer.Stock.ToString();
        Price = offer.Price.ToString();
        ShippingCost = offer.ShippingCost.ToString();


        foreach (var attribute in (model.Post.Category.GetAttributes()))
        {
            CategoryAttributes.Add(new CategoryAttribute(attribute));
        }

        Dictionary<string, string> attributes = offer.Product.Attributes;
        for (var i = 0; i < CategoryAttributes.Count; i++)
        {
            CategoryAttributes[i].Value = attributes[CategoryAttributes[i].Name];
        }

        foreach (var attribute in CategoryAttributes)
        {
            attribute.OnValueChanged += InvokeValuesChanged;
        }

        foreach (var stock in model.Stocks)
        {
            stock.OnValuesChanged += CopyAttributesFromFirst;
        }
    }


    // Constructor for creating a new stock from a category
    public Stock(Category category, RegisterPostModel model)
    {
        InitializeValues(model);

        StockQuantity = "0";
        Price = "0";
        ShippingCost = "0";

        foreach (var attribute in category.GetAttributes())
        {
            CategoryAttributes.Add(new CategoryAttribute(attribute));
        }

        foreach (var attribute in CategoryAttributes)
        {
            attribute.OnValueChanged += InvokeValuesChanged;
        }
    }

    // Constructor for creating a new stock from an existing stock (Used when there is already a stock in the model)
    public Stock(Stock other, Category category, RegisterPostModel model)
    {
        InitializeValues(model);

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

    #endregion

    #region Methods

    private void InitializeValues(PostModificationModel model)
    {
        _model = model;

        AddImagesCommand = ReactiveCommand.CreateFromTask(AddImage);
        RemoveFromStock = ReactiveCommand.Create(RemoveStock);
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

    private void InvokeValuesChanged()
    {
        OnValuesChanged?.Invoke();
    }

    public void ChangeCategory(Category category)
    {
        foreach (var attribute in CategoryAttributes)
        {
            attribute.OnValueChanged -= InvokeValuesChanged;
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
                attribute.OnValueChanged += InvokeValuesChanged;
            }
        }
    }

    #endregion

    #region CommandDefinitions

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
                attribute.OnValueChanged += _model.Stocks[0].InvokeValuesChanged;
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

    #endregion
}