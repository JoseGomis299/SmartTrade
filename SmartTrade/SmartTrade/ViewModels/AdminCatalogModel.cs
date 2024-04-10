using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels;

public class AdminCatalogModel : ViewModelBase
{
    public ObservableCollection<ProductModel> MyProducts { get; set; }

    public AdminCatalogModel()
    {
        MyProducts = new ObservableCollection<ProductModel>();
    }

    public async Task LoadProductsAsync()
    {
        List<PostDTO> posts = JsonConvert.DeserializeObject<List<PostDTO>>(await SmartTradeService.Instance.GetPostsAsync());

        posts.ForEach(post =>
        {
            MyProducts.Add(new ProductModel(post));
        });
    }

}