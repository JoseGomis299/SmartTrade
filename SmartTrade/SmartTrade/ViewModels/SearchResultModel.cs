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
using ReactiveUI;
using SmartTrade.Views;
using SmartTradeLib.Entities;

namespace SmartTrade.ViewModels
{
    public class SearchResultModel : ViewModelBase
    {
        public string SearchText { get; set; }
        public ObservableCollection<string> SearchAutoComplete { get; set; }
        public ObservableCollection<ProductModel> SearchedProducts { get; set; }
        public ObservableCollection<ProductModel> OriginalSearchedProducts { get; set; }
        public bool PriceAscend, PriceDescend, Sustainable;
        public SearchResultModel()
        {
            SearchAutoComplete = new ObservableCollection<string>();
            SearchedProducts = new ObservableCollection<ProductModel>();
            OriginalSearchedProducts = new ObservableCollection<ProductModel>();
            SearchText = "";
            PriceAscend = PriceDescend = Sustainable = false;
        }

        public List<Post> LoadProducts()
        {
            return MainViewModel.SmartTradeService.GetPostsFuzzyContain(SearchText);
        }

        public List<string> GetNamesProducts() 
        {
            return MainViewModel.SmartTradeService.GetPostsNamesStartWith(SearchText,8);
        }

        public void ApplyFilters() 
        {
            SearchedProducts = OriginalSearchedProducts;
            if (PriceAscend) { sortPriceAscend(); }
            if (PriceDescend) { sortPriceDescend(); }
            if (Sustainable) { }
        }

        public void sortPriceAscend() 
        {
            SearchedProducts.OrderBy(x => x.Price);
        }

        public void sortPriceDescend()
        {
            SearchedProducts.OrderByDescending(x => x.Price);
        }

        /*public void sortSustainable()
        {
            foreach (var product in SearchedProducts)
            {
                if(int.TryParse(product.post.Offers.First().Product.EcologicPrint, out int ecologicPrint) && ecologicPrint < 100)
            }
        }*/

    }
}
