using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using ReactiveUI;
using SmartTrade.Services;
using SmartTrade.Views;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels;

public class NotificationModel : ViewModelBase
{
    public string? Name { get; set; }
    public string? Price { get; set; }
    public Bitmap? Image { get; set; }
    public SimplePostDTO Post { get; set; }

    public ICommand OpenProductCommand { get; }
    public ICommand DeleteNotificationCommand { get; }

    private NotificationDTO _notification;

    public NotificationModel(SimplePostDTO post, AlertViewModel alertViewModel, NotificationDTO notification)
    {
        _notification = notification;

        Post = post;
        OpenProductCommand = ReactiveCommand.CreateFromTask(OpenProduct);
        DeleteNotificationCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            await Service.DeleteNotificationAsync(notification.Id);
            alertViewModel.ProductsNotifications.Remove(this);
        });


        Name = post.Title;
        Price = post.Price + "€";
        Image = post.Image.ToBitmap();
    }

    private async Task OpenProduct()
    {
        if(!_notification.Visited) await Service.SetNotificationAsVisitedAsync(_notification.Id);

        var view = new ProductView(await Service.GetPostAsync((int)Post.Id));
        SmartTradeNavigationManager.Instance.NavigateTo(view);
        ((ProductViewModel)view.DataContext).LoadProducts();
    }
}