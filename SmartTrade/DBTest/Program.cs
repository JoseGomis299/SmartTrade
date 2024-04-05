using SmartTradeLib.BusinessLogic;
using SmartTradeLib.Entities;
using SmartTradeLib.Persistence;

ISmartTradeService service = new SmartTradeService();
//service.RemoveAll();

//Seller pepito = new Seller("ChiclesPepito@gmail.com", "123", "Pepito", "1", "1", "2", "3");
//service.AddSeller(pepito);
//////Product product = new Toy("Balancín", "", "", 1, "a", "metal");
//////Product product2 = new Toy("Balancín", "", "", 1, "asda", "plastico");
//////Post post = new Post("Balancines", "", true, pepito);
//////Offer offer = new Offer(product, 1, 2, 100);
//////Offer offer2 = new Offer(product2, 1, 2, 100);
//////offer.Post = post;
//////offer2.Post = post;
//////post.Offers.Add(offer);
//////post.Offers.Add(offer2);
//////product.Posts.Add(post);
//////product2.Posts.Add(post);
//////pepito.AddPost(post);

//////Set file equal to the image stored in C:\Users\Jose Gomis\Documents\GitHub\SmartTrade\SmartTrade\SmartTrade\Assets\Arrow.png

//byte[] imageData = File.ReadAllBytes(@"C:\Users\Jose Gomis\Documents\GitHub\SmartTrade\SmartTrade\SmartTrade\Assets\Arrow.png");

//List<string> attributes = new List<string>() { "100", "1" };
//List<string> attributes2 = new List<string>() { "100", "1" };

////service.LogIn("ChiclesPepito@gmail.com", "123");
////service.AddPost("Juguete", "buenos Juguetes", "Juguete", Category.Toy, 3, "", "", new List<int>() { 100 }, new List<float>() { 5 }, new List<float>() { 1 }, new List<List<byte[]>>() { new() { imageData } }, new List<List<string>>() { attributes });
////service.SaveChanges();

//var posts = new EntityFrameworkDAL(new SmartTradeContext()).GetAll<Post>().First();
//service.ValidatePost("Juguete", "buenos Juguetes", "Juguete", Category.Toy, 3, "", "", new List<int>() { 100 }, new List<float>() { 5 }, new List<float>() { 1 }, new List<List<byte[]>>() { new() { posts.Offers.First().Product.Images.First().ImageSource, imageData } }, new List<List<string>>() { attributes }, posts);

AddPost();
AddPosts(20);

void AddPosts(int n)
{
    string[] imagePaths = new[]
    {
        "C:\\Users\\Jose Gomis\\Documents\\GitHub\\SmartTrade\\SmartTrade\\SmartTrade\\Assets\\Arrow.png",
        "C:\\Users\\Jose Gomis\\Documents\\GitHub\\SmartTrade\\SmartTrade\\SmartTrade\\Assets\\Cart.png",
        "C:\\Users\\Jose Gomis\\Documents\\GitHub\\SmartTrade\\SmartTrade\\SmartTrade\\Assets\\HalfStar.png",
        "C:\\Users\\Jose Gomis\\Documents\\GitHub\\SmartTrade\\SmartTrade\\SmartTrade\\Assets\\Star.png",
        "C:\\Users\\Jose Gomis\\Documents\\GitHub\\SmartTrade\\SmartTrade\\SmartTrade\\Assets\\Home.png",
        "C:\\Users\\Jose Gomis\\Documents\\GitHub\\SmartTrade\\SmartTrade\\SmartTrade\\Assets\\User.png",
        "C:\\Users\\Jose Gomis\\Documents\\GitHub\\SmartTrade\\SmartTrade\\SmartTrade\\Assets\\VoidStar.png",
    };

    service.LogIn("ChiclesPepito@gmail.com", "123");

    for (int i = 0; i < n; i++)
    {
        List<string> attributes = new List<string>() { "100", ""+i };

        byte[] imageData = File.ReadAllBytes(imagePaths[Random.Shared.Next(0, imagePaths.Length)]);
        service.AddPost("Juguete" + i, "buenos Juguetes", "Juguete", Category.Toy, 3, "","", "", "", true, new List<int>() { 100 }, new List<float>() { i }, new List<float>() { 1 }, new List<List<byte[]>>() { new() { imageData } }, new List<List<string>>() { attributes });
    }
}

void AddPost()
{
    service.RemoveAll();

    Seller pepito = new Seller("ChiclesPepito@gmail.com", "123", "Pepito", "1", "1", "2", "3");
    service.AddSeller(pepito);

    byte[] imageData = File.ReadAllBytes(@"C:\Users\Jose Gomis\Documents\GitHub\SmartTrade\SmartTrade\SmartTrade\Assets\Arrow.png");

    List<string> attributes = new List<string>() { "100", "1" };

    service.LogIn("ChiclesPepito@gmail.com", "123");
    service.AddPost("Juguete", "buenos Juguetes", "Juguete", Category.Toy, 3, "","", "", "", true,new List<int>() { 100 }, new List<float>() { 5 }, new List<float>() { 1 }, new List<List<byte[]>>() { new() { imageData } }, new List<List<string>>() { attributes });
}

void ValidatePost()
{

    byte[] imageData = File.ReadAllBytes(@"C:\Users\Jose Gomis\Documents\GitHub\SmartTrade\SmartTrade\SmartTrade\Assets\Arrow.png");

    List<string> attributes = new List<string>() { "100", "1" };
    List<string> attributes2 = new List<string>() { "100", "1" };

    var posts = new EntityFrameworkDAL(new SmartTradeContext()).GetAll<Post>().First();
    service.ValidatePost("Juguete", "buenos Juguetes", "Juguete", Category.Toy, 3, "","", "", "", new List<int>() { 100 }, new List<float>() { 5 }, new List<float>() { 1 }, new List<List<byte[]>>() { new() { posts.Offers.First().Product.Images.First().ImageSource, imageData } }, new List<List<string>>() { attributes }, posts);

}

//foreach (var post in posts)
//{
//    foreach (var offer in post.Offers)
//    {
//        Console.WriteLine(offer.Product.Name);
//    }
//}

////Seller pepito = new Seller()
////{
////    CompanyName = "Chicles Pepito",
////    Email = "ChiclesPepito@gmail.com",
////    Password = "1234",
////    DNI = "12345678A",
////    IBAN = "ES1234567891234567891234",
////    LastNames = "Gonzalez Gutierrez",
////    Name = "Pepito"
////};

////service.AddSeller(pepito);

////Post post = new Post()
////{
////    Description = "Chicles de fresa muy buenos",
////    Title = "Chicles de fresa",
////    Validated = true,
////    Seller = pepito
////};

////Nutrition nutrition = new Nutrition()
////{
////    Validated = true,
////    Name = "chicle"
////    Allergens = "Ninguno",
////    Calories = "100",
////    Carbohydrates = "20",
////    Fats = "0",
////    Proteins = "0",
////    Weight = "10",
////    MinimumAge = 3,
////    Image = new List<byte[]>(){Array.Empty<byte>()}
////};

////Offer offer = new Offer()
////{
////    Price = 0.5f,
////    ShippingCost = 0.2f,
////    Stock = 100,
////    Product = nutrition,
////    Post = post
////};

////nutrition.Posts = new List<Post>();
////pepito.Posts = new List<Post>();
////post.Offers = new List<Offer>();

////nutrition.Posts.Add(post);
////post.Offers.Add(offer);

////pepito.AddPost(post);
////service.SaveChanges();



