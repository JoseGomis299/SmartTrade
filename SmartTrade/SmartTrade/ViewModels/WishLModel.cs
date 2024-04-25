using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using ReactiveUI;
using SmartTrade.Views;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels;

public class WishLModel : ViewModelBase
{
    public string? Name { get; set; }
    public string? Price { get; set; }
    public Bitmap? Image { get; set; }
    public SimplePostDTO Post { get; set; }

    public ICommand OpenProductCommand { get; }
    public ICommand DeleteWishCommand { get; }

    private WishDTO _wish;

    public WishLModel(SimplePostDTO post, WishListModel wishListModel, WishDTO wish)
    {
        _wish = wish;

        Post = post;
        OpenProductCommand = ReactiveCommand.CreateFromTask(OpenProduct);
        DeleteWishCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            wishListModel.ProductsInWishList.Remove(this);
            await Service.DeleteWishAsync(wish.Id);
        });


        Name = post.Title;
        Price = post.Price + "€";
        Image = post.Image.ToBitmap();
    }

    private async Task OpenProduct()
    {
        var view = new ProductView(await Service.GetPostAsync((int)Post.Id));
        SmartTradeNavigationManager.Instance.NavigateTo(view);
        ((ProductViewModel)view.DataContext).LoadProducts();
    }
}