using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using ReactiveUI;
using SmartTrade.Entities;
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
        public Address DeliveryAddress { get; set; }
        public Address FacturationAddress { get; set; }
        public int? DeliveryState { get; set; }
        public string DeliveryStreet { get; set; }
        public string DeliveryDoor { get; set; }
        public string DeliveryMunicipality { get; set; }
        public string FacturationStreet { get; set; }
        public string FacturationDoor { get; set; }
        public string FacturationMunicipality { get; set; }



        public PostDTO Post { get; set; }

        public SendViewModel()
        {

        }

        public SendViewModel(PurchaseModel purchase)
        {
            
            OpenProductCommand = ReactiveCommand.CreateFromTask(OpenProduct);
            Name = purchase.Name;
            Price = purchase.Price;
            ShippingCost = purchase.ShippingCost;
            Image = purchase.Image;
            Post = purchase.Post;
            DeliveryState = purchase.DeliveryStateInt;
            DeliveryAddress = purchase.DeliveryAddress;
            FacturationAddress =purchase.FacturationAddress;
            DeliveryStreet = DeliveryAddress.Street + DeliveryAddress.Number + " Street";
            DeliveryDoor = "Number " + DeliveryAddress.Door;
            DeliveryMunicipality = DeliveryAddress.City + "," + DeliveryAddress.PostalCode;
            FacturationStreet = FacturationAddress.Street + FacturationAddress.Number + " Street";
            FacturationDoor = "Number " + FacturationAddress.Door;
            FacturationMunicipality = FacturationAddress.City + ", " + FacturationAddress.PostalCode;
            ArrivedDate = DateOnly.FromDateTime(purchase.EstimatedDate).ToString();
        }

        private async Task OpenProduct()
        {
            var view = new ProductView(await Service.GetPostAsync((int)Post.Id));
            SmartTradeNavigationManager.Instance.NavigateTo(view);
            ((ProductViewModel)view.DataContext).LoadProducts();
        }
    }
}
