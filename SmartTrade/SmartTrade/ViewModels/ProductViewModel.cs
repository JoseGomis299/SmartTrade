using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Utilities;
using ReactiveUI;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels
{
    public class ProductViewModel : ReactiveObject
	{
        public event Action OnOfferChanged;

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
        public Bitmap? AlertToggle {  get; set; }
        private ToggleButton? _alertToggle;

        public ProductViewModel()
        {
            OtherSellers = new ObservableCollection<ProductModel>();
            SameSellerProducts = new ObservableCollection<ProductModel>();
            RelatedProducts = new ObservableCollection<ProductModel>();
            Images = new ObservableCollection<Bitmap>();
            Attributes = new ObservableCollection<AttributeModel>();

            _alertActivated = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/AlertSelected.png")));
            _alertDeactivated = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/Alert.png")));

            Title = "Título";
            Seller = "Vendido por: " + "Vendedor";
            Description = "Descripción";
        }

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

            Attributes[0].SetChecked(true);

            _alertActivated = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/AlertSelected.png")));
            _alertDeactivated = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/Alert.png")));

            //SetAlertImage();
        }

        //private void SetAlertImage()
        //{
        //    if (SmartTradeService.Instance.Logged == null /*|| postView.Offers[0].Product.UsersWithAlertsInThisProduct[0] != SmartTradeService.Instance.Logged.Name*/)
        //    {
        //        AlertImage = _alertDeactivated;
        //    }
        //    else
        //    {
        //        AlertImage = _alertActivated;
        //    }
        //}

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
            Images.Clear();

            foreach (var image in offer.Product.Images)
            {
                Images.Add(image.ToBitmap());
            }

            Price = offer.Price + "€";
            ShippingCost = "Shipping Cost: " + offer.ShippingCost + "€";
            Details = offer.Product.Info;

            this.RaisePropertyChanged(nameof(Price));
            this.RaisePropertyChanged(nameof(ShippingCost));
            this.RaisePropertyChanged(nameof(Details));

            OnOfferChanged?.Invoke();
        }

        public void UpdateAlerts()
        {

        }
    }

    public class AttributeModel : ReactiveObject
    {
        public string? Text { get; set; }
        public bool IsChecked { get; set; }
        public ReactiveCommand<ItemCollection, Unit> ChangeOfferCommand { get; set; }

        public AttributeModel(string? text, OfferDTO offer, ProductViewModel model)
        {
            Text = text;
            ChangeOfferCommand = ReactiveCommand.Create<ItemCollection>(collection =>
            {
                foreach (var item in collection)
                {
                    (item as AttributeModel).SetChecked(false);
                }
             
                SetChecked(true);
                model.LoadData(offer);
            });
        }

        public void SetChecked(bool value)
        {
            IsChecked = value;
            this.RaisePropertyChanged(nameof(IsChecked));
        }

    }
}