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

        public abstract Product CreateProduct(string name, string certification, string ecologicPrint, int minimumAge, string howToUse, string howToReducePrint, List<string> attributes);
    }

    public class ToyFactory : ProductFactory
    {
        public override Product CreateProduct(string name, string certification, string ecologicPrint, int minimumAge, string howToUse, string howToReducePrint, List<string> attributes)
        {
            Toy toy = CreateBaseProduct<Toy>(name, certification, ecologicPrint, minimumAge, howToUse, howToReducePrint);

            toy.Brand = attributes[0];
            toy.Material = attributes[1];

            return toy;
        }
    }

    public class NutritionFactory : ProductFactory
    {
        public override Product CreateProduct(string name, string certification, string ecologicPrint, int minimumAge, string howToUse, string howToReducePrint, List<string> attributes)
        {
            Nutrition nutrition = CreateBaseProduct<Nutrition>(name, certification, ecologicPrint, minimumAge, howToUse, howToReducePrint);
            
            nutrition.Weight = attributes[0];
            nutrition.Calories = attributes[1];
            nutrition.Proteins = attributes[2];
            nutrition.Carbohydrates = attributes[3];
            nutrition.Fats = attributes[4];
            nutrition.Allergens = attributes[5];

            return nutrition;
        }
    }

    public class ClothingFactory : ProductFactory
    {
        public override Product CreateProduct(string name, string certification, string ecologicPrint, int minimumAge, string howToUse, string howToReducePrint, List<string> attributes)
        {
            Clothing clothing = CreateBaseProduct<Clothing>(name, certification, ecologicPrint, minimumAge, howToUse, howToReducePrint);

            clothing.Brand = attributes[0];
            clothing.Size = attributes[1];
            clothing.Color = attributes[2];
            clothing.Material = attributes[3];

            return clothing;
        }
    }

    public class BookFactory : ProductFactory
    {

        public override Product CreateProduct(string name, string certification, string ecologicPrint, int minimumAge, string howToUse, string howToReducePrint, List<string> attributes)
        {
            Book book = CreateBaseProduct<Book>(name, certification, ecologicPrint, minimumAge, howToUse, howToReducePrint);

            book.Author = attributes[0];
            book.Publisher = attributes[1];
            book.Pages = attributes[2];
            book.Language = attributes[3];
            book.ISBN = attributes[4];

            return book;
        }
    }
}
