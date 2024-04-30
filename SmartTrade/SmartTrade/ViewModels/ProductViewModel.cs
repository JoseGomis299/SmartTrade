using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Utilities;
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
                else if (await IsRelatedAsync(post))
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
        public async Task<bool> IsRelatedAsync(SimplePostDTO post)
        {
            int limitPoints = 175;
            int count = 0;
            Category categoryPost = post.Category;
            string productNamePost = post.ProductName;
            string sellerIdPost = post.SellerID;
            IEnumerable<SimplePostDTO>? posts = Service.Posts;
            string titlePost = post.Title;

            if (posts == null) { return false; }


            foreach (var postComparable in posts)
            {
                int? idPost = postComparable.Id;
                if (idPost.HasValue)
                {
                    PostDTO postDTO = await Service.GetPostAsync((int)idPost);
                    Category categoryPostDTO = postDTO.Category;
                    String namePostDTO = postDTO.ProductName;
                    String emailSellerPostDTO = postDTO.SellerID;
                    String titlePostPostDTO = postDTO.Title;

                    count += CalculateProductNameScore(productNamePost, namePostDTO, 50);
                    count += CalculateProductNameScore(productNamePost, namePostDTO, 80);
                    count += CalculateCategoryAndSellerScore(categoryPost, categoryPostDTO, sellerIdPost, emailSellerPostDTO);
                    count += CalculateProductNameScore(titlePost, titlePostPostDTO,80);
                    if (count >= limitPoints)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    

    private static int CalculateProductNameScore(string productNamePost, string namePurchase, int threshold)
    {
        int similarity = 100;
        int scoreIncrement = (int)Math.Pow(Math.Max(0, (similarity - threshold)), 2) / (int)Math.Pow(100 - threshold, 2) * 50 * 2;
        if (threshold == 50 || threshold == 80)
        {
            return Math.Min(scoreIncrement, 25);
        }
        else if (threshold == 50)
        {
            return Math.Min(scoreIncrement, 50);
        }
        return scoreIncrement;
    }

    private static int CalculateCategoryAndSellerScore(Category categoryPost, Category categoryPurchase, string sellerIdPost, string emailSellerPurchase)
    {
        int score = 0;

        if (categoryPost.Equals(categoryPurchase)) score += 70;
        if (sellerIdPost.Equals(emailSellerPurchase)) score += 30;

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
            await Service.CreateWishAsync((int)Post.Id);
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