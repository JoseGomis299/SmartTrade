using System.Collections.ObjectModel;

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
        MainViewModel.SmartTradeService.GetPosts().ForEach(post =>
        {
            MyProducts.Add(new ProductModel(post));
        });
    }

}