using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using ReactiveUI;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels
{
    public class ProductViewModel : ReactiveObject
	{
        public PostDTO postView;
        public ObservableCollection<ProductModel> OtherSellers { get; set; }
        public ObservableCollection<ProductModel> SameSellerProducts { get; set; }
        public ObservableCollection<ProductModel> RelatedProducts { get; set; }
        public ObservableCollection<Bitmap> Images { get; set; }
        public ObservableCollection<AttributeModel> Attributes { get; set; }
        public string? Price {  get; set; }
        public string? ShippingCost {  get; set; }
        public string? Title {  get; set; }
        public string? Seller {  get; set; }
        public string? Description {  get; set; }
        public string? Details {  get; set; }
        public Bitmap? AlertImage { get; set; }
        private Bitmap? _alertActivated {  get; set; }
        private Bitmap? _alertDeactivated {  get; set; }

        public ProductViewModel(PostDTO post)
        {
            this.postView = post;

            OtherSellers = new ObservableCollection<ProductModel>();
            SameSellerProducts = new ObservableCollection<ProductModel>();
            RelatedProducts = new ObservableCollection<ProductModel>();
            Images = new ObservableCollection<Bitmap>();
            Attributes = new ObservableCollection<AttributeModel>();

            Title = post.Title;
            Seller = "Vendido por: " + post.SellerCompanyName;
            Description = post.Description;

            LoadData(post.Offers[0]);

            foreach(var offer in post.Offers)
            {
                Attributes.Add(new AttributeModel(offer.Product.Differentiators, offer, this));
            }

            _alertActivated = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/AlertSelected.png")));
            _alertDeactivated = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/Alert.png")));

            //SetAlertImage();
        }

        private void SetAlertImage(PostDTO post)
        {
            //if (post.)
        }

        public void LoadProducts()
        {
            IEnumerable<SimplePostDTO>? posts = SmartTradeService.Instance.Posts;

            foreach(var post in posts)
            {
                if (OtherSellersSameProduct(post))
                {
                    OtherSellers.Add(new ProductModel(post));
                }
                else if (SameSeller(post))
                {
                    SameSellerProducts.Add(new ProductModel(post));
                }
                else if (IsRelated(post))
                {
                    RelatedProducts.Add(new ProductModel(post));
                }
            }
        }

        private bool SameSeller(SimplePostDTO post)
        {
            return post.SellerID == postView.SellerID;
        }
        private bool OtherSellersSameProduct(SimplePostDTO post)
        {
            return post.ProductName == postView.ProductName && post.SellerID != postView.SellerID;
        }
        public bool IsRelated(SimplePostDTO post)
        {
            return Random.Shared.Next(0, 2) == 1;
        }

        public void LoadData(OfferDTO offer)
        {
            foreach (var image in offer.Product.Images)
            {
                Images.Add(image.ToBitmap());
            }

            Price = offer.Price + "€";
            ShippingCost = "Shipping Cost: " + offer.ShippingCost + "€";
            Details = offer.Product.Info;
        }
    }

    public class AttributeModel : ReactiveObject
    {
        public string? Text { get; set; }
        public ICommand ChangeOfferCommand { get; set; }

        public AttributeModel(string? text, OfferDTO offer, ProductViewModel model)
        {
            Text = text;
            ChangeOfferCommand = ReactiveCommand.Create(() => model.LoadData(offer));
        }
    }
}