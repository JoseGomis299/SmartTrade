using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
}
