using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media.Imaging;
using DynamicData.Binding;
using ReactiveUI;
using SmartTrade.Views;
using SmartTrade.Entities;

namespace SmartTrade.ViewModels
{
    public class SearchResultModel : ViewModelBase
    {
        public ObservableCollection<ProductModel> SearchedProducts { get; set; }
        public ObservableCollection<ProductModel> OriginalSearchedProducts { get; set; }
        public bool PriceAscend, PriceDescend, Sustainable;
        public SearchResultModel()
        {
            SearchedProducts = new ObservableCollection<ProductModel>();
            OriginalSearchedProducts = new ObservableCollection<ProductModel>();
            PriceAscend = PriceDescend = Sustainable = false;
        }

        public void ApplyFilters() 
        {
            UpdateProducts(OriginalSearchedProducts);

            if (Sustainable) { sortSustainable(); }

            if (PriceAscend) { sortPriceAscend(); }
            else if (PriceDescend) { sortPriceDescend(); }
            
        }

        public void sortPriceAscend()
        {
          var sortedList = SearchedProducts.OrderBy(x => float.Parse(x.Price.Substring(0, x.Price.Length-1))).ToList();
          UpdateProducts(sortedList);
        }

        public void sortPriceDescend()
        {
            var sortedList = SearchedProducts.OrderByDescending(x => float.Parse(x.Price.Substring(0, x.Price.Length - 1))).ToList();
            UpdateProducts(sortedList);
        }

        public void sortSustainable()
        {
            var sortedList = new List<ProductModel>();
            foreach (var product in SearchedProducts)
            {
                if (int.TryParse(product.Post.EcologicPrint, out int ecologicPrint) && ecologicPrint < 100) 
                {
                    sortedList.Add(product);
                }
            }
            UpdateProducts(sortedList);
        }

        private void UpdateProducts(IEnumerable<ProductModel> list)
        {
            SearchedProducts.Clear();
            foreach (var product in list)
            {
                SearchedProducts.Add(product);
            }
        }

        public void SortByCategory(int? category)
        {
            SearchedProducts.Clear();
            if (category == null)
            {
                foreach (var product in OriginalSearchedProducts)
                {
                    SearchedProducts.Add(product);
                    ApplyFilters();
                }
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
            foreach (var product in OriginalSearchedProducts)
            {
                if (product.Post.Category == category)
                {
                    SearchedProducts.Add(product);
                }
            }
            ApplyFilters();
        }
    }
}
