using System;

namespace SmartTradeLib.Entities
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

        public abstract Product CreateProduct(string name, string certification, string ecologicPrint, int minimumAge, List<string> attributes);
    }

    public class ToyFactory : ProductFactory
    {
        public override Product CreateProduct(string name, string certification, string ecologicPrint, int minimumAge, List<string> attributes)
        {
            Toy toy = new Toy(name, certification, ecologicPrint, minimumAge, attributes[0], attributes[1]);
            return toy;
        }
    }

    public class NutritionFactory : ProductFactory
    {
        public override Product CreateProduct(string name, string certification, string ecologicPrint, int minimumAge, List<string> attributes)
        {
            Nutrition nutrition = new Nutrition(name, certification, ecologicPrint, minimumAge, attributes[0], attributes[1], attributes[2], attributes[3], attributes[4], attributes[5]);
            return nutrition;
        }
    }

    public class ClothingFactory : ProductFactory
    {
        public override Product CreateProduct(string name, string certification, string ecologicPrint, int minimumAge, List<string> attributes)
        {
            Clothing clothing = new Clothing(name, certification, ecologicPrint, minimumAge, attributes[0], attributes[1], attributes[2], attributes[3]);
            return clothing;
        }
    }

    public class BookFactory : ProductFactory
    {

        public override Product CreateProduct(string name, string certification, string ecologicPrint, int minimumAge, List<string> attributes)
        {
            Book book = new Book(name, certification, ecologicPrint, minimumAge, attributes[0], attributes[1], attributes[2], attributes[3], attributes[4]);
            return book;
        }
    }
}
