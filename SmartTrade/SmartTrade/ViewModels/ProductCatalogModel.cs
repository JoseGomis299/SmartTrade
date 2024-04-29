using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SmartTradeDTOs;
using SmartTrade.Entities;
using SmartTrade.Services;
using SmartTrade.DTOs;
using System.Linq;
using FuzzySharp;

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
            int limitPoints = 175;
            int count = 0;
            Category categoryPost = post.Category;
            string productNamePost = post.ProductName;
            string sellerIdPost = post.SellerID;
            List<PurchaseDTO> purchases = new List<PurchaseDTO>();
            
            purchases = await Service.GetPurchases();
           

            if(purchases==null || purchases.Count == 0) { return false; } 
            var purchasesToCompare = purchases.TakeLast(3).ToList();

            foreach (var purchase in purchasesToCompare)
            {
                int? idPostPurchase = purchase.PostId;
                if (idPostPurchase.HasValue)
                {
                    PostDTO postPurchase = await Service.GetPostAsync((int)idPostPurchase);
                    Category categoryPurchase = postPurchase.Category;
                    String namePurchase = postPurchase.ProductName;
                    String emailSellerPurchase = purchase.EmailSeller;

                    if (Fuzz.PartialTokenSortRatio(productNamePost, namePurchase) > 50) count += 50;
                    if (Fuzz.PartialTokenSortRatio(productNamePost, namePurchase) > 80) count += 50;
                    if (categoryPost.Equals(categoryPurchase)) count += 70;
                    if (sellerIdPost.Equals(emailSellerPurchase)) count += 30;
                    if (count >=limitPoints)
                    {
                        return true;
                    }
                }
                
            }

            return false;

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
