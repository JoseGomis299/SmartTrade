using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media.Imaging;
using DynamicData;
using ReactiveUI;
using SmartTrade.Views;
using SmartTradeLib.Entities;

namespace SmartTrade.ViewModels
{
    public class RegisterPostModel : ViewModelBase
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ProductName { get; set; }

        public Category Category
        {
            get => _category;
            set
            {
                foreach (var stock in Stocks)
                {
                    stock.ChangeCategory(value);
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
            Stocks.Add(new Stock(Category, this));
        }

        public void CheckErrors()
        {
            if(Stocks.Count == 0)
                throw new Exception("Post must have at least one stock");

            for (var i = 0; i < Stocks.Count; i++)
            {
                var stock = Stocks[i];
                if (string.IsNullOrEmpty(stock.StockQuantity) || string.IsNullOrEmpty(stock.Price) ||
                    string.IsNullOrEmpty(stock.ShippingCost))
                    throw new Exception("Stock must have all fields filled");

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

        public void PublishPost()
        {
            List<int> stocks = new();
            List<float> prices = new();
            List<float> shippingCosts = new();

            List<List<byte[]>> images = new();
            List<List<string>> attributes = new();

            foreach (var stock in Stocks)
            {
                images.Add(stock.Images.Select(image => image.Bytes).ToList());

                stocks.Add(int.Parse(stock.StockQuantity));
                prices.Add(float.Parse(stock.Price));
                shippingCosts.Add(float.Parse(stock.ShippingCost));

                attributes.Add(stock.CategoryAttributes.Select(attribute => attribute.Value).ToList());
            }

            MainViewModel.SmartTradeService.AddPost(Title, Description, ProductName, Category, int.Parse(MinimumAge), Certifications, EcologicPrint, stocks, prices, shippingCosts, images, attributes);
        }
    }
    public class CategoryAttribute
    {
        public string? Name { get; set; }
        public string? Value { get; set; }
        public bool OnlyInt { get; set; }
        public bool OnlyFloat { get; set; }

        public CategoryAttribute(string name)
        {
            if (name.EndsWith('i')) OnlyInt = true;
            else if (name.EndsWith('f')) OnlyFloat = true;
            
            Name = name.Substring(0, name.Length - 2);
        }

        public override bool Equals(object? obj)
        {
            return obj is CategoryAttribute attribute && attribute.Name == Name && attribute.Value == Value;
        }
    }

    public class ImageSource
    {
        public Bitmap? Image { get; set; }

        public byte[] Bytes { get; set; }

        public ICommand RemoveImage { get; }

        public ImageSource(byte[] image, Stock stock)
        {
            Bytes = image;
            Image = image.ToBitmap();

            RemoveImage = ReactiveCommand.Create(() =>
            {
                stock.Images.Remove(this);
            });
        }
    }

    public class Stock
    {
        public string? StockQuantity { get; set; }
        public string? Price { get; set; }
        public string? ShippingCost { get; set; }
        public ObservableCollection<ImageSource> Images { get; set; } = new ObservableCollection<ImageSource>();
        public ObservableCollection<CategoryAttribute> CategoryAttributes { get; set; } = new ObservableCollection<CategoryAttribute>();
        public ICommand AddImagesCommand { get; }
        public ICommand RemoveFromStock { get; }

        public Stock(Category category, RegisterPostModel model)
        {
            AddImagesCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                if (App.Current.ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
                {
                    UserControl? mainView = (UserControl?)singleViewPlatform.MainView;

                    foreach (var image in await mainView.OpenFileDialogMultiple("Select images", "png jpg jpeg"))
                    {
                        Images.Add(new ImageSource(image, this));
                    }

                }
            });

            RemoveFromStock = ReactiveCommand.Create(() =>
            {
                model.Stocks.Remove(this);
            });

            StockQuantity = "0";
            Price = "0";
            ShippingCost = "0";

            ChangeCategory(category);
        }

        public void ChangeCategory(Category category)
        {
            CategoryAttributes.Clear();
            foreach (var attribute in category.GetAttributes())
            {
                CategoryAttributes.Add(new CategoryAttribute(attribute));
            }
        }
    }
}
