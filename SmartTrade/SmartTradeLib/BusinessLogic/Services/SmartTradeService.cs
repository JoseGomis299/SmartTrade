﻿using FuzzySharp;
using SmartTradeLib.Entities;
using SmartTradeLib.Persistence;
using SmartTradeDTOs;
using Newtonsoft.Json;

namespace SmartTradeLib.BusinessLogic;

public class SmartTradeService : ISmartTradeService
{
    private readonly IDAL _dal;
    public User? Logged {get; set; }


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

    public Post AddPost(string postInfoJson)
    {
        //LogIn("ChiclesPepito@gmail.com", "123");

        PostDTO postDto = JsonConvert.DeserializeObject<PostDTO>(postInfoJson);

        Seller? seller = null;
        if(postDto.SellerID != null) seller = _dal.GetById<Seller>(postDto.SellerID);

        Post post = new Post(postDto.Title, postDto.Description, postDto.Validated || Logged is Admin, seller ??= (Seller) Logged);

        List<Product> products = new();
        List<Offer> offers = new();

        //var dbProducts = _dal.GetAll<Product>();

        for (int i = 0; i < postDto.Offers.Count; i++)
        {
            OfferDTO offerDto = postDto.Offers[i];
            ProductDTO productDto = offerDto.Product;

            Product product = ProductFactory.GetFactory(postDto.Category).CreateProduct(postDto.ProductName, postDto.Certifications, postDto.EcologicPrint, postDto.MinimumAge, postDto.HowToUse, postDto.HowToReducePrint, productDto.Attributes);
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
    }

    public void EditPost(int postID, string postInfoJson)
    {
        DeletePost(postID);
        AddPost(postInfoJson);

        _dal.Commit();
    }

    public string GetPosts()
    { 
        List<Post> posts = new();
        List<PostDTO> postDtos = new();

        if(Logged is Admin) posts = _dal.GetWhere<Post>(x => !x.Validated).ToList();
        else if (Logged is Seller seller) posts = seller.Posts.Where(x => x.Validated).ToList();
        else posts = _dal.GetWhere<Post>(x => x.Validated).ToList();

        foreach (var post in posts)
        {
            postDtos.Add(new PostDTO(post));   
        }

        return JsonConvert.SerializeObject(postDtos);
    }

    public string GetPostsFuzzyContain(string searchFor)
    {
        List<Post> posts = _dal.GetAll<Post>().Where(x => Fuzz.PartialTokenSortRatio(searchFor,x.Title) > 60)
            .OrderByDescending(x => Fuzz.PartialTokenSortRatio(searchFor, x.Title)).ToList();

        List<PostDTO> postDtos = posts.Select(x => new PostDTO(x)).ToList();
        return JsonConvert.SerializeObject(postDtos);
    }

    public List<string> GetPostsNamesStartWith(string startWith, int numPosts)
    {
        var res = _dal.GetWhere<Post>(x => x.Title.StartsWith(startWith)).Select(y => new string(y.Title)).ToList();
        return res.Take(Math.Min(numPosts, res.Count)).ToList();
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

    public void AddPost(Post post)
    {
        _dal.Insert<Post>(post);
        _dal.Commit();
    }

    public void AddAlert(Alert alert)
    {
        _dal.Insert<Alert>(alert);
        _dal.Commit();
    }

    public void RegisterConsumer(string email, string password, string name, string lastNames, string dni, DateTime birthDate, Address billingAddress, Address address)
    {
        if (_dal.GetWhere<Consumer>(x => x.Email == email).Any() || _dal.GetWhere<Consumer>(x => x.DNI == dni).Any())
        {
            throw new Exception("Usuario existente");
        }
        else
        {
            _dal.Insert<Consumer>(new Consumer(email, password, name, lastNames, dni, birthDate, billingAddress, address));
            _dal.Commit();

        }
    }

    public void RegisterSeller(string email, string password, string name, string lastNames, string dni, string companyName, string iban)
    {
        if (_dal.GetWhere<Seller>(x => x.Email == email).Any() || _dal.GetWhere<Seller>(x => x.DNI == dni).Any() || _dal.GetWhere<Seller>(x => x.IBAN == iban).Any())
        {
            throw new Exception("Usuario existente");
        }
        else
        {
            _dal.Insert<Seller>(new Seller(email, password, name, lastNames, dni, companyName, iban));
            _dal.Commit();

        }
    }

    public void LogIn(string email, string password)
    {
        if (_dal.GetWhere<User>(x => x.Email == email).Any())
        {
            User user = user = _dal.GetById<User>(email);
            if (user != null)
            {
                if (user.Password == password)
                {
                    Logged = user;
                }
                else throw new Exception("Contraseña incorrecta");
            }
        }
        else throw new Exception("Usuario no registrado");
        //prueba

    }

    public void LogOut()
    {
        Logged = null;
    }

    public User getuser()
    {
        return Logged; 
    }
}