using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using ReactiveUI;
using SmartTrade.Entities;
using SmartTrade.Views;
using SmartTradeAPI.Library.Persistence.DTOs;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels;

public class PurchaseModel : ViewModelBase
{
    public string? Name { get; set; }
    public string? Price { get; set; }
    public string? ShippingCost { get; set; }
    public Bitmap? Image { get; set; }
    public PostDTO Post { get; set; }
    public OfferDTO Offer { get; set; }
    public string? EstimatedTime { get; set; }
    public string? Quantity { get; set; }

    public ICommand OpenProductCommand { get; }

    public PurchaseModel(PurchaseDTO purchaseDTO, OrderHistoryModel OrderHistoryModel)
    {
        /*Post = purchaseDTO.Post;
        Offer = purchaseDTO.Offer;

        OpenProductCommand = ReactiveCommand.Create(OpenProduct);
        DeleteItemCommand = ReactiveCommand.CreateFromTask( async () =>
        {
            OrderrHistoryModel.Products.Remove(this);
            await Service.DeleteItemFromCartAsync(purchaseDTO.Offer.Id);
        });


        Name = purchaseDTO.Post.Title;
        Price = purchaseDTO.Offer.Price + "€";
        ShippingCost = purchaseDTO.Offer.ShippingCost + "€";
        Image = purchaseDTO.Offer.Product.Images[0].ToBitmap();
        Quantity = purchaseDTO.Quantity.ToString();
        EstimatedTime = purchaseDTO.EstimatedShippingDays + " days";

        OnQuantityChanged += async (prev, quantity) => await AddItemToCartAsync(prev, quantity);*/
    }

    private void OpenProduct()
    {
        var view = new ProductView(Post);
        SmartTradeNavigationManager.Instance.NavigateTo(view);
        ((ProductViewModel)view.DataContext).LoadProductsAsync();
    }

    private async Task AddItemToCartAsync(int previousQuantity, int quantity)
    {
        if (previousQuantity != 0)
            await Service.AddItemToCartAsync(Post, Offer, quantity - previousQuantity);
    }
}