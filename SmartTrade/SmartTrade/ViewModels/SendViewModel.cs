using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using ReactiveUI;
using SmartTrade.Views;
using SmartTradeDTOs;



namespace SmartTrade.ViewModels
{
    public class SendViewModel:ViewModelBase
    {
        public string? Name { get; set; }
        public string? Price { get; set; }
        public string? ShippingCost { get; set; }
        public Bitmap? Image { get; set; }
        public ICommand OpenProductCommand { get; }
        public string? ArrivedDate { get; set; }
        public string? DeliveryAddress { get; set; }
        public string? FacturationAddress { get; set; }
        public int? DeliveryState { get; set; }
        

        public PostDTO Post { get; set; }

        public SendViewModel(PurchaseModel purchase)
        {
            
            OpenProductCommand = ReactiveCommand.CreateFromTask(OpenProduct);
            Name = purchase.Name;
            Price = purchase.Price + "€";
            ShippingCost = purchase.ShippingCost + "€";
            Image = purchase.Image;
            Post = purchase.Post;
            DeliveryState = purchase.DeliveryStateInt;

        }

        private async Task OpenProduct()
        {
            var view = new ProductView(await Service.GetPostAsync((int)Post.Id));
            SmartTradeNavigationManager.Instance.NavigateTo(view);
            ((ProductViewModel)view.DataContext).LoadProductsAsync();
        }
    }
}
