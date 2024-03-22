using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
        public int MinimumAge { get; set; }
        public string? Certifications { get; set; }
        public string? EcologicPrint { get; set; }
        public ObservableCollection<Stock> Stocks { get; } = new ObservableCollection<Stock>();

        private Category _category;

        public void AddStock()
        {
            Stocks.Add(new Stock(Category, this));
        }

        public void PublishPost()
        {
            List<OfferDTO> offers = new();
            List<List<byte[]>> images = new();

            foreach (var stock in Stocks)
            {
                images.Add(stock.Images.Select(image => image.Image.ToByteArray()).ToList());
                offers.Add(new OfferDTO(Convert.ToSingle(stock.Price), Convert.ToSingle(stock.ShippingCost), Convert.ToInt32(stock.StockQuantity)));
            }

            MainViewModel.SmartTradeService.AddPost(Title, Description, ProductName, Category, MinimumAge, Certifications, EcologicPrint, offers, images);
        }
    }

    public class CategoryAttribute
    {
        public string? Name { get; set; }
        public string? Value { get; set; }

        public CategoryAttribute(string name)
        {
            Name = name;
        }
    }

    public class ImageSource
    {
        public Bitmap? Image { get; set; }

        public ICommand RemoveImage { get; }

        public ImageSource(Bitmap image, Stock stock)
        {
            Image = image;

            RemoveImage = ReactiveCommand.Create(() =>
            {
                stock.Images.Remove(this);
            });
        }
    }

    public class Stock
    {
        public string? StockQuantity { get; set; } = "0";
        public string? Price { get; set; } = "0";
        public string? ShippingCost { get; set; } = "0";
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
                        Images.Add(new ImageSource(image.ToBitmap(), this));
                    }

                    Console.WriteLine(Images.Count);
                }
            });

            RemoveFromStock = ReactiveCommand.Create(() =>
            {
                model.Stocks.Remove(this);
            });

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
