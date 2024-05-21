using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using ReactiveUI;
using SmartTrade.Views;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels;

public class WishModel : ViewModelBase
{
    public string? Name { get; set; }
    public string? Price { get; set; }
    public string? ShippingCost { get; set; }
    public Bitmap? Image { get; set; }
    public SimplePostDTO Post { get; set; }

    public ICommand OpenProductCommand { get; }
    public ICommand DeleteWishCommand { get; }

    public WishModel(SimplePostDTO post, WishListModel wishListModel, WishDTO wish)
    {
        Post = post;
        OpenProductCommand = ReactiveCommand.CreateFromTask(OpenProduct);
        DeleteWishCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            wishListModel.ProductsInWishList.Remove(this);
            await Service.DeleteWishAsync(wish.Id);
        });


        Name = post.Title;
        Price = post.Price + "€";
        ShippingCost = post.ShippingCost + "€";
        Image = post.Image.ToBitmap();
    }

    private async Task OpenProduct()
    {
        var view = new ProductView(await Service.GetPostAsync((int)Post.Id));
        SmartTradeNavigationManager.Instance.NavigateTo(view);
        ((ProductViewModel)view.DataContext).LoadProducts();
    }
}