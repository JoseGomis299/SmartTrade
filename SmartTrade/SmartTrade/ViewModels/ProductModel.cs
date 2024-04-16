using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using ReactiveUI;
using SmartTrade.Views;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels;

public class ProductModel : ViewModelBase
{
    public string? Name { get; set; }
    public string? Price { get; set; }
    public Bitmap? Image { get; set; }
    public SimplePostDTO Post { get; set; }

    public ICommand OpenProductCommand { get; }
    public ICommand EditProductCommand { get; }

    public ProductModel(SimplePostDTO post)
    {
        Post = post;
        OpenProductCommand = ReactiveCommand.CreateFromTask(OpenProduct);
        EditProductCommand = ReactiveCommand.CreateFromTask(EditProduct);

        Name = post.Title;
        Price = post.Price + "€";
        Image = post.Image.ToBitmap();
    }

    private async Task OpenProduct()
    {
        var view = new ProductView(await SmartTradeService.Instance.GetPostAsync((int)Post.Id));
        SmartTradeNavigationManager.Instance.NavigateTo(view);
        ((ProductViewModel)view.DataContext).LoadProducts();
    }

    private async Task EditProduct()
    {
        SmartTradeNavigationManager.Instance.NavigateTo(new ValidatePost(await SmartTradeService.Instance.GetPostAsync((int)Post.Id)));
    }


}