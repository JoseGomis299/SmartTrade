﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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

        public UserType LoggedType
        {
            get
            {
                if (Logged == null) return UserType.None;
                return Logged.GetUserType();
            }
        }

        public List<CartItem>? CartItems => _cache.CartItems; 

        private SmartTradeBroker _broker;
        private SmartTradeCache _cache;

        private static SmartTradeService? _instance;
        public static SmartTradeService Instance => _instance ??= new SmartTradeService();

        private SmartTradeService()
        {
            _broker = new SmartTradeBroker();
            _cache = new SmartTradeCache();
        }

        public void AddItemToCart(PostDTO post, int offerIndex, int quantity = 1)
        {
            _cache.AddItemToCart(post, offerIndex, quantity);
        }

        public void DeleteItemFromCart(int? postId, int offerIndex)
        {
            _cache.DeleteItemFromCart(postId, offerIndex);
        }

        #region User

        public void LogOut()
        {
            _broker.LogOut();
        }

        public async Task LogInAsync(string email, string password)
        {
           await _broker.UserClient.LogInAsync(email, password);
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

        public async Task<int> CreateWishAsync(int postId)
        {
            return await _broker.WishClient.CreateWishAsync(postId);
        }

        public async Task DeleteWishAsync(int wishId)
        {
            await _broker.WishClient.DeleteWishAsync(wishId);
        }

        public async Task<List<WishDTO?>> GetWishAsync()
        {
            return await _broker.WishClient.GetWishAsync();
        }

        #endregion

    }
}
