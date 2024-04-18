using System;

namespace SmartTrade.Entities
{
    public abstract class ProductFactory
    {
        public static ProductFactory GetFactory(Category category)
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

        protected T CreateBaseProduct<T>(string name, string certification, string ecologicPrint, int minimumAge, string howToUse, string howToReducePrint) where T : Product, new()
        {
            T product = new T();
            product.Name = name;
            product.Certification = certification;
            product.EcologicPrint = ecologicPrint;
            product.MinimumAge = minimumAge;
            product.HowToUse = howToUse;
            product.HowToReducePrint = howToReducePrint;

            return product;
        }

        public abstract Product CreateProduct(string name, string certification, string ecologicPrint, int minimumAge, string howToUse, string howToReducePrint, Dictionary<string, string> attributes);
    }

    public class ToyFactory : ProductFactory
    {
        public override Product CreateProduct(string name, string certification, string ecologicPrint, int minimumAge, string howToUse, string howToReducePrint, Dictionary<string, string> attributes)
        {
            Toy toy = CreateBaseProduct<Toy>(name, certification, ecologicPrint, minimumAge, howToUse, howToReducePrint);

            toy.Brand = attributes[nameof(toy.Brand)];
            toy.Material = attributes[nameof(toy.Material)];

            return toy;
        }
    }

    public class NutritionFactory : ProductFactory
    {
        public override Product CreateProduct(string name, string certification, string ecologicPrint, int minimumAge, string howToUse, string howToReducePrint, Dictionary<string, string> attributes)
        {
            Nutrition nutrition = CreateBaseProduct<Nutrition>(name, certification, ecologicPrint, minimumAge, howToUse, howToReducePrint);
            
            nutrition.Weight = attributes[nameof(nutrition.Weight)];
            nutrition.Calories = attributes[nameof(nutrition.Calories)];
            nutrition.Proteins = attributes[nameof(nutrition.Proteins)];
            nutrition.Carbohydrates = attributes[nameof(nutrition.Carbohydrates)];
            nutrition.Fats = attributes[nameof(nutrition.Fats)];
            nutrition.Allergens = attributes[nameof(nutrition.Allergens)];

            return nutrition;
        }
    }

    public class ClothingFactory : ProductFactory
    {
        public override Product CreateProduct(string name, string certification, string ecologicPrint, int minimumAge, string howToUse, string howToReducePrint, Dictionary<string, string> attributes)
        {
            Clothing clothing = CreateBaseProduct<Clothing>(name, certification, ecologicPrint, minimumAge, howToUse, howToReducePrint);

            clothing.Brand = attributes[nameof(clothing.Brand)];
            clothing.Size = attributes[nameof(clothing.Size)];
            clothing.Color = attributes[nameof(clothing.Color)];
            clothing.Material = attributes[nameof(clothing.Material)];

            return clothing;
        }
    }

    public class BookFactory : ProductFactory
    {

        public override Product CreateProduct(string name, string certification, string ecologicPrint, int minimumAge, string howToUse, string howToReducePrint, Dictionary<string, string> attributes)
        {
            Book book = CreateBaseProduct<Book>(name, certification, ecologicPrint, minimumAge, howToUse, howToReducePrint);

            book.Author = attributes[nameof(book.Author)];
            book.Publisher = attributes[nameof(book.Publisher)];
            book.Pages = attributes[nameof(book.Pages)];
            book.Language = attributes[nameof(book.Language)];
            book.ISBN = attributes[nameof(book.ISBN)];

            return book;
        }
    }
}
