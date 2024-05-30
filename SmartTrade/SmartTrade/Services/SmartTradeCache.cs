using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartTrade.Entities;
using SmartTrade.Helpers;
using SmartTradeDTOs;

namespace SmartTrade.Services
{
    public class SmartTradeCache
    {
        private List<PostDTO> _completePosts = new List<PostDTO>();
        private List<SimplePostDTO>? _simplePosts;
        public List<SimplePostDTO>? Posts => _simplePosts;
        public List<NotificationDTO>? Notifications { get; set; }
        public List<CartItemDTO>? CartItems { get; set; } = new List<CartItemDTO>();
        public List<PurchaseDTO>? Purchases { get; set; }
        public List<WishDTO>? Wishes { get; set; } = new List<WishDTO>();
        public List<GiftListDTO>? GiftLists { get; set; }
        public List<AlertDTO>? Alerts { get; set; }

        public SmartTradeCache()
        {
            EventBus.RegisterEvent("OnCartChanged");
            EventBus.RegisterEvent("OnPostsChanged");
            EventBus.RegisterEvent("OnGiftsChanged");
            EventBus.RegisterEvent("OnNotificationsChanged");
        }

        public async Task LoadCartItemsAsync()
        {
            CartItems = await JSONsaving.ReadFromJsonFileAsync<List<CartItemDTO>>("ShoppingCartItems") ?? new List<CartItemDTO>();
            await JSONsaving.WriteToJsonFileAsync(CartItems, "ShoppingCartItems");

            EventBus.Publish("OnCartChanged");
        }
        public void SetCartItems(List<CartItemDTO> items)
        {
            CartItems = items;
            EventBus.Publish("OnCartChanged");
        }

        public void SetPosts(List<SimplePostDTO>? posts)
        {
            _simplePosts = posts;
            EventBus.Publish("OnPostsChanged");
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
            EventBus.Publish("OnNotificationsChanged");
        }

        public void MarkNotificationAsVisited(int notificationId)
        {
            NotificationDTO? notification = GetNotification(notificationId);
            if (notification != null) notification.Visited = true;
            EventBus.Publish("OnNotificationsChanged");
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

            EventBus.Publish("OnCartChanged");
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

            EventBus.Publish("OnCartChanged");

            return index == -1 ? quantity : CartItems[index].Quantity;
        }

        public void DeleteItemFromCart(int offerId)
        {
            int index = CartItems.FindIndex(x=> x.Offer.Id == offerId);
            if (index != -1) CartItems.RemoveAt(index);

            EventBus.Publish("OnCartChanged");
        }

        public async Task DeleteItemFromCartAsync(int offerId)
        {
            int index = CartItems.FindIndex(x => x.Offer.Id == offerId);
            if (index != -1) CartItems.RemoveAt(index);

            EventBus.Publish("OnCartChanged");
            await JSONsaving.WriteToJsonFileAsync(CartItems, "ShoppingCartItems");
        }

        public void AddGiftList(string name, DateOnly? date, string consumerId)
        {
            GiftLists.Add(new GiftListDTO(name, date, consumerId, new List<GiftDTO>()));
        }

        public int EditGiftList(string name, string newName, DateOnly? date)
        {
            var index = GiftLists.FindIndex(x => x.Name == name);
            if (index != -1)
            {
                GiftLists[index].Name = newName;
                GiftLists[index].Date = date;

                foreach (var gift in GiftLists[index].Gifts)
                {
                    gift.GiftListName = newName;
                }
                
                return GiftLists[index].Id ?? -1;
            }

            return -1;
        }

        public void RemoveGiftList(string listName)
        {
            int index = GiftLists.FindIndex(x => x.Name == listName);
            if (index != -1) GiftLists.RemoveAt(index);
        }

        public void LoadGiftLists(List<GiftListDTO> giftLists)
        {
            GiftLists = giftLists;
        }

        public int AddGift(int quantity, PostDTO post, OfferDTO offer, string giftListName)
        {
            int indexList = GiftLists.FindIndex(x => x.Name == giftListName);
            int indexGift = GiftLists[indexList].Gifts.FindIndex(x => x.Offer.Id == offer.Id);

            if (indexGift != -1)
            {
                GiftLists[indexList].Gifts[indexGift].Quantity += quantity;
            }
            else
            {
                GiftLists[indexList].Gifts.Add(new GiftDTO(quantity, post, offer, giftListName));
            }

            EventBus.Publish("OnGiftsChanged");
            return indexGift == -1 ? quantity : GiftLists[indexList].Gifts[indexGift].Quantity;
        }

        public void RemoveGift(string GiftListName, int offerId)
        {
            var indexList = GiftLists.FindIndex(x => x.Name == GiftListName);
            var indexGift = GiftLists[indexList].Gifts.FindIndex(x => x.Offer.Id == offerId);
            if (indexGift != -1) GiftLists[indexList].Gifts.RemoveAt(indexGift);

            EventBus.Publish("OnGiftsChanged");
        }

        public void AddPurchase(float price, float shippingPrice, int quantity, int productId, string emailSeller, PostDTO post, OfferDTO offer, DateTime purchaseDate, DateTime expectedDate, Address deliveryAddress, Address billingAddress)
        {
            Purchases.Add(new PurchaseDTO(price, shippingPrice, quantity, productId, emailSeller, post, offer, purchaseDate, expectedDate, deliveryAddress, billingAddress));
        }

        public void SetNotifications(List<NotificationDTO>? getNotificationsAsync)
        {
            Notifications = getNotificationsAsync;
            EventBus.Publish("OnGiftsChanged");
        }

        public void ClearPostCache()
        {
            _completePosts.Clear();
            _simplePosts?.Clear();
        }
    }
}
