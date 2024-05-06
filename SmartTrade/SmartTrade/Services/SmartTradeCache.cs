using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Platform;
using Newtonsoft.Json;
using SmartTrade.Entities;
using SmartTrade.Helpers;
using SmartTradeDTOs;

namespace SmartTrade.Services
{
    public class SmartTradeCache
    {
        public event Action? OnPostsChanged;
        public event Action? OnCartChanged;
        private List<PostDTO> _completePosts = new List<PostDTO>();
        private List<SimplePostDTO>? _simplePosts;
        public List<SimplePostDTO>? Posts => _simplePosts;
        public List<NotificationDTO>? Notifications { get; set; }
        public List<CartItemDTO>? CartItems { get; set; } = new List<CartItemDTO>();
        public List<PurchaseDTO>? Purchases { get; set; }
        public List<WishDTO>? Wishes { get; set; }
        public List<List<CartItemDTO>>? GiftLists { get; set; } = new List<List<CartItemDTO>>();
        public List<AlertDTO>? Alerts { get; set; }

        public async Task LoadCartItemsAsync()
        {
            CartItems = await JSONsaving.ReadFromJsonFileAsync<List<CartItemDTO>>("ShoppingCartItems") ?? new List<CartItemDTO>();
            await JSONsaving.WriteToJsonFileAsync(CartItems, "ShoppingCartItems");

            OnCartChanged?.Invoke();
        }
        public void LoadCartItems(List<CartItemDTO> items)
        {
            CartItems = items;
            OnCartChanged?.Invoke();
        }

        public void SetPosts(List<SimplePostDTO>? posts)
        {
            _simplePosts = posts;
            OnPostsChanged?.Invoke();
        }

        public PostDTO? GetPost(int postId)
        {
            PostDTO? post = _completePosts.Find(x => x.Id == postId);
            return post;
        }

        public void StorePost(PostDTO post)
        {
            _completePosts.Add(post);
        }

        public void RemovePost(int postId)
        {
            SimplePostDTO? simplePost = GetSimplePost(postId);
            PostDTO? post = GetCompletePost(postId);

            if (simplePost != null)
                Posts.Remove(simplePost);
              
            if(post != null)
                _completePosts.Remove(post);
        }

        public void RemoveNotification(int notificationId)
        {
            NotificationDTO? notification = GetNotification(notificationId);
         
            if (notification != null) Notifications.Remove(notification);
        }

        public void MarkNotificationAsVisited(int notificationId)
        {
            NotificationDTO? notification = GetNotification(notificationId);
            if (notification != null) notification.Visited = true;
        }

        private NotificationDTO? GetNotification(int notificationId)
        {
            NotificationDTO? notification = Notifications.Find(x => x.Id == notificationId);
            return notification;
        }

        private SimplePostDTO? GetSimplePost(int postId)
        {
            SimplePostDTO? simplePost = Posts.Find(x => x.Id == postId);
            return simplePost;
        }

        private PostDTO? GetCompletePost(int postId)
        {
            PostDTO? post = _completePosts.Find(x => x.Id == postId);
            return post;
        }

        public async Task<int> AddItemToCartAsync(PostDTO post, OfferDTO offer, int quantity)
        {
            int index = CartItems.FindIndex(x => x.Post.Id == post.Id && x.Offer.Id == offer.Id);
            
            if (index == -1)
            {
                CartItems.Add(new CartItemDTO(post, offer, quantity));
            }
            else CartItems[index].Quantity += quantity;

            OnCartChanged?.Invoke();
            await JSONsaving.WriteToJsonFileAsync(CartItems, "ShoppingCartItems");

            return index == -1 ? quantity : CartItems[index].Quantity;
        }

        public int AddItemToCart(PostDTO post, OfferDTO offer, int quantity)
        {
            int index = CartItems.FindIndex(x => x.Post.Id == post.Id && x.Offer.Id == offer.Id);

            if (index == -1)
            {
                CartItems.Add(new CartItemDTO(post, offer, quantity));
            }
            else CartItems[index].Quantity += quantity;

            OnCartChanged?.Invoke();

            return index == -1 ? quantity : CartItems[index].Quantity;
        }

        public void DeleteItemFromCart(int offerId)
        {
            int index = CartItems.FindIndex(x=> x.Offer.Id == offerId);
            if (index != -1) CartItems.RemoveAt(index);

            OnCartChanged?.Invoke();
        }

        public async Task DeleteItemFromCartAsync(int offerId)
        {
            int index = CartItems.FindIndex(x => x.Offer.Id == offerId);
            if (index != -1) CartItems.RemoveAt(index);

            OnCartChanged?.Invoke();
            await JSONsaving.WriteToJsonFileAsync(CartItems, "ShoppingCartItems");
        }
    }
}
