using Newtonsoft.Json;
using SmartTrade.BusinessLogic;
using SmartTrade.Entities;
using SmartTrade.Persistence;
using SmartTradeDTOs;
using ArgumentNullException = System.ArgumentNullException;
using Console = System.Console;
using DateTime = System.DateTime;
using Exception = System.Exception;
using HttpClient = System.Net.Http.HttpClient;
using HttpContent = System.Net.Http.HttpContent;
using HttpResponseMessage = System.Net.Http.HttpResponseMessage;
using NotImplementedException = System.NotImplementedException;
using Task = System.Threading.Tasks.Task;
using Uri = System.Uri;

//ISmartTradeService service = new SmartTradeService();
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
//service.EditPost("Juguete", "buenos Juguetes", "Juguete", Category.Toy, 3, "", "", new List<int>() { 100 }, new List<float>() { 5 }, new List<float>() { 1 }, new List<List<byte[]>>() { new() { posts.Offers.First().Product.Images.First().ImageSource, imageData } }, new List<List<string>>() { attributes }, posts);

Console.WriteLine(await SmartTradeService.Instance.GetPostsNames());
//AddPost();
//AddPosts(20);

//void AddPosts(int n)
//{
//    string[] imagePaths = new[]
//    {
//        "C:\\Users\\Jose Gomis\\Documents\\GitHub\\SmartTrade\\SmartTrade\\SmartTrade\\Assets\\Arrow.png",
//        "C:\\Users\\Jose Gomis\\Documents\\GitHub\\SmartTrade\\SmartTrade\\SmartTrade\\Assets\\Cart.png",
//        "C:\\Users\\Jose Gomis\\Documents\\GitHub\\SmartTrade\\SmartTrade\\SmartTrade\\Assets\\HalfStar.png",
//        "C:\\Users\\Jose Gomis\\Documents\\GitHub\\SmartTrade\\SmartTrade\\SmartTrade\\Assets\\Star.png",
//        "C:\\Users\\Jose Gomis\\Documents\\GitHub\\SmartTrade\\SmartTrade\\SmartTrade\\Assets\\Home.png",
//        "C:\\Users\\Jose Gomis\\Documents\\GitHub\\SmartTrade\\SmartTrade\\SmartTrade\\Assets\\User.png",
//        "C:\\Users\\Jose Gomis\\Documents\\GitHub\\SmartTrade\\SmartTrade\\SmartTrade\\Assets\\VoidStar.png",
//    };

//    service.LogIn("ChiclesPepito@gmail.com", "123");

//    for (int i = 0; i < n; i++)
//    {
//        List<string> attributes = new List<string>() { "100", ""+i };

//        byte[] imageData = File.ReadAllBytes(imagePaths[Random.Shared.Next(0, imagePaths.Length)]);
//     //   service.AddPost("Juguete" + i, "buenos Juguetes", "Juguete", Category.Toy, 3, "","", "", "", true, new List<int>() { 100 }, new List<float>() { i }, new List<float>() { 1 }, new List<List<byte[]>>() { new() { imageData } }, new List<List<string>>() { attributes });
//    }
//}

//void AddPost()
//{
//    service.RemoveAll();

//    Seller pepito = new Seller("ChiclesPepito@gmail.com", "123", "Pepito", "1", "1", "2", "3");
//    service.AddSeller(pepito);

//    byte[] imageData = File.ReadAllBytes(@"C:\Users\Jose Gomis\Documents\GitHub\SmartTrade\SmartTrade\SmartTrade\Assets\Arrow.png");

//    List<string> attributes = new List<string>() { "100", "1" };

//    service.LogIn("ChiclesPepito@gmail.com", "123");
//  //  service.AddPost("Juguete", "buenos Juguetes", "Juguete", Category.Toy, 3, "","", "", "", true,new List<int>() { 100 }, new List<float>() { 5 }, new List<float>() { 1 }, new List<List<byte[]>>() { new() { imageData } }, new List<List<string>>() { attributes });
//}

void ValidatePost()
{

    byte[] imageData = File.ReadAllBytes(@"C:\Users\Jose Gomis\Documents\GitHub\SmartTrade\SmartTrade\SmartTrade\Assets\Arrow.png");

    List<string> attributes = new List<string>() { "100", "1" };
    List<string> attributes2 = new List<string>() { "100", "1" };

    var posts = new EntityFrameworkDAL(new SmartTradeContext()).GetAll<Post>().First();
  //  service.EditPost("Juguete", "buenos Juguetes", "Juguete", Category.Toy, 3, "","", "", "", new List<int>() { 100 }, new List<float>() { 5 }, new List<float>() { 1 }, new List<List<byte[]>>() { new() { posts.Offers.First().Product.Images.First().ImageSource, imageData } }, new List<List<string>>() { attributes }, posts);

}

async Task ConnectToAPI()
{
    using var client = new HttpClient();

    // Configura la base URL de la API
    client.BaseAddress = new Uri("https://localhost:7185/");

    // Realiza una solicitud GET a la ruta "SmartTradeAPI/Post"
    try
    {
        var response = await client.GetAsync("SmartTradeAPI/Post");

        if (response.IsSuccessStatusCode)
        {
            // Lee la respuesta como una cadena
            var content = await response.Content.ReadAsStringAsync();
            List<Producto> listaDeProductos = JsonConvert.DeserializeObject<List<Producto>>(content);
          
            foreach (var producto in listaDeProductos)
            {
                Console.WriteLine($"ID: {producto.Id}");
                Console.WriteLine($"Título: {producto.Title}");
                Console.WriteLine($"Descripción: {producto.Description}");
                // ... otras propiedades
            }
        }
        else
        {
            Console.WriteLine($"Error al conectar a la API. Código de estado: {response.StatusCode}");
        }
    }
    catch (Exception e)
    {
        Console.WriteLine($"Error al conectar a la API: {e.Message}");
    }
}

public class Producto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool Validated { get; set; }
    public string SellerID { get; set; }
    public string SellerCompanyName { get; set; }
    public List<int> OffersIDs { get; set; }
    public decimal Price { get; set; }
}

public class SmartTradeService
{
    private static SmartTradeService? _instance;
    public static SmartTradeService Instance => _instance ??= new SmartTradeService();

    public UserDTO? Logged { get; private set; }

    public UserDTO LogIn(string email, string password)
    {
        throw new NotImplementedException();
    }

    public ConsumerDTO RegisterConsumer(string email, string password, string name, string lastnames, string dni, DateTime dateBirth, Address billingAddress, Address consumerAddress)
    {
        throw new NotImplementedException();
    }

    public async Task AddPostAsync(string serializeObject, string loggedEmail)
    {
        throw new NotImplementedException();
    }

    public async System.Threading.Tasks.Task<string> GetPosts(string loggedEmail)
    {
        return await PerformApiInstructionAsync("Post/GetAll", ApiInstruction.Get);
    }

    public async System.Threading.Tasks.Task<string> GetPostsNames()
    {
        return await PerformApiInstructionAsync("Post/GetAllNames", ApiInstruction.Get);
    }

    public async System.Threading.Tasks.Task<string> GetPostsFuzzyContainAsync(string? searchText)
    {
        return await PerformApiInstructionAsync("Post/GetContaining?content=" + Uri.EscapeDataString(searchText), ApiInstruction.Get);
    }

    public async Task EditPostAsync(int postId, string postInfoJson, string loggedEmail)
    {
        throw new NotImplementedException();
    }

    public async Task DeletePostAsync(int postId)
    {
        throw new NotImplementedException();
    }

    private async System.Threading.Tasks.Task<string> PerformApiInstructionAsync(string function, ApiInstruction instruction, HttpContent content = null)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://smarttradeapi00.azurewebsites.net/");
        client.DefaultRequestHeaders.Add("Logged", Logged?.Email);

        try
        {
            HttpResponseMessage response = null;

            switch (instruction)
            {
                case ApiInstruction.Get:
                    response = await client.GetAsync("SmartTradeAPI/" + function);
                    break;
                case ApiInstruction.Post:
                    if (content == null) throw new ArgumentNullException(nameof(content));
                    response = await client.PostAsync("SmartTradeAPI/" + function, content);
                    break;
                case ApiInstruction.Put:
                    if (content == null) throw new ArgumentNullException(nameof(content));
                    response = await client.PutAsync("SmartTradeAPI/" + function, content);
                    break;
                case ApiInstruction.Delete:
                    response = await client.DeleteAsync("SmartTradeAPI/" + function);
                    break;
            }

            if (response == null || !response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al conectar a la API. Código de estado: {response?.StatusCode}");
            }

            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al conectar a la API: {e.Message}");
            return string.Empty;
        }
    }
}

public enum ApiInstruction
{
    Get,
    Post,
    Put,
    Delete
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



