using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SmartTradeDTOs;
using SmartTrade.Entities;
using SmartTrade.Services;
using SmartTrade.DTOs;

namespace SmartTrade.ViewModels
{
    public class ProductCatalogModel : CatalogModel
    {
        public List<ProductModel> OriginalProducts { get; set; }
        public ObservableCollection<ProductModel> OtherProducts { get; set; }
        public ObservableCollection<ProductModel> RecommendedProducts { get; set; }
        public ObservableCollection<ProductModel> RelatedProducts { get; set; }

        public ProductCatalogModel()
        {
            OriginalProducts = new List<ProductModel>();
            OtherProducts = new ObservableCollection<ProductModel>();
            RecommendedProducts = new ObservableCollection<ProductModel>();
            RelatedProducts = new ObservableCollection<ProductModel>();
        }

        public override async Task LoadProductsAsync()
        {
            List<SimplePostDTO>? posts = await Service.RefreshPostsAsync();

            foreach(var post in posts)
            {
                OriginalProducts.Add(new ProductModel(post));

                if (IsEcologic(post))
                {
                    RecommendedProducts.Add(new ProductModel(post));
                }
                else if (await IsRelated(post))
                {
                    RelatedProducts.Add(new ProductModel(post));
                }
                else OtherProducts.Add(new ProductModel(post));
            }
        }

        public bool IsEcologic(SimplePostDTO post)
        {
            return int.TryParse(post.EcologicPrint, out int ecologicPrint) && ecologicPrint < 10;
        }

        public async Task<bool> IsRelated(SimplePostDTO post)
        {
            Category categoryPost = post.Category;
            string productNamePost = post.ProductName;
            string sellerIdPost = post.SellerID;
            List<PurchaseDTO> purchases = new List<PurchaseDTO>();

            purchases = await Service.GetPurchases();

            return Random.Shared.Next(0, 2) == 1;
        }

        public async void UpdateProducts(IEnumerable<ProductModel> list)
        {
            OtherProducts.Clear();
            RecommendedProducts.Clear();
            RelatedProducts.Clear();

            foreach (var product in list)
            {
                SimplePostDTO post = product.Post;

                if (IsEcologic(post))
                {
                    RecommendedProducts.Add(product);
                }
                else if (await IsRelated(post))
                { 
                    RelatedProducts.Add(product);
                }
                else OtherProducts.Add(product);
            }
        }

        public override void SortByCategory(int? category)
        {
            if (category == null)
            {
                UpdateProducts(OriginalProducts);
            }
            else
            {
                switch (category)
                {

                    case 0:
                        this.Filtering(Category.Toy); break;


                    case 1:
                        this.Filtering(Category.Nutrition); break;


                    case 2:
                        this.Filtering(Category.Clothing); break;


                    case 3:
                        this.Filtering(Category.Book); break;

                }
            }
        }

        public void Filtering(Category category)
        {
            List<ProductModel> FilteredProducts = new List<ProductModel>();
            foreach (var product in OriginalProducts)
            {
                if (product.Post.Category == category)
                {
                    FilteredProducts.Add(product);
                }
            }

            UpdateProducts(FilteredProducts);
        }
    }
}
