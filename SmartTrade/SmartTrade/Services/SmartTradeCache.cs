using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Platform;
using Newtonsoft.Json;
using SmartTradeDTOs;

namespace SmartTrade.Services
{
    public class SmartTradeCache
    {
        public event Action? OnPostsChanged;
        private List<PostDTO> _completePosts = new List<PostDTO>();
        private List<SimplePostDTO>? _simplePosts;
        public List<SimplePostDTO>? Posts => _simplePosts;
        public List<NotificationDTO>? Notifications { get; set; }

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
    }
}
