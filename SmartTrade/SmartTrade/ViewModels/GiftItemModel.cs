using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using ReactiveUI;
using SmartTrade.Entities;
using SmartTrade.Views;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels;

public class GiftItemModel : ViewModelBase
{
    private event Action<int, int> OnQuantityChanged;
    public string? Name { get; set; }
    public string? GiftListName { get; set; }
    public string? Price { get; set; }
    public string? ShippingCost { get; set; }
    public Bitmap? Image { get; set; }
    public PostDTO Post { get; set; }
    public OfferDTO Offer { get; set; }
    private int _quantity;

    public string? Quantity
    {
        get => _quantity.ToString();
        set
        {
            int prev = _quantity;
            _quantity = int.Parse(value);
            OnQuantityChanged?.Invoke(prev, _quantity);
        }
    }

    public ICommand OpenProductCommand { get; }
    public ICommand DeleteItemCommand { get; }

    public GiftItemModel(GiftDTO giftDTO, GiftsModel GiftsModel)
    {
        Post = giftDTO.Post;
        Offer = giftDTO.Offer;

        OpenProductCommand = ReactiveCommand.Create(OpenProduct);
        DeleteItemCommand = ReactiveCommand.CreateFromTask( async () =>
        {
            GiftsModel.Gifts.Remove(this);
            await Service.RemoveGiftAsync(giftDTO.Quantity, (int)giftDTO.Post.Id, giftDTO.Offer.Id, giftDTO.GiftListName);
        });

        Name = giftDTO.Post.Title;
        GiftListName = giftDTO.GiftListName;
        Price = giftDTO.Offer.Price + "€";
        ShippingCost = giftDTO.Offer.ShippingCost + "€";
        Image = giftDTO.Offer.Product.Images[0].ToBitmap();
        Quantity = giftDTO.Quantity.ToString();

        OnQuantityChanged += async (prev, quantity) =>
        {
            await AddGiftAsync(prev, quantity);
        };
    }

    private void OpenProduct()
    {
        var view = new ProductView(Post);
        SmartTradeNavigationManager.Instance.NavigateTo(view);
        ((ProductViewModel)view.DataContext).LoadProducts();
    }

    private async Task AddGiftAsync(int previousQuantity, int quantity)
    {
        if (previousQuantity != 0)
            await Service.AddGiftAsync(quantity - previousQuantity, Post, Offer, GiftListName);
    }
}