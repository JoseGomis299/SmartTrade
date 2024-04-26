﻿using FuzzySharp;
using SmartTradeDTOs;
using Newtonsoft.Json;
using SmartTrade.Entities;
using SmartTrade.Persistence;
using Microsoft.EntityFrameworkCore;
using SmartTradeAPI.Library.Persistence.DTOs;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace SmartTrade.BusinessLogic;

public class SmartTradeService : ISmartTradeService
{
    private readonly IDAL _dal;

    public SmartTradeService()
    {
        _dal = new EntityFrameworkDAL(new SmartTradeContext());
    }
    public void SaveChanges()
    {
        _dal.Commit();
    }

    public void RemoveAll()
    {
        _dal.RemoveAllData();
    }

    public void AddAdmin(Admin admin)
    {
        _dal.Insert<Admin>(admin);
        _dal.Commit();
    }

    public void AddCostumer(Consumer costumer)
    {
        _dal.Insert<Consumer>(costumer);
        _dal.Commit();
    }

    public void AddSeller(Seller seller)
    {
        _dal.Insert<Seller>(seller);
        _dal.Commit();
    }


    public Post AddPost(PostDTO postInfo, string loggedID)
    {
        //LogIn("ChiclesPepito@gmail.com", "123");
        User? logged = _dal.GetById<User>(loggedID);

        Seller? seller = null;
        if(postInfo.SellerID != null) seller = _dal.GetById<Seller>(postInfo.SellerID);

        Post post = new Post(postInfo.Title, postInfo.Description, postInfo.Validated || logged is Admin, seller ??= (Seller)logged);

        List<Product> products = new();
        List<Offer> offers = new();

        //var dbProducts = _dal.GetAll<Product>();

        for (int i = 0; i < postInfo.Offers.Count; i++)
        {
            OfferDTO offerDto = postInfo.Offers[i];
            ProductDTO productDto = offerDto.Product;

            Product product = GetProductFactory(postInfo.Category).CreateProduct(postInfo.ProductName, postInfo.Certifications, postInfo.EcologicPrint, postInfo.MinimumAge, postInfo.HowToUse, postInfo.HowToReducePrint, productDto.Attributes);
            foreach (var image in productDto.Images)
            {
                product.AddImage(new Image(image));
            }

            var dbProducts = _dal.GetWhere<Product>(p => p.Name == product.Name).ToList();

            foreach (var prod in dbProducts)
            {
                if (!prod.Equals(product)) continue;
                foreach (var image in product.Images)
                {
                    prod.AddImage(image);
                }

                product = prod;
                break;
            }
            products.Add(product);

            offers.Add(new Offer(product, offerDto.Price, offerDto.ShippingCost, offerDto.Stock));
        }

        seller.Posts.Add(post);
        post.Offers = offers;

        for (int i = 0; i < products.Count; i++)
        {
            products[i].AddPost(post);
            offers[i].Post = post;
        }

        _dal.Commit();
        return post;

        ProductFactory GetProductFactory(Category category)
        {
            switch (category)
            {
                case Category.Toy:
                    return new ToyFactory();
                case Category.Nutrition:
                    return new NutritionFactory();
                case Category.Clothing:
                    return new ClothingFactory();
                case Category.Book:
                    return new BookFactory();
                default:
                    throw new ArgumentException("Invalid category");
            }
        }
    }

    public void EditPost(int postID, PostDTO postInfo, string loggedID)
    {
        Post post = _dal.GetById<Post>(postID);
        
        post.Title = postInfo.Title;
        post.Description = postInfo.Description;
        post.Validated = postInfo.Validated;
                
        List<Offer> offers = new();
        List<Offer> originalOffers = post.Offers.ToList();

        foreach (var originalOffer in originalOffers)
        {
            OfferDTO? offerDto = postInfo.Offers.FirstOrDefault(x => x.Id == originalOffer.Id);
            if (offerDto == null) continue;
            
            originalOffer.Price = offerDto.Price;
            originalOffer.ShippingCost = offerDto.ShippingCost;
            originalOffer.Stock = offerDto.Stock;

            originalOffer.Product.Name = postInfo.ProductName;
            originalOffer.Product.Certification = postInfo.Certifications;
            originalOffer.Product.EcologicPrint = postInfo.EcologicPrint;
            originalOffer.Product.MinimumAge = postInfo.MinimumAge;
            originalOffer.Product.HowToUse = postInfo.HowToUse;
            originalOffer.Product.HowToReducePrint = postInfo.HowToReducePrint;

            if (originalOffer.Product is Nutrition nutrition)
            {
                nutrition.Weight = offerDto.Product.Attributes["Weight"];
            }else if (originalOffer.Product is Toy toy)
            {
                toy.Material = offerDto.Product.Attributes["Material"];
            }else if (originalOffer.Product is Clothing clothing)
            {
                clothing.Size = offerDto.Product.Attributes["Size"];
                clothing.Color = offerDto.Product.Attributes["Color"];
            }else if (originalOffer.Product is Book book)
            {
                book.Language = offerDto.Product.Attributes["Language"];
            }

            List<Image> images = new();
            for (int i = 0; i < originalOffer.Product.Images.Count; i++)
            {
                if(offerDto.Product.Images.Count <= i) break;
                Image image = originalOffer.Product.GetImage(i);
                image.ImageSource = offerDto.Product.Images[i];
                images.Add(image);
            }
            originalOffer.Product.Images = images;
            offers.Add(originalOffer);
        }
        post.Offers = offers;

        //TODO: Si el postInfo está validado creamos la notificación pertinente si toca crear
       if(postInfo.Validated) CreateNotifications(post);

        _dal.Commit();
    }

    private void CreateNotifications(Post post)
    {
        foreach (var alert in post.Offers.First().Product.Alerts)
        {
            var notification = new Notification(false, (Consumer)alert.User, post);
            _dal.Insert(notification);
        }
    }

    public List<SimplePostDTO> GetPosts(string? loggedID)
    { 
        User? logged = _dal.GetById<User>(loggedID);

        var postDtos = _dal.GetAll<Post>().AsNoTracking()
            .Select(p => new
            {
                Id = p.Id,
                Title = p.Title,
                Category = p.Offers.Select(o => o.Product.GetCategories().First()).FirstOrDefault(),
                MinimumAge = p.Offers.Select(o => o.Product.MinimumAge).FirstOrDefault(),
                EcologicPrint = p.Offers.Select(o => o.Product.EcologicPrint).FirstOrDefault(),
                Validated = p.Validated,
                SellerEmail = p.Seller.Email,
                Price = p.Offers.Select(o => o.Price).FirstOrDefault(),
                ImageSource = p.Offers.Select(o => o.Product.Images.Select(i => i.ImageSource).FirstOrDefault()).FirstOrDefault(),
                ProductName = p.Offers.Select(o => o.Product.Name).FirstOrDefault()
            })
            .ToList()
            .Select(anon => new SimplePostDTO
            {
                Id = anon.Id,
                Title = anon.Title,
                Category = anon.Category,
                MinimumAge = anon.MinimumAge,
                EcologicPrint = anon.EcologicPrint,
                Validated = anon.Validated,
                SellerID = anon.SellerEmail,
                Price = anon.Price,
                Image = anon.ImageSource,
                ProductName = anon.ProductName
            })
            .ToList();

        if (logged is Admin) postDtos = postDtos.Where(x => !x.Validated).ToList();
        else if (logged is Seller seller) postDtos = postDtos.Where(x => x.SellerID == seller.Email).ToList();
        else postDtos = postDtos.Where(x => x.Validated).ToList();

        return postDtos;
    }

    public PostDTO GetPost(int postId)
    {
        return new PostDTO(_dal.GetById<Post>(postId));
    }

    public List<SimplePostDTO> GetPostsFuzzyContain(string searchFor)
    {
        return _dal.GetAll<Post>().AsNoTracking()
            .Select(p => new
            {
                Id = p.Id,
                Title = p.Title,
                Category = p.Offers.Select(o => o.Product.GetCategories().First()).FirstOrDefault(),
                MinimumAge = p.Offers.Select(o => o.Product.MinimumAge).FirstOrDefault(),
                EcologicPrint = p.Offers.Select(o => o.Product.EcologicPrint).FirstOrDefault(),
                Validated = p.Validated,
                SellerEmail = p.Seller.Email,
                Price = p.Offers.Select(o => o.Price).FirstOrDefault(),
                ImageSource = p.Offers.Select(o => o.Product.Images.Select(i => i.ImageSource).FirstOrDefault()).FirstOrDefault(),
                ProductName = p.Offers.Select(o => o.Product.Name).FirstOrDefault()
            }).ToList()
            .Select(anon => new SimplePostDTO
            {
                Id = anon.Id,
                Title = anon.Title,
                Category = anon.Category,
                MinimumAge = anon.MinimumAge,
                EcologicPrint = anon.EcologicPrint,
                Validated = anon.Validated,
                SellerID = anon.SellerEmail,
                Price = anon.Price,
                Image = anon.ImageSource,
                ProductName = anon.ProductName
            }).Where(x => Fuzz.PartialTokenSortRatio(searchFor, x.Title) > 60)
            .OrderByDescending(x => Fuzz.PartialTokenSortRatio(searchFor, x.Title))
            .ToList();
    }

    public List<string> GetPostsNamesStartWith(string startWith, int numPosts)
    {
        var res = _dal.GetWhere<Post>(x => x.Title.StartsWith(startWith)).Select(y => new string(y.Title)).ToList();
        return res.Take(Math.Min(numPosts, res.Count)).ToList();
    }

    public List<string> GetPostNames()
    {
        return _dal.GetAll<Post>().Select(x => x.Title).ToList();
    }

    public void DeletePost(int postID)
    {
        Post post = _dal.GetById<Post>(postID);
        post.Seller.Posts.Remove(post);

        foreach (var offer in post.Offers)
        {
            Product product = offer.Product;

            product.Posts.Remove(post);
            if (product.Posts.Count == 0)
            {
                foreach (var image in product.Images)
                    _dal.Delete<Image>(image);
                product.Images.Clear();
                _dal.Delete<Product>(product);
            }
            _dal.Delete<Offer>(offer);
        }
        post.Offers.Clear();
        _dal.Delete<Post>(post);

        _dal.Commit();
    }

    public ConsumerDTO RegisterConsumer(ConsumerRegisterData registerData)
    {
        if (_dal.GetWhere<Consumer>(x => x.Email == registerData.Email).Any() || _dal.GetWhere<Consumer>(x => x.DNI == registerData.DNI).Any())
        {
            throw new Exception("Existing user");
        }

        var address = new Address(registerData.Address.Street, registerData.Address.Number, registerData.Address.City, registerData.Address.PostalCode, registerData.Address.Number, registerData.Address.Door);
        var billingAddress = new Address(registerData.BillingAddress.Street, registerData.BillingAddress.Number, registerData.BillingAddress.City, registerData.BillingAddress.PostalCode, registerData.BillingAddress.Number, registerData.BillingAddress.Door);

        Consumer consumer = new Consumer(registerData.Email, registerData.Password, registerData.Name, registerData.LastNames, registerData.DNI, registerData.BirthDate, billingAddress, address);
        _dal.Insert<Consumer>(consumer);
        _dal.Commit();

        return new ConsumerDTO(consumer);
    }

    public SellerDTO RegisterSeller(SellerRegisterData registerData)
    {
        if (_dal.GetWhere<Seller>(x => x.Email == registerData.Email).Any() || _dal.GetWhere<Seller>(x => x.DNI == registerData.DNI).Any())
        {
            throw new Exception("Existing user");
        }

        Seller seller = new Seller(registerData.Email, registerData.Password, registerData.Name, registerData.LastNames, registerData.DNI, registerData.CompanyName, registerData.IBAN);
        _dal.Insert<Seller>(seller);
        _dal.Commit();

        return new SellerDTO(seller);
    }

    public void RegisterAdmin(AdminRegisterData registerData)
    {
        if (_dal.GetWhere<Admin>(x => x.Email == registerData.Email).Any())
        {
            throw new Exception("Existing user");
        }

        Admin admin = new Admin(registerData.Email, registerData.Password, registerData.Name, registerData.LastNames);
        _dal.Insert<Admin>(admin);
        _dal.Commit();
    }

    public void AddPaypal(PayPalInfo paypalInfo, string loggedID) 
    {
        Consumer? loggedConsumer = _dal.GetById<Consumer>(loggedID);
        loggedConsumer.AddPaymentMethod(paypalInfo);
        _dal.Commit();
    }

    public void AddCreditCard(CreditCardInfo creditCardInfo, string loggedID)
    {
        Consumer? loggedConsumer = _dal.GetById<Consumer>(loggedID);
        loggedConsumer.AddPaymentMethod(creditCardInfo);
        _dal.Commit();
    }

    public void AddBizum(BizumInfo bizum, string loggedID)
    {
        Consumer? loggedConsumer = _dal.GetById<Consumer>(loggedID);
        loggedConsumer.AddPaymentMethod(bizum);
        _dal.Commit();
    }

    public UserDTO LogIn(string email, string password)
    {
        if (!_dal.GetWhere<User>(x => x.Email == email).Any()) throw new Exception("Unregistered user");
            
        User user = _dal.GetById<User>(email);
        if (user.Password != password) throw new Exception("Incorrect password");

        UserDTO userDto;
        if (user is Consumer consumer)
        {
            userDto = new ConsumerDTO(consumer);
        }
        else if (user is Seller seller)
        {
            userDto = new SellerDTO(seller);
        }
        else
        {
            userDto = new UserDTO(user);
        }

        return userDto;
    }

    public List<NotificationDTO> GetNotifications(string loggedId)
    {
       return _dal.GetAll<Notification>().AsNoTracking().Where(n => n.TargetUser.Email == loggedId)
           .Select(n => new
           {
               Id = n.Id,
               Visited = n.Visited,
               ConsumerId = n.TargetUser.Email,
               Post = new SimplePostDTO()
               {
                   Id = n.TargetPost.Id,
                   Title = n.TargetPost.Title,
                   Category = n.TargetPost.Offers.Select(o => o.Product.GetCategories().First()).FirstOrDefault(),
                   MinimumAge = n.TargetPost.Offers.Select(o => o.Product.MinimumAge).FirstOrDefault(),
                   EcologicPrint = n.TargetPost.Offers.Select(o => o.Product.EcologicPrint).FirstOrDefault(),
                   Validated = n.TargetPost.Validated,
                   SellerID = n.TargetPost.Seller.Email,
                   Price = n.TargetPost.Offers.Select(o => o.Price).FirstOrDefault(),
                   Image = n.TargetPost.Offers.Select(o => o.Product.Images.Select(i => i.ImageSource).FirstOrDefault()).FirstOrDefault(),
                   ProductName = n.TargetPost.Offers.Select(o => o.Product.Name).FirstOrDefault()
               }
           }).ToList()
           .Select(anon => new NotificationDTO
           {
               Id = anon.Id,
               ConsumerId = anon.ConsumerId,
               Post = anon.Post,
               Visited = anon.Visited
           }).ToList();
    }

    public int CreateAlert(string userId, int productId)
    {
        var product =_dal.GetById<Product>(productId);
        var user = _dal.GetById<User>(userId);
        var alert = new Alert(user, product);
        product.AddAlert(alert);
        user.AddAlert(alert);
        _dal.Commit();
        return alert.Id;
    }

    public void DeleteAlert(int alertId)
    {
        Alert alert = _dal.GetById<Alert>(alertId);
        alert.Product.Alerts.Remove(alert);
        alert.User.Alerts.Remove(alert);

        _dal.Delete<Alert>(alert);

        _dal.Commit();
    }

    public AlertDTO GetAlert(string productName)
    {
        return _dal.GetWhere<AlertDTO>(n => n.ProductName == productName).FirstOrDefault();
    }

    public void DeleteNotification(int id)
    {
        _dal.Delete<Notification>(_dal.GetById<Notification>(id));
        _dal.Commit();
    }

    public void SetVisited(int id)
    {
        _dal.GetById<Notification>(id).Visited = true;
        _dal.Commit();
    }

    public int CreateWish(string userId, int postId)
    {
        var post = _dal.GetById<Post>(postId);
        var user = _dal.GetById<User>(userId);
        var wish = new Wish(user, post);
        user.AddWish(wish);
        _dal.Commit();
        return wish.Id;
    }

    public List<WishDTO> GetWishList(string loggedId)
    {
        return _dal.GetAll<Wish>().AsNoTracking().Where(n => n.User.Email == loggedId)
            .Select(n => new
            {
                Id = n.Id,
                User = n.User.Email,
                Post = new SimplePostDTO()
                {
                    Id = n.Post.Id,
                    Title = n.Post.Title,
                    Category = n.Post.Offers.Select(o => o.Product.GetCategories().First()).FirstOrDefault(),
                    MinimumAge = n.Post.Offers.Select(o => o.Product.MinimumAge).FirstOrDefault(),
                    EcologicPrint = n.Post.Offers.Select(o => o.Product.EcologicPrint).FirstOrDefault(),
                    Validated = n.Post.Validated,
                    SellerID = n.Post.Seller.Email,
                    Price = n.Post.Offers.Select(o => o.Price).FirstOrDefault(),
                    Image = n.Post.Offers.Select(o => o.Product.Images.Select(i => i.ImageSource).FirstOrDefault()).FirstOrDefault(),
                    ProductName = n.Post.Offers.Select(o => o.Product.Name).FirstOrDefault()
                }
            }).ToList()
            .Select(anon => new WishDTO()
            {
                Id = anon.Id,
                UserId = anon.User,
                Post = anon.Post
            }).ToList();
    }


    public void DeleteWish(int wishId)
    {
        Wish wish = _dal.GetById<Wish>(wishId);
        wish.User.WishList.Remove(wish);

        _dal.Delete<Wish>(wish);

        _dal.Commit();
    }

    public void AddToCart(string consumerId, CartItemDTO cartItemDTO)
    {
        Consumer consumer = _dal.GetById<Consumer>(consumerId);
        Post post = _dal.GetById<Post>(cartItemDTO.Post.Id);
        Offer offer = _dal.GetById<Offer>(cartItemDTO.Offer.Id);
        consumer.AddToCart(new CartItem(post, offer, cartItemDTO.Quantity));
        _dal.Commit();
    }

    public void RemoveFromCart(string consumerId, int offerId)
    {
        Consumer consumer = _dal.GetById<Consumer>(consumerId);
        consumer.RemoveFromCart(offerId);
        _dal.Commit();
    }

    public List<CartItemDTO> GetShoppingCart(string consumerId)
    {
        Consumer consumer = _dal.GetById<Consumer>(consumerId);

        return consumer.ShoppingCart.AsQueryable().Select(c => new CartItemDTO
        {
            Offer = new OfferDTO
            {
                Id = c.Offer.Id,
                Price = c.Offer.Price,
                ShippingCost = c.Offer.ShippingCost,
                Stock = c.Offer.Stock,
                Product = new ProductDTO
                {
                    Id = c.Offer.Product.Id,
                    Images = new List<byte[]>(){ c.Offer.Product.Images.First().ImageSource }
                }
            },
            Post = GetPost(c.Post.Id),
            Quantity = c.Quantity
        }).ToList();
    }

    public void AddPurchase(string userId, PurchaseDTO purchaseDTO)
    {
        Consumer? logged = _dal.GetById<Consumer>(userId);
       // logged.AddPurchases(purchaseDTO);
        _dal.Commit();
    }

    public List<PurchaseDTO> GetPurchases(string? emailconsumer)
    {
        Consumer? logged = _dal.GetById<Consumer>(emailconsumer);
        return (logged.Purchases.AsQueryable())
        .Select(p => new PurchaseDTO
        {
            Image = p.PurchaseProduct.Images.First().ImageSource,
            ProductId = p.PurchaseProduct.Id,
            PostId = p.PurchasePost.Id,
            EmailSeller = p.PurchaseSeller.Email,
            OfferId = p.PurchaseOffer.Id,
            Price = p.Price,
            ShippingPrice = p.ShippingPrice
        }).ToList();
    }
}
