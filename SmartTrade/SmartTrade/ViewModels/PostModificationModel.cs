using System;
using System.Collections.ObjectModel;
using System.Linq;
using SmartTradeLib.Entities;

namespace SmartTrade.ViewModels;

public class PostModificationModel : ViewModelBase
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? ProductName { get; set; }

    public Category Category
    {
        get => _category;
        set
        {
            if (Stocks.Count > 0)
            {
                Stocks[0].ChangeCategory(value);
                for (int i = 1; i < Stocks.Count; i++)
                {
                    Stocks.RemoveAt(i);
                }
            }
            _category = value;
        }
    }
    public string? MinimumAge { get; set; }
    public string? Certifications { get; set; }
    public string? EcologicPrint { get; set; }
    public ObservableCollection<Stock> Stocks { get; } = new ObservableCollection<Stock>();

    private Category _category;

    public void AddStock()
    {

        if (Stocks.Count >= 1)
        {
            Stocks.Add(new Stock(Stocks[0], Category, this));
        }
        else Stocks.Add(new Stock(Category, this));

    }

    public void CheckErrors()
    {
        if (Stocks.Count == 0)
            throw new Exception("Post must have at least one stock");

        for (var i = 0; i < Stocks.Count; i++)
        {
            var stock = Stocks[i];
            if (string.IsNullOrEmpty(stock.StockQuantity) || string.IsNullOrEmpty(stock.Price) ||
                string.IsNullOrEmpty(stock.ShippingCost))
                throw new Exception("Stock must have all fields filled");

            if(int.Parse(stock.StockQuantity) <= 0)
                throw new Exception("Stock quantity must be greater than 0");

            if (float.Parse(stock.Price) <= 0)
                throw new Exception("Price must be greater than 0");


            foreach (var attribute in stock.CategoryAttributes)
            {
                if (string.IsNullOrEmpty(attribute.Value))
                    throw new Exception($"Stock {i} must have all fields filled");

                if (attribute.OnlyInt && !int.TryParse(attribute.Value, out _))
                    throw new Exception($"Field \"{attribute.Name}\" in stock {i} must be a number");

                if (attribute.OnlyFloat && !float.TryParse(attribute.Value, out _))
                    throw new Exception($"Field \"{attribute.Name}\" in stock {i}  must be a number");
            }

            if (stock.Images.Count == 0)
                throw new Exception($"Stock {i} must have at least one image");

            for (var j = 0; j < Stocks.Count; j++)
            {
                var otherStock = Stocks[j];
                if (stock != otherStock && stock.CategoryAttributes.SequenceEqual(otherStock.CategoryAttributes))
                    throw new Exception($"Stocks {i} and {j} must have different attributes");
            }
        }
    }
}