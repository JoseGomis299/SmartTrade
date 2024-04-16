using SmartTrade.Entities;
using System.Collections.ObjectModel;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels;

public class SellerCatalogModel : CatalogModel
{
    public List<ProductModel> OriginalMyProducts { get; set; }
    public ObservableCollection<ProductModel> MyProducts { get; set; }

    public SellerCatalogModel()
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

        OriginalMyProducts = new List<ProductModel>(MyProducts);
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