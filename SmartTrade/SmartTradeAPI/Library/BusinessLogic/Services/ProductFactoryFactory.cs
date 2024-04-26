namespace SmartTrade.Entities;

public class ProductFactoryFactory
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
}