using SmartTrade.Entities;
using System.Collections.ObjectModel;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartTradeDTOs;
using SmartTrade.Services;

namespace SmartTrade.ViewModels;

public class SellerCatalogModel : CatalogModel
{
    public List<ProductModel> OriginalMyProducts { get; set; }
    public List<ProductModel> OriginalNotValidatedPosts { get; set; }
    public ObservableCollection<ProductModel> MyProducts { get; set; }
    public ObservableCollection<ProductModel> NotValidatedPosts { get; set; }

    public SellerCatalogModel()
    {
        MyProducts = new ObservableCollection<ProductModel>();
        NotValidatedPosts = new ObservableCollection<ProductModel>();
    }

    public override async Task LoadProductsAsync()
    {
        List<SimplePostDTO>? posts = await Service.RefreshPostsAsync();
        
        posts.ForEach(post =>
        {
            if(post.Validated)
             MyProducts.Add(new ProductModel(post));
            else NotValidatedPosts.Add(new ProductModel(post));
        });

        OriginalMyProducts = new List<ProductModel>(MyProducts);
        OriginalNotValidatedPosts = new List<ProductModel>(NotValidatedPosts);
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

            foreach (var product in OriginalNotValidatedPosts)
            {
                NotValidatedPosts.Add(product);
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

        foreach (var product in OriginalNotValidatedPosts)
        {
            if (product.Post.Category == category)
            {
                NotValidatedPosts.Add(product);
            } 
        }
    }
}