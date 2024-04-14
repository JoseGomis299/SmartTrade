using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels;

public class AdminCatalogModel : CatalogModel
{
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

}