using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DynamicData;
using FuzzySharp;
using Newtonsoft.Json;
using SmartTrade.Entities;
using SmartTradeDTOs;

namespace SmartTrade.Services
{
    public class SmartTradeService
    {
        public List<SimplePostDTO>? Posts => _cache.Posts;
        public UserDTO? Logged => _broker.Logged;

        public event Action OnPostsChanged
        {
            add => _cache.OnPostsChanged += value;
            remove => _cache.OnPostsChanged -= value;
        }

        public event Action OnCartChanged
        {
            add => _cache.OnCartChanged += value;
            remove => _cache.OnCartChanged -= value;
        }

        public UserType LoggedType
        {
            get
            {
                if (Logged == null) return UserType.None;
                return Logged.GetUserType();
            }
        }
        public void SetIsParentalControlEnabled(bool isEnabled)
        {
            IsParentalControlEnabled = isEnabled;
        }

        public bool IsParentalControlEnabled { get; private set; }

        public List<CartItemDTO>? CartItems => _cache.CartItems; 
        public List<WishDTO>? WishList => _cache.Wishes; 
        public int CartItemsCount => CartItems.Sum(item => item.Quantity);

        private SmartTradeBroker _broker;
        private SmartTradeCache _cache;

        private static SmartTradeService? _instance;
        public static SmartTradeService Instance => _instance ??= new SmartTradeService();

        private SmartTradeService()
        {
            _broker = new SmartTradeBroker();
            _cache = new SmartTradeCache();
        }

        public async Task BuyItemAsync(PostDTO post, OfferDTO offer, int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                _cache.Purchases.Add(new PurchaseDTO(offer.Price, offer.ShippingCost, offer.Product.Id, post.SellerID, (int)post.Id, offer.Id));
            }
        }

        public async Task InitializeCacheAsync()
        {
            await LoadCartItems();
        }


        #region User

        public async Task LogOut()
        {
            _broker.LogOut();
            await LoadCartItems();
        }

        public async Task LogInAsync(string email, string password)
        {
            await _broker.UserClient.LogInAsync(email, password);

            await LoadCartItems();
            _cache.Purchases = null;
            _cache.Wishes = await GetWishAsync();
        }

        public async Task RegisterConsumerAsync(string email, string password, string name, string lastnames, string dni, DateTime dateBirth, Address billingAddress, Address consumerAddress)
        {
            await _broker.UserClient.RegisterConsumerAsync(email, password, name, lastnames, dni, dateBirth, billingAddress, consumerAddress);
        }

        public async Task RegisterSellerAsync(string email, string password, string name, string lastnames, string dni, string companyName, string iban)
        {
            await _broker.UserClient.RegisterSellerAsync(email, password, name, lastnames, dni, companyName, iban);
        }

        public async Task AddPaypalAsync(PayPalInfo paypalinfo, string loggedID)
        {
            await _broker.UserClient.AddPaypalAsync(paypalinfo, loggedID);
        }

        public async Task AddCreditCardAsync(CreditCardInfo creditCard)
        {
            await _broker.UserClient.AddCreditCardAsync(creditCard);
        }

        public async Task AddBizumAsync(BizumInfo bizum)
        {
           await _broker.UserClient.AddBizumAsync(bizum);
        }

        public async Task<List<PurchaseDTO>?> GetPurchases()
        {
            if(_cache.Purchases == null)
            {
                var res = await _broker.UserClient.GetPurchaseAsync();
                _cache.Purchases = res ?? new List<PurchaseDTO>();
            }

            return _cache.Purchases;
        }

        public async Task AddItemToCartAsync(PostDTO post, OfferDTO offer, int quantity = 1)
        {
            int count;
            if (Logged != null) count = _cache.AddItemToCart(post, offer, quantity);
            else count = await _cache.AddItemToCartAsync(post, offer, quantity);

            await _broker.UserClient.AddToCartAsync(new SimpleCartItemDTO((int)post.Id, offer.Id, count));
        }

        public async Task DeleteItemFromCartAsync(int offerId)
        {
            if (Logged != null) _cache.DeleteItemFromCart(offerId);
            else await _cache.DeleteItemFromCartAsync(offerId);

            await _broker.UserClient.RemoveFromCartAsync(offerId);
        }
        private async Task LoadCartItems()
        {
            await _cache.LoadCartItemsAsync();
            var guestItems = new List<CartItemDTO>(_cache.CartItems);
            var userItems = await _broker.UserClient.GetShoppingCartAsync();

            if (userItems == null) return;

            _cache.LoadCartItems(userItems);
            foreach (var cartItem in guestItems)
            {
                if (userItems.Any(x => x.Offer.Id == cartItem.Offer.Id)) continue;
                await AddItemToCartAsync(cartItem.Post, cartItem.Offer, cartItem.Quantity);
            }
        }
        #endregion

        #region Post

        public List<SimplePostDTO>? GetPostsFuzzyContain(string? searchText)
        {
            return Posts.Where(x => Fuzz.PartialTokenSortRatio(searchText, x.Title) > 51)
                .OrderByDescending(x => Fuzz.PartialTokenSortRatio(searchText, x.Title)).ToList();
        }

        public async Task AddPostAsync(PostDTO post)
        {
            await _broker.PostClient.AddPostAsync(post);
            _cache.SetPosts(await _broker.PostClient.GetPostsAsync());
            
        }

        public async Task<List<SimplePostDTO>?> RefreshPostsAsync()
        {
            var posts = await _broker.PostClient.GetPostsAsync();
            _cache.SetPosts(posts);
            return posts;
        }

        public async Task<PostDTO> GetPostAsync(int postId)
        {
            PostDTO? post = _cache.GetPost(postId);
            if (post != null) return post;

            post = await _broker.PostClient.GetPostAsync(postId);
            _cache.StorePost(post);
            return post;
        }

        public async Task EditPostAsync(int postId, PostDTO postInfo)
        {
            await _broker.PostClient.EditPostAsync(postId, postInfo);
        }

        public async Task DeletePostAsync(int postId)
        {
            _cache.RemovePost(postId);
            await _broker.PostClient.DeletePostAsync(postId);
        }

        #endregion

        #region Notification

        public async Task<List<NotificationDTO>?> GetNotificationsAsync()
        {
            if(_cache.Notifications != null) return _cache.Notifications;

            _cache.Notifications = await _broker.NotificationClient.GetNotificationsAsync();
            return _cache.Notifications;
        }

        public async Task DeleteNotificationAsync(int notificationId)
        {
            _cache.RemoveNotification(notificationId);
            await _broker.NotificationClient.DeleteNotificationAsync(notificationId);
        }

        public async Task SetNotificationAsVisitedAsync(int notificationId)
        {
           _cache.MarkNotificationAsVisited(notificationId);
           await _broker.NotificationClient.SetNotificationAsVisitedAsync(notificationId);
        }

        #endregion

        #region Alert

        public async Task<int> CreateAlertAsync(int productId)
        {
            return await _broker.AlertClient.CreateAlertAsync(productId);
        }

        public async Task DeleteAlertAsync(int alertId)
        {
            await _broker.AlertClient.DeleteAlertAsync(alertId);
        }
        
        public async Task<AlertDTO?> GetAlertsAsync(string productName)
        {
            return await _broker.AlertClient.GetAlertsAsync(productName);
        }

        #endregion

        #region WishList

        public async Task CreateWishAsync(PostDTO post)
        {
            if (_cache.Wishes.Exists(x => x.Post.Id == post.Id))
            {
                return;
            }

            int id = await _broker.WishClient.CreateWishAsync((int)post.Id);
            _cache.Wishes.Add(new WishDTO(post, id));
        }

        public async Task DeleteWishAsync(int wishId)
        {
            int index = _cache.Wishes.FindIndex(w => w.Id == wishId);
            if (index != -1) _cache.Wishes.RemoveAt(index);

            await _broker.WishClient.DeleteWishAsync(wishId);
        }
        
        public async Task<List<WishDTO>>? GetWishAsync()
        {
            _cache.Wishes ??= await _broker.WishClient.GetWishAsync();
            return _cache.Wishes;
        }

        public async Task DeleteWishFromPostAsync(int id)
        {
            int wishId = 0;
            int index = _cache.Wishes.FindIndex(w => w.Post.Id == id);
            if (index != -1)
            {
                wishId = _cache.Wishes[index].Id;
                _cache.Wishes.RemoveAt(index);
            }
            
            await _broker.WishClient.DeleteWishAsync(wishId);
        }
    }

    #endregion

    #region Product


    #endregion


}

