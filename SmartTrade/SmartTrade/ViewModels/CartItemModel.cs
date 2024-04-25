using System;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using ReactiveUI;
using SmartTrade.Entities;
using SmartTrade.Views;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels;

public class CartItemModel : ViewModelBase
{
    public string? Name { get; set; }
    public string? Price { get; set; }
    public string? ShippingCost { get; set; }
    public Bitmap? Image { get; set; }
    public PostDTO Post { get; set; }
    public int OfferIndex { get; set; }
    private int _quantity;

    public string? Quantity
    {
        get => _quantity.ToString();
        set
        {
            int prev = _quantity;
            _quantity = int.Parse(value);
            if(prev != 0)
                Service.AddItemToCart(Post, OfferIndex, _quantity-prev);
        }
    }

    public ICommand OpenProductCommand { get; }
    public ICommand DeleteItemCommand { get; }

    public CartItemModel(CartItem item, ShoppingCartModel shoppingCartModel)
    {
        Post = item.Post;
        OfferIndex = item.OfferIndex;

        OpenProductCommand = ReactiveCommand.Create(OpenProduct);
        DeleteItemCommand = ReactiveCommand.Create(() =>
        {
            shoppingCartModel.Products.Remove(this);
            Service.DeleteItemFromCart(item.Post.Id, item.OfferIndex);
        });


        Name = item.Post.Title;
        Price = item.Post.Offers[item.OfferIndex].Price + "€";
        ShippingCost = item.Post.Offers[item.OfferIndex].ShippingCost + "€";
        Image = item.Post.Offers[item.OfferIndex].Product.Images[0].ToBitmap();
        Quantity = item.Quantity.ToString();
    }

    private void OpenProduct()
    {
        var view = new ProductView(Post);
        SmartTradeNavigationManager.Instance.NavigateTo(view);
        ((ProductViewModel)view.DataContext).LoadProducts();
    }
}