using SmartTrade.Entities;

public class Datos
{

    string[] nombresEspañoles = new[]
    {
        "Alejandro", "Andrés", "Antonio", "Carlos", "David", "Diego", "Eduardo", "Emilio",
        "Fernando", "Francisco", "Gabriel", "Gonzalo", "Guillermo", "Ignacio", "Javier",
        "Jesús", "Joaquín", "Jorge", "José", "Juan", "Luis", "Manuel", "Miguel", "Pablo",
        "Pedro", "Rafael", "Ramón", "Roberto", "Rubén", "Santiago", "Sergio", "Vicente"
    };

    string[] apellidosEspañoles = new[]
    {
        "García", "Fernández", "González", "Rodríguez", "López", "Martínez", "Sánchez",
        "Pérez", "Gómez", "Martín", "Jiménez", "Ruiz", "Hernández", "Díaz", "Moreno",
        "Álvarez", "Romero", "Alonso", "Gutiérrez", "Navarro", "Torres", "Domínguez",
        "Vázquez", "Ramos", "Gil", "Ramírez", "Serrano", "Blanco", "Molina", "Morales"
    };

    string[] nombresDeTiendasDeRopa = new[]
    {
        "C&A", "Zara", "Mango", "H&M", "Pull&Bear", "Bershka", "Stradivarius",
        "Springfield", "Massimo Dutti", "Desigual", "Cortefiel", "Sfera", "Merkal",
        "Kiabi", "Primark", "Lefties", "Oysho", "Inside", "Pimkie", "Cortefiel",
    };

    string[] nombresDeTiendasDeAlimentacion = new[]
    {
        "Mercadona", "Carrefour", "Lidl", "Aldi", "Dia", "Eroski", "Alcampo",
        "Consum", "Covirán", "Froiz", "Bonpreu", "Caprabo", "Condis", "El Corte Inglés",
        "Hipercor", "La Plaza de DIA", "Maxi Dia", "Simply", "Spar", "Supermercados MAS",
        "Supermercados Piedra", "Supermercados Sánchez Romero"
    };

    string[] nombresDeTiendasDeElectronica = new[]
    {
        "MediaMarkt", "El Corte Inglés", "Fnac", "Worten", "Carrefour", "PcComponentes",
        "Amazon", "AliExpress", "eBay", "Coolmod", "Game", "Phone House", "Vodafone",
        "Orange", "Movistar", "Jazztel", "Yoigo", "Simyo", "Pepephone", "Lowi",
        "MásMóvil", "Tuenti", "Digi", "Llamaya", "Amena", "Virgin Telco", "Lycamobile",
        "Lebara", "Aire Networks", "Telecable", "R", "Euskaltel"
    };

    string[] nombresDeTiendasDeJuguetes = new[]
    {
        "Toys R Us", "Juguettos", "Imaginarium", "Eurekakids", "Poly", "Drims",
        "Juguetilandia", "Juguetoon", "Juguetes Don Dino", "Juguetes Cayro",
        "Juguetes Trenexpreso", "Juguetes El Cid", "Juguetes Artesanos",
        "Juguetes La Casita", "Juguetes La Cebra", "Juguetes La Rana",
        "Juguetes La Ardilla", "Juguetes La Hormiga", "Juguetes La Mariposa",
        "Juguetes La Tortuga", "Juguetes La Oveja", "Juguetes La Cigüeña",
        "Juguetes La Paloma", "Juguetes La Gaviota", "Juguetes La Ballena",
        "Juguetes La Estrella", "Juguetes La Luna", "Juguetes La Nube",
        "Juguetes La Montaña", "Juguetes La Selva", "Juguetes La Jungla",
        "Juguetes La Sabana", "Juguetes La Cueva", "Juguetes La Montaña",
        "Juguetes La Playa", "Juguetes La Ciudad", "Juguetes La Granja",
        "Juguetes La Montaña", "Juguetes La Isla"
    };

    string[] nombresDeTiendasDeLibros = new[]
    {
        "Casa del Libro", "Fnac", "El Corte Inglés", "Amazon", "AliExpress", "eBay",
        "AbeBooks", "IberLibro", "Todocolección", "Uniliber", "Libros Antiguos",
        "Libros de Segunda Mano", "Libros de Ocasión", "Libros Baratos", "Libros Usados",
    };

    string[] nombresDeCiudades = new[]
    {
        "Madrid", "Barcelona", "Valencia", "Sevilla", "Zaragoza", "Málaga", "Murcia",
        "Palma", "Las Palmas de Gran Canaria", "Bilbao", "Alicante", "Córdoba",
        "Valladolid", "Vigo", "Gijón", "Hospitalet de Llobregat", "Vitoria", "La Coruña",
        "Granada", "Elche", "Oviedo", "Badalona", "Cartagena", "Terrassa", "Jerez",
        "Sabadell", "Alcalá de Henares", "Pamplona", "Fuenlabrada", "San Sebastián",
        "Getafe", "Albacete", "Leganés", "Santander", "Castellón", "Burgos", "Almería",
        "Salamanca", "Alcorcón", "Logroño", "San Cristóbal de la Laguna", "Badajoz",
        "Huelva", "Marbella", "León", "Tarragona", "Dos Hermanas", "Torrejón de Ardoz",
        "Parla", "Mataró", "Algeciras", "Alcobendas", "Cádiz", "Jaén", "Ourense",
        "Reus", "Telde", "Barakaldo", "Lugo", "Girona", "Santiago de Compostela",
        "Cáceres", "Lorca", "Cornellá de Llobregat", "Avilés", "Palencia", "Toledo",
        "Segovia", "Ceuta", "Melilla"
    };

    string[] nombresDeCalles = new[]
    {
        "Calle de la Piruleta", "Calle de la Gominola", "Calle de la Chuchería",
        "Calle de la Galleta", "Calle del Caramelo", "Calle del Chocolate",
        "Calle de la Nube", "Calle del Cielo", "Calle de la Estrella",
        "Calle de la Luna", "Calle del Sol", "Calle de la Tierra",
        "Calle de la Montaña", "Calle del Mar", "Calle del Río",
        "Calle de la Playa", "Calle de la Selva", "Calle de la Jungla",
        "Calle de la Sabana", "Calle de la Cueva", "Calle de la Montaña",
        "Calle de la Isla", "Calle de la Ciudad", "Calle de la Granja"
    };

    string[] nombresDeProvincias = new[]
    {
        "Álava", "Albacete", "Alicante", "Almería", "Asturias", "Ávila", "Badajoz",
        "Barcelona", "Burgos", "Cáceres", "Cádiz", "Cantabria", "Castellón", "Ceuta",
        "Ciudad Real", "Córdoba", "Cuenca", "Gerona", "Granada", "Guadalajara", "Guipúzcoa",
        "Huelva", "Huesca", "Islas Baleares", "Jaén", "La Coruña", "La Rioja", "Las Palmas",
        "León", "Lérida", "Lugo", "Madrid", "Málaga", "Melilla", "Murcia", "Navarra",
        "Orense", "Palencia", "Pontevedra", "Salamanca", "Segovia", "Sevilla", "Soria",
        "Tarragona", "Tenerife", "Teruel", "Toledo", "Valencia", "Valladolid", "Vizcaya",
        "Zamora", "Zaragoza"
    };

    string[] nombresDeJuguetes = new[]
    {
        "Pelota", "Muñeca", "Coche", "Avión", "Barco", "Tren", "Puzzle", "Rompecabezas",
        "Cubo de Rubik", "Tetris", "Ajedrez", "Damas", "Dominó", "Jenga", "Twister",
        "Scalextric", "Playmobil", "Lego", "Tente", "Nenuco", "Nancy", "Furby", "Tamagotchi",
        "Polly Pocket"
    };

    string[] nombresDeAlimentos = new[]
    {
        "Manzana", "Plátano", "Naranja", "Pera", "Melocotón", "Cereza", "Fresa", "Uva",
        "Sandía", "Melón", "Piña", "Kiwi", "Mango", "Papaya", "Aguacate", "Tomate",
        "Chicle", "Caramelo", "Chocolate", "Galleta", "Gominola", "Piruleta", "Chuchería",
        "Filete de ternera", "Filete de cerdo", "Filete de pollo", "Filete de pavo",
        "Remolacha", "Zanahoria", "Calabacín", "Berenjena", "Pimiento", "Pepino", "Lechuga",
        "Espinacas", "Acelgas", "Coliflor", "Brócoli", "Patata", "Boniatos", "Calabaza"
    };

    string[] nombresDeLibros = new[]
    {
        "El Quijote", "Don Quijote", "La Celestina", "El Lazarillo de Tormes",
        "La vida es sueño", "Fuenteovejuna", "El burlador de Sevilla", "El Buscón",
        "El lazarillo de Tormes", "El conde Lucanor", "El coloquio de los perros",
        "El licenciado Vidriera", "La gitanilla", "Rinconete y Cortadillo", "La fuerza de la sangre",
        "La ilustre fregona", "Novelas ejemplares", "El casamiento engañoso", "El celoso extremeño",
    };

    string[] nombresDeRopa = new[]
    {
        "Camiseta", "Pantalón", "Falda", "Vestido", "Chaqueta", "Jersey", "Sudadera",
        "Pijama", "Bañador", "Bikini", "Calcetines", "Zapatos", "Botas", "Zapatillas",
        "Sandalias", "Chanclas", "Gorra", "Sombrero", "Bufanda", "Guantes", "Gorro",
        "Pantalones cortos", "Pantalones largos", "Pantalones vaqueros", "Pantalones de chándal",
        "Pantalones de vestir", "Pantalones de cuero", "Pantalones de tela", "Pantalones de pana"
    };

    string[] tallasDeRopa = new[]
    {
        "XS", "S", "M", "L", "XL", "XXL"
    };

    private string[] coloresDeRopa = new[]
    {
        "Rojo", "Verde", "Azul", "Amarillo", "Naranja", "Rosa", "Morado", "Marrón",
        "Beix", "Negro", "Blanco", "Gris"
    };

    private string[] materialesDeJuguetes = new[]
    {
        "Plástico", "Madera", "Metal", "Vidrio"
    };

    private string[] idiomas = new[]
    {
        "Español", "Inglés", "Alemán", "Chino", "Japonés"
    };

    public Address GetRandomAddress()
    {
        string city = nombresDeCiudades[Random.Shared.Next(nombresDeCiudades.Length)];
        string postalCode = Random.Shared.Next(10000, 99999).ToString();
        string street = nombresDeCalles[Random.Shared.Next(nombresDeCalles.Length)];
        string number = Random.Shared.Next(1, 100).ToString();
        string door = Random.Shared.Next(1, 10).ToString();
        string province = nombresDeProvincias[Random.Shared.Next(nombresDeProvincias.Length)];

        return new Address()
        {
            City = city,
            PostalCode = postalCode,
            Street = street,
            Number = number,
            Door = door,
            Province = province
        };
    }

    public string GetNombre()
    {
        return nombresEspañoles[Random.Shared.Next(nombresEspañoles.Length)];
    }

    public string GetApellido()
    {
        return apellidosEspañoles[Random.Shared.Next(apellidosEspañoles.Length)] + " " + apellidosEspañoles[Random.Shared.Next(apellidosEspañoles.Length)];
    }

    public string GetNombreDeTienda(Category category)
    {
        switch (category)
        {
            case Category.Toy:
                return nombresDeTiendasDeJuguetes[Random.Shared.Next(nombresDeTiendasDeJuguetes.Length)];
            case Category.Nutrition:
                return nombresDeTiendasDeAlimentacion[Random.Shared.Next(nombresDeTiendasDeAlimentacion.Length)];
            case Category.Book:
                return nombresDeTiendasDeLibros[Random.Shared.Next(nombresDeTiendasDeLibros.Length)];
            default:
                return nombresDeTiendasDeRopa[Random.Shared.Next(nombresDeTiendasDeRopa.Length)];
        }
    }

    public string GetNombreDeProducto(Category category)
    {
        switch (category)
        {
            case Category.Toy:
                return nombresDeJuguetes[Random.Shared.Next(nombresDeJuguetes.Length)];
            case Category.Nutrition:
                return nombresDeAlimentos[Random.Shared.Next(nombresDeAlimentos.Length)];
            case Category.Book:
                return nombresDeLibros[Random.Shared.Next(nombresDeLibros.Length)];
            default:
                return nombresDeRopa[Random.Shared.Next(nombresDeRopa.Length)];
        }
    }

    public string GetRandomDNI()
    {
        return Random.Shared.Next(10000000, 99999999).ToString() + "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Random.Shared.Next(26)];
    }

    public Category GetSellerCategory(string companyName)
    {
        if (nombresDeTiendasDeJuguetes.Contains(companyName))
        {
            return Category.Toy;
        }
        else if (nombresDeTiendasDeAlimentacion.Contains(companyName))
        {
            return Category.Nutrition;
        }
        else if (nombresDeTiendasDeLibros.Contains(companyName))
        {
            return Category.Book;
        }
        else
        {
            return Category.Clothing;
        }
    }

    public string GetTalla()
    {
        return tallasDeRopa[Random.Shared.Next(tallasDeRopa.Length)]; 
    }

    public string GetColor()
    {
        return coloresDeRopa[Random.Shared.Next(coloresDeRopa.Length)];
    }

    public string GetMaterialJuguete()
    {
        return materialesDeJuguetes[Random.Shared.Next(materialesDeJuguetes.Length)];
    }

    public string GetIdioma()
    {
        return idiomas[Random.Shared.Next(idiomas.Length)];
    }


    public byte[] GetRandomImage(Category category)
    {
        switch (category)
        {
            case Category.Toy:
                return GetRandomImageByteArray("C:\\Users\\Jose Gomis\\Documents\\GitHub\\SmartTrade\\SmartTrade\\DBTest\\Imágenes\\Juguetes\\");
            case Category.Nutrition:
                return GetRandomImageByteArray("C:\\Users\\Jose Gomis\\Documents\\GitHub\\SmartTrade\\SmartTrade\\DBTest\\Imágenes\\Comida\\");
            case Category.Book:
                return GetRandomImageByteArray("C:\\Users\\Jose Gomis\\Documents\\GitHub\\SmartTrade\\SmartTrade\\DBTest\\Imágenes\\Libros\\");
            default:
                return GetRandomImageByteArray("C:\\Users\\Jose Gomis\\Documents\\GitHub\\SmartTrade\\SmartTrade\\DBTest\\Imágenes\\Ropa\\");
        }
    }

    private byte[] GetRandomImageByteArray(string imageFolderPath)
    {
        // Obtener la lista de archivos de imagen en la carpeta
        string[] imageFiles = Directory.GetFiles(imageFolderPath, "*.*", SearchOption.AllDirectories).ToArray();

        // Elegir un archivo de imagen al azar
        Random random = new Random();
        string randomImageFile = imageFiles[random.Next(imageFiles.Length)];

        // Leer el archivo de imagen como bytes
        byte[] imageBytes = File.ReadAllBytes(randomImageFile);

        return imageBytes;
    }
}