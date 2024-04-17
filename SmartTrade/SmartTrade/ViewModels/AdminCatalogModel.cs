using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartTrade.Entities;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels;

public class AdminCatalogModel : CatalogModel
{
    public List<ProductModel> OriginalMyProducts { get; set; }
    public ObservableCollection<ProductModel> MyProducts { get; set; }

    public AdminCatalogModel()
    {
        MyProducts = new ObservableCollection<ProductModel>();
    }

    public override async Task LoadProductsAsync()
    {
        List<SimplePostDTO>? posts = await SmartTradeService.Instance.GetPostsAsync();

        posts.ForEach(post =>
        {
            MyProducts.Add(new ProductModel(post));
        });
    }

    public void UpdateProducts(IEnumerable<ProductModel> list)
    {
        MyProducts.Clear();
        foreach (var product in list)
        {
            MyProducts.Add(product);
        }
    }

    public override void SortByCategory(int? category)
    {
        MyProducts.Clear();
        if (category == null)
        {
            foreach (var product in OriginalMyProducts)
            {
                MyProducts.Add(product);
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
        foreach (var product in OriginalMyProducts)
        {
            if (product.Post.Category == category)
            {
                MyProducts.Add(product);
            }
        }
    }
}