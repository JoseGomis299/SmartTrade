using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using ReactiveUI;
using SmartTrade.Entities;
using SmartTrade.Views;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels;

public class CartItemModel : ProductModel
{
    private event Action<int, int> OnQuantityChanged;
    public new PostDTO Post { get; set; }
    public OfferDTO Offer { get; set; }
    public string? EstimatedTime { get; }

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

    public CartItemModel(CartItemDTO itemDto)
    {
        Post = itemDto.Post;
        Offer = itemDto.Offer;

        OpenProductCommand = ReactiveCommand.Create(OpenProduct);
        Name = itemDto.Post.Title;
        Price = itemDto.Offer.Price + "€";
        ShippingCost = itemDto.Offer.ShippingCost + "€";
        Image = itemDto.Offer.Product.Images[0].ToBitmap();
        Quantity = itemDto.Quantity.ToString();
        EstimatedTime = itemDto.EstimatedShippingDays + " days";

        OnQuantityChanged += async (prev, quantity) => await AddItemToCartAsync(prev, quantity);
    }

    public CartItemModel(CartItemDTO itemDto, ShoppingCartModel shoppingCartModel)
    {
        Post = itemDto.Post;
        Offer = itemDto.Offer;

        OpenProductCommand = ReactiveCommand.Create(OpenProduct);
        DeleteItemCommand = ReactiveCommand.CreateFromTask( async () =>
        {
            shoppingCartModel.Products.Remove(this);
            await Service.DeleteItemFromCartAsync(itemDto.Offer.Id);
        });


        Name = itemDto.Post.Title;
        Price = itemDto.Offer.Price + "€";
        ShippingCost = itemDto.Offer.ShippingCost + "€";
        Image = itemDto.Offer.Product.Images[0].ToBitmap();
        Quantity = itemDto.Quantity.ToString();
        EstimatedTime = itemDto.EstimatedShippingDays + " days";

        OnQuantityChanged += async (prev, quantity) => await AddItemToCartAsync(prev, quantity);
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