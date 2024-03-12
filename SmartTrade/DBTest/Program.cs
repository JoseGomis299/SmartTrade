using SmartTradeLib.BusinessLogic;
using SmartTradeLib.Entities;

ISmartTradeService service = new SmartTradeService();
service.RemoveAll();

Seller pepito = new Seller("ChiclesPepito@gmail.com", "123", "Pepito", "1", "1", "2", "3");
Product product = new Toy("Balancín", "", "", 1,  "a", "metal", "1");
Product product2 = new Toy("Balancín", "", "", 1,  "asda", "plastico", "1");
Post post = new Post("Balancines", "", true, pepito);
Offer offer = new Offer(post, product, 1, 2, 100);
Offer offer2 = new Offer(post, product2, 1, 2, 100);
post.Offers.Add(offer);
post.Offers.Add(offer2);
product.Posts.Add(post);
product2.Posts.Add(post);
pepito.AddPost(post);

service.AddSeller(pepito);

service.SaveChanges();

//Seller pepito = new Seller()
//{
//    CompanyName = "Chicles Pepito",
//    Email = "ChiclesPepito@gmail.com",
//    Password = "1234",
//    DNI = "12345678A",
//    IBAN = "ES1234567891234567891234",
//    LastNames = "Gonzalez Gutierrez",
//    Name = "Pepito"
//};

//service.AddSeller(pepito);

//Post post = new Post()
//{
//    Description = "Chicles de fresa muy buenos",
//    Title = "Chicles de fresa",
//    Validated = true,
//    Seller = pepito
//};

//Nutrition nutrition = new Nutrition()
//{
//    Validated = true,
//    Name = "chicle"
//    Allergens = "Ninguno",
//    Calories = "100",
//    Carbohydrates = "20",
//    Fats = "0",
//    Proteins = "0",
//    Weight = "10",
//    MinimumAge = 3,
//    Images = new List<byte[]>(){Array.Empty<byte>()}
//};

//Offer offer = new Offer()
//{
//    Price = 0.5f,
//    ShippingCost = 0.2f,
//    Stock = 100,
//    Product = nutrition,
//    Post = post
//};

//nutrition.Posts = new List<Post>();
//pepito.Posts = new List<Post>();
//post.Offers = new List<Offer>();

//nutrition.Posts.Add(post);
//post.Offers.Add(offer);

//pepito.AddPost(post);
//service.SaveChanges();



