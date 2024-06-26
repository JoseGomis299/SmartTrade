﻿using SmartTrade.Entities;
using SmartTradeDTOs;

namespace SmartTrade.BusinessLogic
{
    public interface ISmartTradeService
    {
        public void SaveChanges();
        public void RemoveAll();

        public void AddAdmin(Admin admin);
        public void AddCostumer(Consumer costumer);
        public void AddSeller(Seller seller);
        public void AddPaypal(PayPalInfo paypalInfo, string loggedId);
        public void AddBizum(BizumInfo bizumInfo, string loggedId);

        public void AddCreditCard(CreditCardInfo creditcard, string loggedId);

        public Post AddPost(PostDTO postInfo, string loggedID);

        public void EditPost(int postID, PostDTO postInfo, string loggedID);

        public UserDTO LogIn(string email, string password);

        public SellerDTO RegisterSeller(SellerRegisterData registerData);

        public ConsumerDTO RegisterConsumer(ConsumerRegisterData registerData);
        public void RegisterAdmin(AdminRegisterData registerData);
        public int CreateAlert(string userId, string productName);

        public void DeleteAlert(int alertId);
        public void DeleteAlert(string productName, string loggedId);
        public void DeletePost(int postID);

        public List<SimplePostDTO> GetPosts(string? loggedID);
        public PostDTO GetPost(int postId);
        public List<SimplePostDTO> GetPostsFuzzyContain(string searchFor);
        public List<String> GetPostsNamesStartWith(string startWith, int numPosts);
        public List<string> GetPostNames();
        public List<NotificationDTO> GetNotifications(string loggedId);
        public AlertDTO GetAlert(string productName, string loggedId);
        public void DeleteNotification(int notificationId);
        public void SetVisited(int id);
        public int CreateWish(string? loggedId, int id);
        public List<WishDTO> GetWishList(string loggedId);
        public void DeleteWish(int id);

        public void AddToCart(string consumerId, SimpleCartItemDTO  cartItemDTO);
        public void RemoveFromCart(string consumerId, int id);
        public List<CartItemDTO> GetShoppingCart(string consumerId);

        public void AddPurchase(string userId, PurchaseDTO purchaseDTO);
        public List<PurchaseDTO> GetPurchases(string? emailConsumer);
        public void AddGiftList(string consumerId, SimpleGiftListDTO giftListDTO);
        public void RemoveGiftList(string consumerId, string listName);
        public List<GiftListDTO> GetGiftLists(string consumerId);
        public void AddGift(string consumerId, SimpleGiftDTO giftDTO);
        public void RemoveGift(string consumerId, SimpleGiftDTO giftDTO);
        public List<AlertDTO> GetAlerts(string loggedId);
        public int AddAddress(string? loggedId, Address address);

        public void AddRating(int points, string description, string consumerId, int postId);
        public List<RatingDTO> GetRatings(int postId);
        public void DeleteRating(int ratingId);

    }
}
