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
            List<SimplePostDTO>? posts = await SmartTradeService.Instance.GetPostsAsync();

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

        public bool IsEcologic(SimplePostDTO post)
        {
            return int.TryParse(post.EcologicPrint, out int ecologicPrint) && ecologicPrint < 100;
        }

        public bool IsRelated(SimplePostDTO post)
        {
            return Random.Shared.Next(0, 2) == 1;
        }
        
    }

    public class ProductModel : ViewModelBase
    {
        public string? Name { get; set; }
        public string? Price { get; set; }
        public Bitmap? Image { get; set; }
        public SimplePostDTO Post { get; set; }

        public ICommand OpenProductCommand { get; }
        public ICommand EditProductCommand { get; }

        public ProductModel(SimplePostDTO post)
        {
            Post = post;
            OpenProductCommand = ReactiveCommand.CreateFromTask(OpenProduct);
            EditProductCommand = ReactiveCommand.CreateFromTask(EditProduct);

            Name = post.Title;
            Price = post.Price + "€";
            Image = post.Image.ToBitmap();
        }

        private async Task OpenProduct()
        {
            var view = new ProductView(await SmartTradeService.Instance.GetPostAsync((int)Post.Id));
            ((ProductViewModel)view.DataContext).LoadProducts();
            SmartTradeNavigationManager.Instance.NavigateTo(view);
        }

        private async Task EditProduct()
        {
            SmartTradeNavigationManager.Instance.NavigateTo(new ValidatePost(await SmartTradeService.Instance.GetPostAsync((int)Post.Id)));
        }


    }
}
