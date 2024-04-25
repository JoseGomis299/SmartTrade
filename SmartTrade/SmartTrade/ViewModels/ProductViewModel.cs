using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Utilities;
using ReactiveUI;
using SmartTrade.Services;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels
{
    public class ProductViewModel : ViewModelBase
	{
        public event Action OnOfferChanged;

        public PostDTO Post { get; set; }
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
        private int _quantity;

        public string? Quantity
        {
            get => _quantity.ToString();
            set
            {
                _quantity = int.Parse(value);
                this.RaisePropertyChanged(nameof(Quantity));
            }
        }
        private Bitmap? _alertActivated {  get; set; }
        private Bitmap? _alertDeactivated {  get; set; }
        private ToggleButton? _alertToggle;
        private int _currentOfferIndex;

        public UserDTO? Logged => Service.Logged;

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
            this.Post = post;

            OtherSellers = new ObservableCollection<ProductModel>();
            SameSellerProducts = new ObservableCollection<ProductModel>();
            RelatedProducts = new ObservableCollection<ProductModel>();
            Images = new ObservableCollection<Bitmap>();
            Attributes = new ObservableCollection<AttributeModel>();

            Title = post.Title;
            Seller = "Vendido por: " + post.SellerCompanyName;
            Description = post.Description;

            LoadData(post.Offers[0], 0);

            for (var i = 0; i < post.Offers.Count; i++)
            {
                var offer = post.Offers[i];
                Attributes.Add(new AttributeModel(offer.Product.Differentiators, i, offer, this));
            }

            Attributes[0].SetChecked(true);

            _alertActivated = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/AlertSelected.png")));
            _alertDeactivated = new Bitmap(AssetLoader.Open(new Uri("avares://SmartTrade/Assets/Alert.png")));

            Quantity = "1";
            //SetAlertImage();
        }

        //private void SetAlertImage()
        //{
        //    if (SmartTradeBroker.Instance.Logged == null /*|| Post.Offers[0].Product.UsersWithAlertsInThisProduct[0] != SmartTradeBroker.Instance.Logged.Name*/)
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
            IEnumerable<SimplePostDTO>? posts = Service.Posts;

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
            return post.SellerID == Post.SellerID;
        }
        private bool OtherSellersSameProduct(SimplePostDTO post)
        {
            return post.ProductName == Post.ProductName && post.SellerID != Post.SellerID;
        }
        public bool IsRelated(SimplePostDTO post)
        {
            return Random.Shared.Next(0, 2) == 1;
        }

        public void LoadData(OfferDTO offer, int offerIndex)
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

            _currentOfferIndex = offerIndex;
            OnOfferChanged?.Invoke();
        }

        public async Task CreateAlertAsync(int productId)
        {
            await Service.CreateAlertAsync(productId);
        }

        public void AddItemToCart()
        {
            Service.AddItemToCart(Post, _currentOfferIndex, _quantity);
        }
    }

    public class AttributeModel : ReactiveObject
    {
        public string? Text { get; set; }
        public bool IsChecked { get; set; }
        public ReactiveCommand<ItemCollection, Unit> ChangeOfferCommand { get; set; }

        public AttributeModel(string? text, int offerIndex,OfferDTO offer, ProductViewModel model)
        {
            Text = text;

            ChangeOfferCommand = ReactiveCommand.Create<ItemCollection>(collection =>
            {
                foreach (var item in collection)
                {
                    (item as AttributeModel).SetChecked(false);
                }
             
                SetChecked(true);
                model.LoadData(offer, offerIndex);
            });
        }

        public void SetChecked(bool value)
        {
            IsChecked = value;
            this.RaisePropertyChanged(nameof(IsChecked));
        }

    }
}