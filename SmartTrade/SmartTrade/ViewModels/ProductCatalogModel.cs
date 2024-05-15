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

            List<PurchaseDTO> purchases = await Service.GetPurchasesAsync();
            string titlePost = post.Title;

            if (purchases == null || purchases.Count == 0) { return false; }

            var purchasesToCompare = purchases.DistinctBy(x => x.PostId).TakeLast(3).ToList();

            foreach (var purchase in purchasesToCompare)
            {
                float count = 0;
                int? idPostPurchase = purchase.PostId;
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

        private float CalculateProductNameScore(string productNamePost, string namePurchase, int threshold)
        {
            float similarity = Fuzz.PartialTokenSortRatio(productNamePost, namePurchase);
            float scoreIncrement = MathF.Max(0, (similarity - threshold)) / (100 - threshold);
            return scoreIncrement;
        }


        private float CalculateCategoryAndSellerScore(Category categoryPost, Category categoryPurchase, string sellerIdPost, string emailSellerPurchase)
        {
            float score = 0;

            if (categoryPost.Equals(categoryPurchase)) score += 0.85f;
            if (sellerIdPost.Equals(emailSellerPurchase)) score += 0.15f;

            return score;
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

        public bool ParentalControlerChecker(DateTime BirthDate)
        {
            DateTime currentDate = DateTime.Now;
            int totalDays = currentDate.Day - BirthDate.Day;
            int totalMonths = currentDate.Month - BirthDate.Month;
            int totalYears = currentDate.Year - BirthDate.Year;
            if (totalDays < 0)
            {
                totalDays += DateTime.DaysInMonth(BirthDate.Year, BirthDate.Month);
                totalMonths--;
            }
            if (totalMonths < 0)
            {
                totalMonths += 12;
                totalYears--;
            }
            int age = totalYears;
            if (totalMonths > 0 || totalDays > 0)
            {
                age++;
            }
            if (age >= 18)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
