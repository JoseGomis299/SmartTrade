using SmartTradeLib.Entities;
using SmartTradeLib.Persistence;
using System;
using System.Collections.Concurrent;
using SmartTradeAPI.Library.Persistence.NewFolder;

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

    public Post AddPost(string? title, string? description, string? productName, Category category, int minimumAge,
        string howToUse,
        string? certifications, string? ecologicPrint, string? howToReducePrint, bool validated, List<int> stocks,
        List<float> prices, List<float> shippingCosts, List<List<byte[]>> images, List<List<string>> attributes,
        Seller? seller = null)
    {
        //LogIn("ChiclesPepito@gmail.com", "123");
        
        Post post = new Post(title, description, validated, seller ??= (Seller) Logged);


        List<Product> products = new();
        List<Offer> offers = new();

        //var dbProducts = _dal.GetAll<Product>();

        for (int i = 0; i < stocks.Count; i++)
        {
            Product product = ProductFactory.GetFactory(category).CreateProduct(productName, certifications, ecologicPrint, minimumAge, howToUse, howToReducePrint, attributes[i]);
            foreach (var image in images[i])
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

            offers.Add(new Offer(products[i], prices[i], shippingCosts[i], stocks[i]));
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

    public void ValidatePost(string? title, string? description, string? productName, Category category, int minimumAge,
        string howToUse, string? certifications, string? ecologicPrint, string? howToReducePrint, List<int> stocks,
        List<float> prices, List<float> shippingCosts, List<List<byte[]>> images, List<List<string>> attributes,
        Post post)
    {
        Seller seller = _dal.GetById<Seller>(post.Seller.Email);
        RemovePost(post);

        var newPost = AddPost(title, description, productName, category, minimumAge, howToUse,certifications, ecologicPrint, howToReducePrint, true, stocks, prices, shippingCosts, images, attributes, seller);


      //  RemovePost(post);
        //        ((Admin)Logged).ValidatePost(post);
        _dal.Commit();
    }

    public List<PostDTO> GetPosts()
    {
        List<Post> posts;

        if(Logged is Seller seller) posts = seller.Posts.ToList();
        else posts = _dal.GetWhere<Post>(x => x.Validated).ToList();

        return posts.Select(post => new PostDTO(post)).ToList();
    }

    public void RejectPost(Post post)
    {
        RemovePost(post);

        _dal.Commit();
    }

    private void RemovePost(Post post)
    {
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
                else throw new Exception("Contraseña incorrecta.");
            }
        }
        else throw new Exception("No está registrado.");
        //prueba

    }

    public void LogOut()
    {
        Logged = null;
    }
}