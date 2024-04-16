using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using Newtonsoft.Json;
using ReactiveUI;
using SmartTrade.Views;
using SmartTradeDTOs;
using SmartTrade.Entities;
using System.Threading.Tasks;
using SkiaSharp;

namespace SmartTrade.ViewModels
{
	public class ProductViewModel : ReactiveObject
	{
        public PostDTO postView;
        public ObservableCollection<ProductModel> OtherSellers { get; set; }
        public ObservableCollection<ProductModel> SameSellerProducts { get; set; }
        public ObservableCollection<ProductModel> RelatedProducts { get; set; }
        public ObservableCollection<Bitmap> Images { get; set; }

        public ProductViewModel(PostDTO post)
        {
            this.postView = post;

            OtherSellers = new ObservableCollection<ProductModel>();
            SameSellerProducts = new ObservableCollection<ProductModel>();
            RelatedProducts = new ObservableCollection<ProductModel>();
            Images = new ObservableCollection<Bitmap>();

            foreach(var image in post.Offers[0].Product.Images)
            {
                Images.Add(image.ToBitmap());
            }
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
    }
}