using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SmartTradeDTOs;
using SmartTrade.Entities;
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

            foreach (var post in posts)
            {

                OriginalProducts.Add(new ProductModel(post));

                if (IsEcologic(post))
                {
                    RecommendedProducts.Add(new ProductModel(post));
                }
                else if (Service.Logged != null && IsRelated(post))
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

        public bool IsRelated(SimplePostDTO post)
        {
            Category categoryPost = post.Category;
            string productNamePost = post.ProductName;
            string sellerIdPost = post.SellerID;

            List<PurchaseDTO> purchases = Service.Purchases;
            string titlePost = post.Title;

            if (purchases == null || purchases.Count == 0) { return false; }

            var purchasesToCompare = purchases.DistinctBy(x => x.Post.Id).TakeLast(3).ToList();

            foreach (var purchase in purchasesToCompare)
            {
                float count = 0;
                int? idPostPurchase = purchase.Post.Id;
                if (idPostPurchase.HasValue)
                {
                    SimplePostDTO postPurchase = Service.Posts.First(x => x.Id == idPostPurchase);
                    Category categoryPurchase = postPurchase.Category;
                    String namePurchase = postPurchase.ProductName;
                    String emailSellerPurchase = purchase.EmailSeller;
                    String titlePostPurchase = postPurchase.Title;

                    count += CalculateProductNameScore(namePurchase, productNamePost, 50) * 30;
                    count += CalculateProductNameScore(titlePost, titlePostPurchase, 40) * 10;
                    count += CalculateCategoryAndSellerScore(categoryPost, categoryPurchase, sellerIdPost, emailSellerPurchase) * 80;

                    if (count >= 65f)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public float CalculateProductNameScore(string productNamePost, string namePurchase, int threshold)
        {
            float similarity = Fuzz.PartialTokenSortRatio(productNamePost, namePurchase);
            float scoreIncrement = MathF.Max(0, (similarity - threshold)) / (100 - threshold);
            return scoreIncrement;
        }


        public float CalculateCategoryAndSellerScore(Category categoryPost, Category categoryPurchase, string sellerIdPost, string emailSellerPurchase)
        {
            float score = 0;

            if (categoryPost.Equals(categoryPurchase)) score += 0.85f;
            if (sellerIdPost.Equals(emailSellerPurchase)) score += 0.15f;

            return score;
        }

        public void UpdateProducts(IEnumerable<ProductModel> list, int? category)
        {
            if(category != null) list = Filtering((Category)category);
            
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
                else if (IsRelated(post))
                { 
                    RelatedProducts.Add(product);
                }
                else OtherProducts.Add(product);
            }
        }

        public override void SortByCategory(int? category)
        {
            UpdateProducts(OriginalProducts, category);
        }

        public List<ProductModel> Filtering(Category category)
        {
            List<ProductModel> FilteredProducts = new List<ProductModel>();
            foreach (var product in OriginalProducts)
            {
                if (product.Post.Category == category)
                {
                    FilteredProducts.Add(product);
                }
            }

            return FilteredProducts;
        }


    }

}
