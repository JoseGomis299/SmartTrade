using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using ReactiveUI;
using SmartTrade.Views;
using SmartTradeLib.Entities;

namespace SmartTrade.ViewModels
{
    public class ProductCatalogModel : ViewModelBase
    {
        public ObservableCollection<ProductModel> Products { get; set; }

        public ProductCatalogModel()
        {
            Products = new ObservableCollection<ProductModel>();
            LoadProducts();
        }

        private void LoadProducts()
        {
            MainViewModel.SmartTradeService.GetPosts().ForEach(post =>
            {
                Products.Add(new ProductModel(post));
            });
        }
        
    }

    public class ProductModel : ViewModelBase
    {
        public string? Name { get; set; }
        public string? Price { get; set; }
        public Bitmap? Image { get; set; }
        private Post _post { get; set; }

        public ICommand OpenProductCommand { get; }

        public ProductModel(Post post)
        {
            _post = post;
            OpenProductCommand = ReactiveCommand.Create(OpenProduct);

            Name = post.Title;
            Price = post.Offers.First().Price + "€";
            Image = post.Offers.First().Product.Images.First().ImageSource.ToBitmap();
        }

        private void OpenProduct()
        {
            NavigationManager.NavigateTo(new ProductView(_post));
        }
    }
}
