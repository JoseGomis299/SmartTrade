using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Utilities;
using FuzzySharp;
using ReactiveUI;
using SmartTrade.DTOs;
using SmartTrade.Entities;
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
        private OfferDTO _currentOffer;

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

            LoadData(post.Offers[0]);

            foreach (var offer in post.Offers)
            {
                Attributes.Add(new AttributeModel(offer.Product.Differentiators, offer, this));
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

        public async Task LoadProductsAsync()
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
            return post.Id != Post.Id && post.SellerID == Post.SellerID;
        }
        private bool OtherSellersSameProduct(SimplePostDTO post)
        {
            return post.ProductName == Post.ProductName && post.SellerID != Post.SellerID;
        }
        public bool IsRelated(SimplePostDTO post)
        {
            float count = 0;
            Category categoryPost = post.Category;
            string namePost = post.ProductName;
            string sellerIdPost = post.SellerID;
            string titlePost = post.Title;

            int? idPost = Post.Id;
            if (idPost.HasValue)
            {
                Category category = Post.Category;
                String name = Post.ProductName;
                String emailSeller = Seller;
                String title = Title;

                count += CalculateProductNameScore(namePost, name, 60) * 10;
                count += CalculateProductNameScore(titlePost, title, 40) * 10;
                count += CalculateCategoryAndSellerScore(categoryPost, category, sellerIdPost, emailSeller) * 80;

                if (count >= 70f)
                {
                    return true;
                }
            }
            

            return false;
        }
    

    private float CalculateProductNameScore(string productNamePost, string namePurchase, int threshold)
    {
        float similarity = Fuzz.PartialTokenSortRatio(productNamePost, namePurchase);
        float scoreIncrement = MathF.Max(0, (similarity - threshold)) / (100 - threshold);
        return scoreIncrement;
    }

    private float CalculateCategoryAndSellerScore(Category categoryPost, Category categoryPurchase, string sellerIdPost, string emailSellerPurchase)
    {
        float score = 0;

        if (categoryPost.Equals(categoryPurchase)) score += 1f;
       // if (sellerIdPost.Equals(emailSellerPurchase)) score += 0.3f;

        return score;
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

            _currentOffer = offer;
            OnOfferChanged?.Invoke();
        }

        public async Task CreateAlertAsync(int productId)
        {
            await Service.CreateAlertAsync(productId);
        }

        public async Task AddItemToCartAsync()
        {
            await Service.AddItemToCartAsync(Post, _currentOffer, _quantity);
        }

        public async Task AddItemToWishListAsync()
        {
            await Service.CreateWishAsync(Post);
        }

        public async Task DeleteFromWishListAsync()
        {
            await Service.DeleteWishFromPostAsync((int)Post.Id);
        }
    }

    public class AttributeModel : ReactiveObject
    {
        public string? Text { get; set; }
        public bool IsChecked { get; set; }
        public ReactiveCommand<ItemCollection, Unit> ChangeOfferCommand { get; set; }

        public AttributeModel(string? text,OfferDTO offer, ProductViewModel model)
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