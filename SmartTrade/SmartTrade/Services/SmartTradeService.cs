using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuzzySharp;
using SmartTrade.Entities;
using SmartTrade.Helpers;
using SmartTradeDTOs;

namespace SmartTrade.Services;

    public class SmartTradeService
    {
        public List<SimplePostDTO>? Posts => _cache.Posts;
        public UserDTO? Logged => _broker.Logged;

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
        public List<AlertDTO>? Alerts => _cache.Alerts;
        public List<NotificationDTO>? Notifications => _cache.Notifications;
        public List<GiftListDTO>? GiftLists => _cache.GiftLists;
        public List<PurchaseDTO> Purchases => _cache.Purchases;
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

        public async Task BuyItemAsync(PostDTO post, OfferDTO offer, int quantity, int estimatedDays, Address deliveryAddress, Address billingAddress)
        {
            PurchaseDTO purchase = new PurchaseDTO(offer.Price, offer.ShippingCost, quantity, offer.Product.Id, post.SellerID, post, offer, DateTime.Now, DateTime.Now.AddDays(estimatedDays), deliveryAddress, billingAddress);
             
            _cache.Purchases.Add(purchase);
            await _broker.UserClient.AddPurchaseAsync(purchase);
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
            _cache.GiftLists = null;
            _cache.Purchases = null;
            _cache.Notifications = null;
            _cache.Alerts = null;
            _cache.Wishes = null;
        }

        public async Task LogInAsync(string email, string password)
        {
            int loadingScreen = LoadingScreenManager.Instance.StartLoading();
            await _broker.UserClient.LogInAsync(email, password);

            if (Logged == null)
            {
                LoadingScreenManager.Instance.StopLoading(loadingScreen);
                throw new Exception("Email or Password are incorrect");
            }
            
            await LoadCartItems();
            if (LoggedType == UserType.Consumer)
            {
                _cache.Purchases = await GetPurchasesAsync() ?? new List<PurchaseDTO>();
                _cache.Notifications = await GetNotificationsAsync() ?? new List<NotificationDTO>();
                _cache.Alerts = await GetAlertsAsync() ?? new List<AlertDTO>();
                _cache.Wishes = await GetWishAsync() ?? new List<WishDTO>();
                _cache.GiftLists = await LoadGiftListsAsync() ?? new List<GiftListDTO>();
            }

            LoadingScreenManager.Instance.StopLoading(loadingScreen);
        }

        public async Task RegisterConsumerAsync(string email, string password, string name, string lastnames, string dni, DateTime dateBirth, Address billingAddress, Address consumerAddress)
        {
            await _broker.UserClient.RegisterConsumerAsync(email, password, name, lastnames, dni, dateBirth, billingAddress, consumerAddress);
        }

        public async Task RegisterSellerAsync(string email, string password, string name, string lastnames, string dni, string companyName, string iban)
        {
            await _broker.UserClient.RegisterSellerAsync(email, password, name, lastnames, dni, companyName, iban);
        }

        public async Task AddPaypalAsync(PayPalInfo paypalinfo)
        {
            await _broker.UserClient.AddPaypalAsync(paypalinfo);
            (Logged as ConsumerDTO).PayPalAccounts.Add(paypalinfo);
        }
    /*
        public async Task DeletePaypalAsync(int addressId)
        {
            await _broker.UserClient.DeletePaypalAsync(addressId);

            int addressPaypal = (Logged as ConsumerDTO).PayPalAccounts.FindIndex(a => a.Id == addressId);
            (Logged as ConsumerDTO).Addresses.RemoveAt(addressPaypal);
        }
    */
        public async Task AddCreditCardAsync(CreditCardInfo creditCard)
        {
            await _broker.UserClient.AddCreditCardAsync(creditCard);
            (Logged as ConsumerDTO).CreditCards.Add(creditCard);
        }
    /*
        public async Task DeleteCreditCardAsync(int addressId)
        {
            await _broker.UserClient.DeleteCreditCardAsync(addressId);

            int creditCardIndex = (Logged as ConsumerDTO).Addresses.FindIndex(a => a.Id == addressId);
            (Logged as ConsumerDTO).CreditCards.RemoveAt(creditCardIndex);
        }
        */
        public async Task AddBizumAsync(BizumInfo bizum)
        {
           await _broker.UserClient.AddBizumAsync(bizum);
           (Logged as ConsumerDTO).BizumAccounts.Add(bizum);
        }
    /*
        public async Task DeleteBizumAsync(int addressId)
        {
            await _broker.UserClient.DeleteBizumAsync(addressId);

            int addressIndex = (Logged as ConsumerDTO).Addresses.FindIndex(a => a.Id == addressId);
            (Logged as ConsumerDTO).Addresses.RemoveAt(addressIndex);
        }
    */
        public async Task AddAddressAsync(Address address)
        {
            int addressId = await _broker.UserClient.AddAddressAsync(address);
            address.Id = addressId;

            (Logged as ConsumerDTO).Addresses.Add(address);
        }
    /*
        public async Task DeleteAddressAsync(int addressId)
        {
            await _broker.UserClient.DeleteAddressAsync(addressId);

            int addressIndex = (Logged as ConsumerDTO).Addresses.FindIndex(a => a.Id == addressId);
            (Logged as ConsumerDTO).Addresses.RemoveAt(addressIndex);
        }
    */
        public virtual async Task<List<PurchaseDTO>?> GetPurchasesAsync()
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
            if(Logged == null) await _cache.LoadCartItemsAsync();
            var guestItems = new List<CartItemDTO>(_cache.CartItems);
            var userItems = await _broker.UserClient.GetShoppingCartAsync();

            if (userItems == null) return;

            _cache.LoadCartItems(userItems);
            foreach (var cartItem in guestItems)
            {
                if (userItems.Any(x => x.Post.ProductName == cartItem.Post.ProductName)) continue;
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

        public virtual async Task<List<SimplePostDTO>?> RefreshPostsAsync()
        {
            var posts = await _broker.PostClient.GetPostsAsync();

            if (IsParentalControlEnabled)
            {
                posts = posts.Where(x => x.MinimumAge < 18).ToList();
                _cache.Purchases = _cache.Purchases.Where(x => posts.Exists(y => y.Id == x.Post.Id)).ToList();
            }
            _cache.SetPosts(posts);

            return posts;
        }

        public async Task<PostDTO> GetPostAsync(int postId)
        {
            PostDTO? post = _cache.GetPost(postId);
            if (post != null) return post;

            int i = LoadingScreenManager.Instance.StartLoading();

            post = await _broker.PostClient.GetPostAsync(postId);
            _cache.StorePost(post);

            LoadingScreenManager.Instance.StopLoading(i);
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

            _cache.SetNotifications(await _broker.NotificationClient.GetNotificationsAsync());
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

        public async Task<int> CreateAlertAsync(string productName)
        {
            if (_cache.Alerts.Exists(x => x.ProductName == productName)) return -1;
            
            int id = await _broker.AlertClient.CreateAlertAsync(productName);
            _cache.Alerts.Add(new AlertDTO(productName, Logged.Email, id));
            return id;
        }

        public async Task DeleteAlertAsync(string productName)
        {
            int index = _cache.Alerts.FindIndex(a => a.ProductName == productName);

            if (index != -1)
            {
                _cache.Alerts.RemoveAt(index);
                await _broker.AlertClient.DeleteAlertAsync(productName);
            }
        }
        
        public async Task<AlertDTO?> GetAlertAsync(string productName)
        {
            return await _broker.AlertClient.GetAlertAsync(productName);
        }

        public async Task<List<AlertDTO>?> GetAlertsAsync()
        {
            return await _broker.AlertClient.GetAlertsAsync();
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
        
        public async Task<List<WishDTO>?> GetWishAsync()
        {
            if(WishList != null) return WishList;

             _cache.Wishes = await _broker.WishClient.GetWishAsync(); 
             return WishList;
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

        #endregion

        #region Gifts
        public async Task AddGiftListAsync(string name, DateOnly? date)
        {
            if (Logged == null)
            {
                return;
            }
            _cache.AddGiftList(name, date, Logged.Email);
            await _broker.UserClient.AddGiftListAsync(new SimpleGiftListDTO(name, date.Value.ToDateTime(new TimeOnly()), Logged.Email));
        }

        public async Task EditGiftListAsync(string name, string newName, DateOnly? date)
        {
            if (Logged == null)
            {
                return;
            }
            int id = _cache.EditGiftList(name, newName, date);
            if (id == -1)
            {
                return;
            }

            await _broker.UserClient.AddGiftListAsync(new SimpleGiftListDTO(newName, date?.ToDateTime(new TimeOnly()), Logged.Email, id));
        }

        public async Task RemoveGiftListAsync(string listName)
        {
            if (Logged == null)
            {
                return;
            }
            _cache.RemoveGiftList(listName);
            await _broker.UserClient.RemoveGiftListAsync(listName);
        }

        public async Task<List<GiftListDTO>?> LoadGiftListsAsync()
        {
            if (_cache.GiftLists == null)
            {
                var res = await _broker.UserClient.GetGiftListsAsync();
                _cache.GiftLists = res ?? new List<GiftListDTO>();
            }
            return _cache.GiftLists;
        }

        public List<String> GetGiftListNames()
        {
            List<String> listNames = new List<String>();

            foreach(GiftListDTO list in _cache.GiftLists)
            {
                listNames.Add(list.Name);
            }

            return listNames;
        }

        public async Task AddGiftAsync(int quantity, PostDTO post, OfferDTO offer, string giftListName)
        {
            int count;

            if (Logged == null)
            {
                return;
            }

            count = _cache.AddGift(quantity, post, offer, giftListName);
            await _broker.UserClient.AddGiftAsync(new SimpleGiftDTO(count, (int)post.Id, (int)offer.Id, giftListName));
        }
        
        public async Task RemoveGiftAsync(int quantity, int postId, int OfferId, string giftListName)
        {
            if (Logged == null)
            {
                return;
            }
            _cache.RemoveGift(giftListName, OfferId);
            await _broker.UserClient.RemoveGiftAsync(new SimpleGiftDTO(quantity, postId, OfferId, giftListName));
        }
    #endregion

    #region Product


    #endregion

        #region Ratings

        public async Task CreateRatingAsync(PostDTO post, int points, string description)
        {
            PostDTO? cachedPost = _cache.GetPost((int)post.Id);
            if (cachedPost != null)
            {
                int index = cachedPost.Ratings.FindIndex(x => x.UserId == Logged.Email);

                if (index == -1)
                {
                    cachedPost.AddRating(new RatingDTO(post, (ConsumerDTO)Logged, points, description));
                }
                else cachedPost.AddRating(new RatingDTO(post, (ConsumerDTO)Logged, points, description), index);
            }
            await _broker.RatingClient.CreateRatingAsync(new RatingDTO(post, (ConsumerDTO)Logged, points, description));
        }

        public async Task DeleteRatingAsync(int ratingId)
        {
            await _broker.RatingClient.DeleteRatingAsync(ratingId);
        }

        public async Task<List<RatingDTO>?> GetRatingAsync(int postId)
        {
            List<RatingDTO> RatingList = new List<RatingDTO>();
            RatingList = await _broker.RatingClient.GetRatingListAsync(postId);
            return RatingList;
        }


    #endregion

        public void ClearPostCache()
        {
            _cache.ClearPostCache();
        }

        public void AddPurchaseTest(List<PurchaseDTO> purchases)
        {
            _cache.Purchases = purchases;
        }

        public void AddPostTest(List<SimplePostDTO> posts)
        {
            _cache.SetPosts(posts);
        }

        public void SetLoggedTest(UserDTO user)
        {
            _broker.Logged = user;
        }
    }
