using System;
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
        public List<SimplePostDTO>? Posts => _proxy.Posts;
        public UserDTO? Logged => _broker.Logged;

        public event Action OnPostsChanged
        {
            add => _proxy.OnPostsChanged += value;
            remove => _proxy.OnPostsChanged -= value;
        }

        public UserType LoggedType
        {
            get
            {
                if (Logged == null) return UserType.None;
                return Logged.GetUserType();
            }
        }

        private SmartTradeBroker _broker;
        private SmartTradeProxy _proxy;

        public SmartTradeService(SmartTradeBroker broker, SmartTradeProxy proxy)
        {
            _broker = broker;
            _proxy = proxy;
        }

        #region User

        public void LogOut()
        {
            _broker.LogOut();
        }

        public async Task LogInAsync(string email, string password)
        {
           await _broker.LogInAsync(email, password);
        }

        public async Task RegisterConsumerAsync(string email, string password, string name, string lastnames, string dni, DateTime dateBirth, Address billingAddress, Address consumerAddress)
        {
            await _broker.RegisterConsumerAsync(email, password, name, lastnames, dni, dateBirth, billingAddress, consumerAddress);
        }

        public async Task RegisterSellerAsync(string email, string password, string name, string lastnames, string dni, string companyName, string iban)
        {
            await _broker.RegisterSellerAsync(email, password, name, lastnames, dni, companyName, iban);
        }

        public async Task AddPaypalAsync(PayPalInfo paypalinfo, string loggedID)
        {
            await _broker.AddPaypalAsync(paypalinfo, loggedID);
        }

        public async Task AddCreditCardAsync(CreditCardInfo creditCard)
        {
            await _broker.AddCreditCardAsync(creditCard);
        }

        public async Task AddBizumAsync(BizumInfo bizum)
        {
           await _broker.AddBizumAsync(bizum);
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
            await _broker.AddPostAsync(post);
            _proxy.SetPosts(await _broker.GetPostsAsync());
            
        }

        public async Task<List<SimplePostDTO>?> RefreshPostsAsync()
        {
            var posts = await _broker.GetPostsAsync();
            _proxy.SetPosts(posts);
            return posts;
        }

        public async Task<PostDTO> GetPostAsync(int postId)
        {
            PostDTO? post = _proxy.GetPost(postId);
            if (post != null) return post;

            post = await _broker.GetPostAsync(postId);
            _proxy.StorePost(post);
            return post;
        }

        public async Task EditPostAsync(int postId, PostDTO postInfo)
        {
            await _broker.EditPostAsync(postId, postInfo);
        }

        public async Task DeletePostAsync(int postId)
        {
            _proxy.RemovePost(postId);
            await _broker.DeletePostAsync(postId);
        }

        #endregion

        #region Notification

        public async Task<List<NotificationDTO>?> GetNotificationsAsync()
        {
            if(_proxy.Notifications != null) return _proxy.Notifications;

            _proxy.Notifications = await _broker.GetNotificationsAsync();
            return _proxy.Notifications;
        }

        public async Task DeleteNotificationAsync(int notificationId)
        {
            _proxy.RemoveNotification(notificationId);
            await _broker.DeleteNotificationAsync(notificationId);
        }

        public async Task SetNotificationAsVisitedAsync(int notificationId)
        {
           _proxy.MarkNotificationAsVisited(notificationId);
           await _broker.SetNotificationAsVisitedAsync(notificationId);
        }

        #endregion

        #region Alert

        public async Task<int> CreateAlertAsync(int productId)
        {
            return await _broker.CreateAlertAsync(productId);
        }

        public async Task DeleteAlertAsync(int alertId)
        {
            await _broker.DeleteAlertAsync(alertId);
        }

        public async Task<AlertDTO?> GetAlertsAsync(string productName)
        {
            return await _broker.GetAlertsAsync(productName);
        }

        #endregion

    }
}
