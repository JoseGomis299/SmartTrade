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
            SearchedProducts = new ObservableCollection<ProductModel>(OriginalSearchedProducts);
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
