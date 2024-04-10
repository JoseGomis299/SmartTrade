using FuzzySharp;
using SmartTradeDTOs;
using Newtonsoft.Json;
using SmartTrade.Entities;
using SmartTrade.Persistence;

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

    public void AddPost(PostDTO postInfo, string loggedID)
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

            Product product = ProductFactory.GetFactory(postInfo.Category).CreateProduct(postInfo.ProductName, postInfo.Certifications, postInfo.EcologicPrint, postInfo.MinimumAge, postInfo.HowToUse, postInfo.HowToReducePrint, productDto.Attributes);
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
    }

    public void EditPost(int postID, PostDTO postInfo, string loggedID)
    {
        DeletePost(postID);
        AddPost(postInfo, loggedID);

        _dal.Commit();
    }

    public List<PostDTO> GetPosts(string? loggedID)
    { 
        User? logged = _dal.GetById<User>(loggedID);
        List<Post> posts = new();
        List<PostDTO> postDtos = new();

        if(logged is Admin) posts = _dal.GetWhere<Post>(x => !x.Validated).ToList();
        else if (logged is Seller seller) posts = seller.Posts.Where(x => x.Validated).ToList();
        else posts = _dal.GetWhere<Post>(x => x.Validated).ToList();

        foreach (var post in posts)
        {
            postDtos.Add(new PostDTO(post));   
        }

        return postDtos;
    }

    public PostDTO GetPost(int postId)
    {
        return new PostDTO(_dal.GetById<Post>(postId));
    }

    public List<PostDTO> GetPostsFuzzyContain(string searchFor)
    {
        List<Post> posts = _dal.GetAll<Post>().Where(x => Fuzz.PartialTokenSortRatio(searchFor,x.Title) > 60)
            .OrderByDescending(x => Fuzz.PartialTokenSortRatio(searchFor, x.Title)).ToList();

        return posts.Select(x => new PostDTO(x)).ToList();
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

    public ConsumerDTO RegisterConsumer(ConsumerRegisterData registerData)
    {
        if (_dal.GetWhere<Consumer>(x => x.Email == registerData.Email).Any() || _dal.GetWhere<Consumer>(x => x.DNI == registerData.DNI).Any())
        {
            throw new Exception("Usuario existente");
        }

        Consumer consumer = new Consumer(registerData.Email, registerData.Password, registerData.Password, registerData.Password, registerData.DNI, registerData.BirthDate, registerData.BillingAddress, registerData.Address);
        _dal.Insert<Consumer>(consumer);
        _dal.Commit();

        return new ConsumerDTO(consumer);
    }

    public SellerDTO RegisterSeller(SellerRegisterData registerData)
    {
        if (_dal.GetWhere<Seller>(x => x.Email == registerData.Email).Any() || _dal.GetWhere<Seller>(x => x.DNI == registerData.DNI).Any())
        {
            throw new Exception("Usuario existente");
        }

        Seller seller = new Seller(registerData.Email, registerData.Password, registerData.Name, registerData.LastNames, registerData.DNI, registerData.CompanyName, registerData.IBAN);
        _dal.Insert<Seller>(seller);
        _dal.Commit();

        return new SellerDTO(seller);
    }

    public UserDTO LogIn(string email, string password)
    {
        if (!_dal.GetWhere<User>(x => x.Email == email).Any()) throw new Exception("Usuario no registrado");
            
        User user = _dal.GetById<User>(email);
        if (user.Password != password) throw new Exception("Contraseña incorrecta");

        return new UserDTO(user);
    }
}