using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels;

public class AdminCatalogModel : ViewModelBase
{
    public ObservableCollection<ProductModel> MyProducts { get; set; }

    public AdminCatalogModel()
    {
        MyProducts = new ObservableCollection<ProductModel>();
        LoadProducts();
    }

    private void LoadProducts()
    {
        List<PostDTO> posts = JsonConvert.DeserializeObject<List<PostDTO>>(MainViewModel.SmartTradeService.GetPosts());

        posts.ForEach(post =>
        {
            MyProducts.Add(new ProductModel(post));
        });
    }

}