using System.Text;
using SmartTrade.BusinessLogic;
using SmartTrade.Entities;
using SmartTradeDTOs;
using DateTime = System.DateTime;

var service = new SmartTradeService();
Datos data = new Datos();

//service.RemoveAll();

SellerRegisterData seller = AddSeller(11);

for (int i = 0; i < 10; i++)
{
    for (int j = 0; j < Random.Shared.Next(1, 5); j++) AddPost(seller);
}

seller = AddSeller(12);

for (int i = 0; i < 10; i++)
{
    for (int j = 0; j < Random.Shared.Next(1, 5); j++) AddPost(seller);
}

seller = AddSeller(13);

for (int i = 0; i < 10; i++)
{
    for (int j = 0; j < Random.Shared.Next(1, 5); j++) AddPost(seller);
}


void AddPost(SellerRegisterData seller)
{
    PostDTO post = new PostDTO();

    post.SellerID = seller.Email;
    post.SellerCompanyName = seller.CompanyName;
    post.ProductName = data.GetNombreDeProducto(data.GetSellerCategory(seller.CompanyName));
    post.Title = post.ProductName;
    post.Description = "Descripción de " + post.ProductName;
    post.Category = data.GetSellerCategory(seller.CompanyName);
    post.MinimumAge = Random.Shared.Next(1, 18);
    post.Certifications = "Certificaciones de " + post.ProductName;
    post.EcologicPrint = $"{Random.Shared.Next(0, 100)}";
    post.HowToUse = "Cómo usar " + post.ProductName;
    post.HowToReducePrint = "Cómo reducir la huella ecológica de " + post.ProductName;
    post.Validated = true;

    var offers = new List<OfferDTO>();
    for (int i = 0; i < Random.Shared.Next(1, 3); i++)
    {
       offers.Add(GetOffer(post.Category, offers));
    }

    post.Offers = offers;
    service.AddPost(post, seller.Email);
}

OfferDTO GetOffer(Category category, List<OfferDTO> alreadyInPost)
{
    OfferDTO offer = new OfferDTO()
    {
        Price = Random.Shared.Next(1, 100),
        ShippingCost = Random.Shared.Next(1, 10),
        Stock = Random.Shared.Next(1, 100),
        Product = GetProduct(category, alreadyInPost.Select(x => x.Product).ToList())
    };

    return offer;
}

ProductDTO GetProduct(Category category, List<ProductDTO> alreadyInOffer)
{
    ProductDTO product = new ProductDTO()
    {
        Attributes = new Dictionary<string, string>(),
        Images = new List<byte[]>()
    };

    do
    {
        product.Attributes.Clear();
        switch (category)
        {
            case Category.Clothing:
                product.Attributes.Add("Color", data.GetColor());
                product.Attributes.Add("Material", "Tela");
                product.Attributes.Add("Size", data.GetTalla());
                product.Attributes.Add("Brand", "Inditex");
                break;
            case Category.Nutrition:
                product.Attributes.Add("Weight", Random.Shared.Next(1000).ToString());
                product.Attributes.Add("Calories", "100");
                product.Attributes.Add("Proteins", "20");
                product.Attributes.Add("Carbohydrates", "5");
                product.Attributes.Add("Fats", "7");
                product.Attributes.Add("Allergens", "");
                break;
            case Category.Toy:
                product.Attributes.Add("Brand", "Mattel");
                product.Attributes.Add("Material", data.GetMaterialJuguete());
                break;
            case Category.Book:
                product.Attributes.Add("Author", "Juan");
                product.Attributes.Add("Publisher", "De Dios");
                product.Attributes.Add("Pages", "200");
                product.Attributes.Add("Language", data.GetIdioma());
                product.Attributes.Add("ISBN", "123456789");
                break;
        }
    }while(alreadyInOffer.Any(x => x.Attributes.SequenceEqual(product.Attributes)));

    for (int i = 0; i < Random.Shared.Next(1, 3); i++)
    {
        product.Images.Add(data.GetRandomImage(category));
    }

    return product;
}

void AddCostumer(int i)
{
    //Set Consumer Data
    string email = $"c{i}@gmail.com";
    string password = "123";
    string name = data.GetNombre();
    string lastnames = data.GetApellido();
    string dni = data.GetRandomDNI();
    DateTime dateBirth = new DateTime(Random.Shared.Next(1950, 2003), Random.Shared.Next(1, 13), Random.Shared.Next(1, 29));
    Address billingAddress = data.GetRandomAddress();
    Address consumerAddress = billingAddress;

    ConsumerRegisterData consumer = new ConsumerRegisterData()
    {
        Address = consumerAddress,
        BillingAddress = billingAddress,
        BirthDate = dateBirth,
        DNI = dni,
        Email = email,
        LastNames = lastnames,
        Name = name,
        Password = password,
    };

    service.RegisterConsumer(consumer);
}

SellerRegisterData AddSeller(int i)
{
    //Set Seller Data
    string email = $"s{i}@gmail.com";
    string password = "123";
    string name = data.GetNombre();
    string lastnames = data.GetApellido();
    string dni = data.GetRandomDNI();
    string companyName = data.GetNombreDeTienda((Category)Random.Shared.Next(4));
    string iban = "ES" + Random.Shared.Next(100000000, 999999999).ToString() + "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Random.Shared.Next(26)];

    SellerRegisterData seller = new SellerRegisterData()
    {
        CompanyName = companyName,
        DNI = dni,
        Email = email,
        IBAN = iban,
        LastNames = lastnames,
        Name = name,
        Password = password
    };

    service.RegisterSeller(seller);
    return seller;
}




