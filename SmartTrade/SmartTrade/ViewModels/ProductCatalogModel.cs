using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using Newtonsoft.Json;
using ReactiveUI;
using SmartTrade.Views;
using SmartTradeDTOs;
using SmartTrade.Entities;

namespace SmartTrade.ViewModels
{
    public class ProductCatalogModel : CatalogModel
    {
        public ObservableCollection<ProductModel> OtherProducts { get; set; }
        public ObservableCollection<ProductModel> RecommendedProducts { get; set; }
        public ObservableCollection<ProductModel> RelatedProducts { get; set; }

        public ProductCatalogModel()
        {
            OtherProducts = new ObservableCollection<ProductModel>();
            RecommendedProducts = new ObservableCollection<ProductModel>();
            RelatedProducts = new ObservableCollection<ProductModel>();
        }

        public override async Task LoadProductsAsync()
        {
            List<PostDTO>? posts = await SmartTradeService.Instance.GetPostsAsync();

            posts.ForEach(post =>
            {
                if (IsEcologic(post))
                {
                    RecommendedProducts.Add(new ProductModel(post));
                }else if (IsRelated(post))
                {
                    RelatedProducts.Add(new ProductModel(post));
                }
                else OtherProducts.Add(new ProductModel(post));
            });
        }

        public bool IsEcologic(PostDTO post)
        {
            return int.TryParse(post.EcologicPrint, out int ecologicPrint) && ecologicPrint < 100;
        }

        public bool IsRelated(PostDTO post)
        {
            return Random.Shared.Next(0, 2) == 1;
        }
        
    }

    public class ProductModel : ViewModelBase
    {
        public string? Name { get; set; }
        public string? Price { get; set; }
        public Bitmap? Image { get; set; }
        public PostDTO Post { get; set; }

        public ICommand OpenProductCommand { get; }
        public ICommand EditProductCommand { get; }

        public ProductModel(PostDTO post)
        {
            Post = post;
            OpenProductCommand = ReactiveCommand.Create(OpenProduct);
            EditProductCommand = ReactiveCommand.Create(() => SmartTradeNavigationManager.Instance.NavigateTo(new ValidatePost(post)));

            Name = post.Title;
            Price = post.Offers.First().Price + "€";
            Image = post.Offers.First().Product.Images.First().ToBitmap();
        }

        private void OpenProduct()
        {
            SmartTradeNavigationManager.Instance.NavigateTo(new ProductView(Post));
        }
    }
}
